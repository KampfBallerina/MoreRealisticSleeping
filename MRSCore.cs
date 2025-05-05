using MelonLoader;
using Il2CppScheduleOne.GameTime;
using Il2CppScheduleOne.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Il2CppScheduleOne.PlayerScripts;
using MoreRealisticSleeping.Config;
using MelonLoader.Utils;
using Il2CppScheduleOne.Money;
using UnityEngine.UI;
using MoreRealisticSleeping.Util;

[assembly: MelonInfo(typeof(MoreRealisticSleeping.MRSCore), "MoreRealisticSleeping", "1.0.0", "KampfBallerina", null)]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace MoreRealisticSleeping
{

    /*
    ObjectScripts.Bed -> Bed.Min_sleep_time, sleep_time_scale
    UI.DailySummary .SleepStart(), .SleepEnd()
    PlayerScripts.Player .IsReadyToSleep, isSleeping, areAllPlayersReadyToSleep_Public_Static_boolean
    UI.SleepCanvas
    GameTime.TimeManager.EndSleep()

    -> SleepButtonPressed() -> SleepStart() -> PostSleepEvent


    ClampWakeTime(int time) -> 0-2400 Ist die Weckzeit
    */

    // UI/HUD/UnreadMessagePrompt

    // 1600 = 16:00 Uhr
    public class MRSCore : MelonMod
    {
        public static MRSCore Instance { get; private set; }
        public bool isLegitVersion = false;
        public bool canTriggerSleep = true;
        private bool isCooldownActive = false;
        private bool isForcedSleep = false;
        private bool isFirstSleep = true;
        public SleepCanvas sleepCanvas;
        public TimeManager timeManager = null;
        public ConfigState config = null;
        private static readonly string ConfigFolder = Path.Combine(MelonEnvironment.UserDataDirectory, "MoreRealisticSleeping");
        private static readonly string AppIconFilePath = Path.Combine(ConfigFolder, "SleepingAppIcon.png");
        private static readonly string UIElementsFolder = Path.Combine(ConfigFolder, "UIElements");
        public NotificationsManager notificationsManager;
        public MoneyManager moneyManager;
        public PropertyManager propertyManager;
        public static PhoneApp.SleepingApp sleepingApp = new PhoneApp.SleepingApp();

        public bool isMonitorTimeForSleepRunning = false;
        public bool isStartCooldownRunning = false;
        public Coroutine monitorTimeForSleepCoroutine;
        public Coroutine startCooldownCoroutine;


        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
            MRSCore.Instance = this;

        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName == "Main")
            {
                MRSCore.Instance.config = Config.ConfigManager.Load();
                MRSCore.Instance.propertyManager = new PropertyManager();
                MRSCore.Instance.moneyManager = UnityEngine.Object.FindObjectOfType<MoneyManager>();
                MRSCore.Instance.notificationsManager = UnityEngine.Object.FindObjectOfType<NotificationsManager>();

                if (MRSCore.Instance.config.Use_Legit_Version)
                {
                    LoggerInstance.Msg("Use_Legit_Version is enabled. Adjusting behavior accordingly.");
                    isLegitVersion = true;
                }
                else
                {
                    LoggerInstance.Msg("Use_Legit_Version is disabled. Proceeding with default behavior.");
                    MelonCoroutines.Start(StartAppCoroutinesAfterDelay());
                }
                MelonCoroutines.Start(InitTimeManager());

            }
            else if (sceneName.Equals("Menu", StringComparison.OrdinalIgnoreCase))
            {
                //LoggerInstance.Msg("Menu scene loaded. Stopping time monitoring.");
                if (timeManager != null)
                {

                    ResetAllVariables();
                }
            }
        }

        private void ResetAllVariables()
        {
            StopAllCoroutines();
            MelonCoroutines.Stop(MRSCore.sleepingApp.InitializeLaunderApp());
            MelonCoroutines.Stop(WaitForSleepingAppAndCreateEntry());
            MelonCoroutines.Stop(StartAppCoroutinesAfterDelay());
            MelonCoroutines.Stop(InitTimeManager());

            MRSCore.Instance.config = null;

            isLegitVersion = false;
            MRSCore.Instance.timeManager = null;
            MRSCore.Instance.sleepCanvas = null;
            isFirstSleep = true;

            isForcedSleep = false;
            canTriggerSleep = true;
            isCooldownActive = false;

            MRSCore.Instance.propertyManager = null;
            MRSCore.Instance.notificationsManager = null;
            MRSCore.Instance.moneyManager = null;

            isMonitorTimeForSleepRunning = false;
            isStartCooldownRunning = false;

            LoggerInstance.Msg("All variables reset.");
        }
        public IEnumerator InitTimeManager()
        {
            while (timeManager == null)
            {
                timeManager = GameObject.FindObjectOfType<TimeManager>();
                if (timeManager != null)
                {
                    MRSCore.Instance.sleepCanvas = UnityEngine.Object.FindObjectOfType<SleepCanvas>();
                    if (MRSCore.Instance.sleepCanvas != null && isFirstSleep)
                    {
                        void FuncThatCallsOtherFunc() => TriggerAfterSleepEffect();
                        MRSCore.Instance.sleepCanvas.onSleepEndFade.AddListener((UnityAction)FuncThatCallsOtherFunc);
                        void FuncThatCallsCooldownFunc() => startCooldownCoroutine = (Coroutine)MelonCoroutines.Start(StartCooldown());
                        MRSCore.Instance.sleepCanvas.onSleepEndFade.AddListener((UnityAction)FuncThatCallsCooldownFunc);
                        isFirstSleep = false; // Setze isFirstSleep auf false, um die Listener nur einmal hinzuzufügen
                    }
                    monitorTimeForSleepCoroutine = (Coroutine)MelonCoroutines.Start(MonitorTimeForSleep());
                    break;
                }
                yield return new WaitForSeconds(2f);
            }

            // LoggerInstance.Msg("Current GetDateTime: " + timeManager.GetDateTime());
            // LoggerInstance.Msg("Current Day: " + timeManager.CurrentDay);
            // LoggerInstance.Msg("Current Hour: " + timeManager.CurrentTime);

        }

        public void StopAllCoroutines()
        {
            // Überprüfe und stoppe die Coroutine MonitorTimeForSleep
            if (monitorTimeForSleepCoroutine != null)
            {
                MelonCoroutines.Stop(monitorTimeForSleepCoroutine);
                monitorTimeForSleepCoroutine = null;
                isMonitorTimeForSleepRunning = false;
                LoggerInstance.Msg("Stopped MonitorTimeForSleep coroutine.");
            }

            // Überprüfe und stoppe die Coroutine StartCooldown
            if (startCooldownCoroutine != null)
            {
                MelonCoroutines.Stop(startCooldownCoroutine);
                startCooldownCoroutine = null;
                isStartCooldownRunning = false;
                LoggerInstance.Msg("Stopped StartCooldown coroutine.");
            }

            isForcedSleep = false;
            canTriggerSleep = true;
            isCooldownActive = false;

            //LoggerInstance.Msg("All coroutines have been checked and stopped if running.");
        }

        public IEnumerator MonitorTimeForSleep()
        {
            isMonitorTimeForSleepRunning = true;

            LoggerInstance.Msg("Monitoring time for sleep...");
            while (true)
            {
                if (timeManager != null)
                {
                    if (400 <= timeManager.CurrentTime && timeManager.CurrentTime < 650) // 4:00 AM - 6:50 AM
                    {
                        if (canTriggerSleep && !isStartCooldownRunning)
                        {
                            LoggerInstance.Msg("Triggering sleep as CurrentTime is between 400 and 650.");
                            if (MRSCore.Instance.config.SleepSettings.Enable_Forced_Sleep == true)
                            {
                                ForceSleep();
                            }
                            else
                            {
                                LoggerInstance.Msg("Forced sleep is disabled in the config.");
                            }

                            // Cooldown von 10 Sekunden aktivieren
                            startCooldownCoroutine = (Coroutine)MelonCoroutines.Start(StartCooldown());
                        }
                    }
                }
                else
                {
                    LoggerInstance.Warning("TimeManager is null. Waiting for initialization..");
                }

                // Wartezeit zwischen den Überprüfungen
                yield return new WaitForSeconds(5f);
            }
        }

        public IEnumerator StartAppCoroutinesAfterDelay()
        {
            bool delayedInit = false;
            while (!delayedInit && !isLegitVersion)
            {
                //  MelonLogger.Msg("Waiting 5 Seconds for other mods to load their apps..");
                delayedInit = true;
                yield return new WaitForSeconds(7f);
            }

            if (!isLegitVersion)
            {
                MelonCoroutines.Start(MRSCore.sleepingApp.InitializeLaunderApp());
                MelonCoroutines.Start(WaitForSleepingAppAndCreateEntry());
            }
        }


        private void ForceSleep()
        {
            // Überprüfen, ob der Cooldown aktiv ist
            if (!canTriggerSleep)
            {
                LoggerInstance.Msg("TriggerSleep() is on cooldown. Skipping execution.");
                return;
            }

            try
            {
                // SleepCanvas-Instanz finden, falls sie noch nicht gesetzt ist
                if (MRSCore.Instance.sleepCanvas == null)
                {
                    MRSCore.Instance.sleepCanvas = UnityEngine.Object.FindObjectOfType<SleepCanvas>();
                }

                // Überprüfen, ob SleepCanvas gefunden wurde
                if (MRSCore.Instance.sleepCanvas != null)
                {
                    if (isFirstSleep)
                    {
                        //Check if Listeners are set
                        void FuncThatCallsOtherFunc() => TriggerAfterSleepEffect();
                        MRSCore.Instance.sleepCanvas.onSleepEndFade.AddListener((UnityAction)FuncThatCallsOtherFunc);
                        void FuncThatCallsCooldownFunc() => startCooldownCoroutine = (Coroutine)MelonCoroutines.Start(StartCooldown());
                        MRSCore.Instance.sleepCanvas.onSleepEndFade.AddListener((UnityAction)FuncThatCallsCooldownFunc);
                        isFirstSleep = false; // Setze isFirstSleep auf false, um die Listener nur einmal hinzuzufügen
                    }

                    canTriggerSleep = false; // Cooldown aktivieren
                    MRSCore.Instance.sleepCanvas.SetIsOpen(open: true); // SleepCanvas öffnen
                    MRSCore.Instance.sleepCanvas.SleepStart(); // Schlaf starten
                    isForcedSleep = true; // Setze isForcedSleep auf true

                    // LoggerInstance.Msg("Sleep forced successfully.");
                }
                else
                {
                    LoggerInstance.Error("Could not find SleepCanvas instance.");
                }
            }
            catch (Exception ex)
            {
                LoggerInstance.Error("Error in TriggerSleep: " + ex.Message + "\n" + ex.StackTrace);
            }
        }

        public IEnumerator StartCooldown()
        {
            // Markiere die Coroutine als aktiv
            if (isCooldownActive)
            {
                // LoggerInstance.Msg("Cooldown is already active. Skipping StartCooldown.");
                yield break; // Beende die Coroutine, wenn bereits ein Cooldown läuft
            }

            isStartCooldownRunning = true;
            float cooldownTime = MRSCore.Instance.config.SleepSettings.Cooldown_Time;
            if (cooldownTime <= 10f)
            {
                LoggerInstance.Error("Cooldown time is not set or invalid. Using default (300sec).");
                cooldownTime = 300f; // Standard-Cooldown-Zeit
            }
            isCooldownActive = true; // Cooldown als aktiv markieren
            LoggerInstance.Msg($"Started {cooldownTime}sec cooldown before next sleep can be triggered.");
            canTriggerSleep = false; // Setze die Variable auf false, um den Cooldown zu aktivieren
            yield return new WaitForSeconds(cooldownTime); // Cooldown-Dauer von 300 Sekunden
            canTriggerSleep = true; // Cooldown beenden
            isCooldownActive = false; // Cooldown als beendet markieren
            isStartCooldownRunning = false;
            LoggerInstance.Msg("Cooldown ended. Sleep triggering is now enabled again.");
        }

        private void TriggerAfterSleepEffect()
        {
            //Spieler wurde zum Schlafen gezwungen
            if (isForcedSleep)
            {
                if (MRSCore.Instance.config.SleepSettings.Enable_Negative_Effects)
                {
                    Player[] players = GameObject.FindObjectsOfType<Player>();
                    foreach (Player player in players)
                    {
                        if (player != null && player.gameObject != null)
                        {
                            // Wahrscheinlichkeit für negative Effekte prüfen
                            var probability = MRSCore.Instance.config.SleepSettings.Negative_Effects_Probability / 100f;
                            var random = new System.Random();
                            if (random.NextDouble() <= probability)
                            {
                                propertyManager.ApplyNegativePropertyToPlayer(player);
                            }
                            else
                            {
                                LoggerInstance.Msg("No negative effect applied due to probability.");
                            }
                            isForcedSleep = false;
                        }
                        else
                        {
                            LoggerInstance.Error("Player or Player GameObject is null.");
                        }
                    }
                } else {
                    LoggerInstance.Msg("Negative effects are disabled in the config.");
                }
            }
            else //Spieler ist frühzeitig schlafen gegangen || TODO: Uhrzeit check, um festzulegen, wann früh ist
            {
                if (MRSCore.Instance.config.SleepSettings.Enable_Positive_Effects)
                {
                    Player[] players = GameObject.FindObjectsOfType<Player>();
                    foreach (Player player in players)
                    {
                        if (player != null && player.gameObject != null)
                        {
                            // Wahrscheinlichkeit für positive Effekte prüfen
                            var probability = MRSCore.Instance.config.SleepSettings.Positive_Effects_Probability / 100f;
                            var random = new System.Random();
                            if (random.NextDouble() <= probability)
                            {
                                propertyManager.ApplyPositivePropertyToPlayer(player);
                            }
                            else
                            {
                                LoggerInstance.Msg("No positive effect applied due to probability.");
                            }
                        }
                        else
                        {
                            LoggerInstance.Error("Player or Player GameObject is null.");
                        }
                    }
                } else {
                    LoggerInstance.Msg("Positive effects are disabled in the config.");
                }
            }
        }
        private IEnumerator WaitForSleepingAppAndCreateEntry()
        {
            while (!MRSCore.sleepingApp._isSleepingAppLoaded)
            {
                yield return new WaitForSeconds(2f); // Warte 2 Sekunde
            }
            CreateAppEntries();
        }

        public void CreateAppEntries()
        {
            if (isLegitVersion)
            {
                return;
            }

            // Überprüfe, ob die Laundering App geladen ist
            if (!MRSCore.sleepingApp._isSleepingAppLoaded)
            {
                MelonCoroutines.Start(WaitForSleepingAppAndCreateEntry());
                return;
            }
            //Settings
            sleepingApp.AddEntryFromTemplate("GeneralSettingsSection", "General Settings", "~Forced Sleep, Enable/Disable Effects, Probabilitys, Durations~", null, ColorUtil.GetColor("Cyan"), Path.Combine(UIElementsFolder, "Settings.png"));

            sleepingApp.AddEntryFromTemplate("PositiveEffectsSection", "Positive Effects", "~Choose positive Early-Sleep-Effects~", null, ColorUtil.GetColor("Cyan"), Path.Combine(UIElementsFolder, "PositiveEffects.png"));

            sleepingApp.AddEntryFromTemplate("NegativeEffectsSection", "Negative Effects", "~Choose negative Forced-Sleep-Effects~", null, ColorUtil.GetColor("Cyan"), Path.Combine(UIElementsFolder, "NegativeEffects.png"));
        }

        public void ChangeAppIconImage(GameObject appIcon, string ImagePath)
        {
            if (ImagePath == null)
            {
                MelonLogger.Msg("ImagePath is null, skipping image change.");
                return;
            }
            Transform transform = appIcon.transform.Find("Mask/Image");
            GameObject gameObject = (transform != null) ? transform.gameObject : null;
            if (gameObject == null)
            {
                return;
            }
            Image component = gameObject.GetComponent<Image>();
            if (!(component == null))
            {
                Texture2D texture2D = Utils.LoadCustomImage(ImagePath);
                if (!(texture2D == null))
                {
                    Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
                    component.sprite = sprite;
                }
                else
                {
                    ((MelonBase)this).LoggerInstance.Msg("Custom image failed to load");
                }
            }
        }

        public void RegisterApp(GameObject App, string Title = "Unknown App")
        {
            GameObject appIcons = GameObject.Find("Player_Local/CameraContainer/Camera/OverlayCamera/GameplayMenu/Phone/phone/HomeScreen/AppIcons");
            App.transform.SetParent(appIcons.transform, worldPositionStays: false);
            ((MelonBase)this).LoggerInstance.Msg("Added " + Title + " to Homescreen.");
        }


    }
}