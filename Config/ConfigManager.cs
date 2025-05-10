using Il2CppScheduleOne.UI.MainMenu;
using MelonLoader;
using MelonLoader.Utils;
using Newtonsoft.Json;
using UnityEngine;

namespace MoreRealisticSleeping.Config
{
    public static class ConfigManager
    {
        private static bool isFirstFailure = true;
        private static bool isConfigUpdated = false;
        private static readonly string ConfigFolder = Path.Combine(MelonEnvironment.UserDataDirectory, "MoreRealisticSleeping");
        private static readonly string FilePath = Path.Combine(ConfigFolder, "MoreRealisticSleeping.json");

        public static ConfigState Load()
        {
            ConfigState configState = new ConfigState();

            if (!Directory.Exists(ConfigFolder))
            {
                Directory.CreateDirectory(ConfigFolder);
            }

            bool isConfigFileMissing = !File.Exists(ConfigManager.FilePath);
            ConfigState result;
            if (isConfigFileMissing)
            {
                MelonLogger.Warning("Config file not found. Creating a new one with default values.");
                MRSCore.Instance.StopAllCoroutines();
                ConfigManager.Save(configState);
                result = configState;
            }
            else
            {
                try
                {
                    string fileContent = File.ReadAllText(ConfigManager.FilePath);
                    dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(fileContent) ?? new ConfigState();
                    isConfigUpdated = false;

                    // Ergänze alle Felder aus ConfigState
                    EnsureFieldExists((object)jsonObject, "Use_Legit_Version", configState.Use_Legit_Version);

                    EnsureFieldExists((object)jsonObject, "SleepSettings", configState.SleepSettings);
                    EnsureFieldExists((object)jsonObject.SleepSettings, "Enable_Forced_Sleep", configState.SleepSettings.Enable_Forced_Sleep);
                    EnsureFieldExists((object)jsonObject.SleepSettings, "Cooldown_Time", configState.SleepSettings.Cooldown_Time);
                    EnsureFieldExists((object)jsonObject.SleepSettings, "Forced_Sleep_Delay", configState.SleepSettings.Forced_Sleep_Delay);
                    EnsureFieldExists((object)jsonObject.SleepSettings, "Auto_Skip_Daily_Summary", configState.SleepSettings.Auto_Skip_Daily_Summary);
                    EnsureFieldExists((object)jsonObject.SleepSettings, "Enable_Positive_Effects", configState.SleepSettings.Enable_Positive_Effects);
                    EnsureFieldExists((object)jsonObject.SleepSettings, "Positive_Effects_Probability", configState.SleepSettings.Positive_Effects_Probability);
                    EnsureFieldExists((object)jsonObject.SleepSettings, "Positive_Effects_Duration", configState.SleepSettings.Positive_Effects_Duration);
                    EnsureFieldExists((object)jsonObject.SleepSettings, "Enable_Negative_Effects", configState.SleepSettings.Enable_Negative_Effects);
                    EnsureFieldExists((object)jsonObject.SleepSettings, "Negative_Effects_Probability", configState.SleepSettings.Negative_Effects_Probability);
                    EnsureFieldExists((object)jsonObject.SleepSettings, "Negative_Effects_Duration", configState.SleepSettings.Negative_Effects_Duration);
                    EnsureFieldExists((object)jsonObject.SleepSettings, "Enable_Effect_Notifications", configState.SleepSettings.Enable_Effect_Notifications);

                    EnsureFieldExists((object)jsonObject, "EffectSettings", configState.EffectSettings);
                    EnsureFieldExists((object)jsonObject.EffectSettings, "PositiveEffectSettings", configState.EffectSettings.PositiveEffectSettings);
                    EnsureFieldExists((object)jsonObject.EffectSettings.PositiveEffectSettings, "Anti_Gravity", configState.EffectSettings.PositiveEffectSettings.Anti_Gravity);
                    EnsureFieldExists((object)jsonObject.EffectSettings.PositiveEffectSettings, "Athletic", configState.EffectSettings.PositiveEffectSettings.Athletic);
                    EnsureFieldExists((object)jsonObject.EffectSettings.PositiveEffectSettings, "Bright_Eyed", configState.EffectSettings.PositiveEffectSettings.Bright_Eyed);
                    EnsureFieldExists((object)jsonObject.EffectSettings.PositiveEffectSettings, "Calming", configState.EffectSettings.PositiveEffectSettings.Calming);
                    EnsureFieldExists((object)jsonObject.EffectSettings.PositiveEffectSettings, "Calorie_Dense", configState.EffectSettings.PositiveEffectSettings.Calorie_Dense);
                    EnsureFieldExists((object)jsonObject.EffectSettings.PositiveEffectSettings, "Electrifying", configState.EffectSettings.PositiveEffectSettings.Electrifying);
                    EnsureFieldExists((object)jsonObject.EffectSettings.PositiveEffectSettings, "Energizing", configState.EffectSettings.PositiveEffectSettings.Energizing);
                    EnsureFieldExists((object)jsonObject.EffectSettings.PositiveEffectSettings, "Euphoric", configState.EffectSettings.PositiveEffectSettings.Euphoric);
                    EnsureFieldExists((object)jsonObject.EffectSettings.PositiveEffectSettings, "Focused", configState.EffectSettings.PositiveEffectSettings.Focused);
                    EnsureFieldExists((object)jsonObject.EffectSettings.PositiveEffectSettings, "Munchies", configState.EffectSettings.PositiveEffectSettings.Munchies);
                    EnsureFieldExists((object)jsonObject.EffectSettings.PositiveEffectSettings, "Refreshing", configState.EffectSettings.PositiveEffectSettings.Refreshing);
                    EnsureFieldExists((object)jsonObject.EffectSettings.PositiveEffectSettings, "Sneaky", configState.EffectSettings.PositiveEffectSettings.Sneaky);

                    EnsureFieldExists((object)jsonObject.EffectSettings, "NegativeEffectSettings", configState.EffectSettings.NegativeEffectSettings);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Balding", configState.EffectSettings.NegativeEffectSettings.Balding);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Bright_Eyed", configState.EffectSettings.NegativeEffectSettings.Bright_Eyed);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Calming", configState.EffectSettings.NegativeEffectSettings.Calming);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Calorie_Dense", configState.EffectSettings.NegativeEffectSettings.Calorie_Dense);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Cyclopean", configState.EffectSettings.NegativeEffectSettings.Cyclopean);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Disorienting", configState.EffectSettings.NegativeEffectSettings.Disorienting);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Electrifying", configState.EffectSettings.NegativeEffectSettings.Electrifying);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Explosive", configState.EffectSettings.NegativeEffectSettings.Explosive);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Foggy", configState.EffectSettings.NegativeEffectSettings.Foggy);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Gingeritis", configState.EffectSettings.NegativeEffectSettings.Gingeritis);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Glowing", configState.EffectSettings.NegativeEffectSettings.Glowing);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Jennerising", configState.EffectSettings.NegativeEffectSettings.Jennerising);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Laxative", configState.EffectSettings.NegativeEffectSettings.Laxative);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Lethal", configState.EffectSettings.NegativeEffectSettings.Lethal);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Long_Faced", configState.EffectSettings.NegativeEffectSettings.Long_Faced);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Paranoia", configState.EffectSettings.NegativeEffectSettings.Paranoia);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Schizophrenic", configState.EffectSettings.NegativeEffectSettings.Schizophrenic);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Sedating", configState.EffectSettings.NegativeEffectSettings.Sedating);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Seizure_Inducing", configState.EffectSettings.NegativeEffectSettings.Seizure_Inducing);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Shrinking", configState.EffectSettings.NegativeEffectSettings.Shrinking);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Slippery", configState.EffectSettings.NegativeEffectSettings.Slippery);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Smelly", configState.EffectSettings.NegativeEffectSettings.Smelly);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Spicy", configState.EffectSettings.NegativeEffectSettings.Spicy);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Thought_Provoking", configState.EffectSettings.NegativeEffectSettings.Thought_Provoking);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Toxic", configState.EffectSettings.NegativeEffectSettings.Toxic);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Tropic_Thunder", configState.EffectSettings.NegativeEffectSettings.Tropic_Thunder);
                    EnsureFieldExists((object)jsonObject.EffectSettings.NegativeEffectSettings, "Zombifying", configState.EffectSettings.NegativeEffectSettings.Zombifying);

                    EnsureFieldExists((object)jsonObject.ArrestedEventSettings, "Enable_GetArrested_Event", configState.ArrestedEventSettings.Enable_GetArrested_Event);
                    EnsureFieldExists((object)jsonObject.ArrestedEventSettings, "Enable_GetArrested_Event_SaveSpaces", configState.ArrestedEventSettings.Enable_GetArrested_Event_SaveSpaces);
                    EnsureFieldExists((object)jsonObject.ArrestedEventSettings, "GetArrested_Event_Probability", configState.ArrestedEventSettings.GetArrested_Event_Probability);

                    EnsureFieldExists((object)jsonObject.MurderedEventSettings, "Enable_GetMurdered_Event", configState.MurderedEventSettings.Enable_GetMurdered_Event);
                    EnsureFieldExists((object)jsonObject.MurderedEventSettings, "Allow_GetMurdered_Event_Respawning", configState.MurderedEventSettings.Allow_GetMurdered_Event_Respawning);
                    EnsureFieldExists((object)jsonObject.MurderedEventSettings, "Enable_GetMurdered_Event_SaveSpaces", configState.MurderedEventSettings.Enable_GetMurdered_Event_SaveSpaces);
                    EnsureFieldExists((object)jsonObject.MurderedEventSettings, "GetMurdered_Event_Probability", configState.MurderedEventSettings.GetMurdered_Event_Probability);
                    
                    if (isConfigUpdated)
                    {
                        MRSCore.Instance.StopAllCoroutines();
                        File.WriteAllText(ConfigManager.FilePath, jsonObject.ToString(Formatting.Indented));
                        MelonLogger.Msg("Config file updated with missing fields.");
                        MRSCore.Instance.monitorTimeForSleepCoroutine = (Coroutine)MelonCoroutines.Start(MRSCore.Instance.MonitorTimeForSleep());
                    }

                    ConfigState loadedConfigState = jsonObject.ToObject<ConfigState>();

                    // Check if Use Legit Version is a boolean
                    if (loadedConfigState.Use_Legit_Version != true && loadedConfigState.Use_Legit_Version != false)
                    {
                        MelonLogger.Warning("Invalid Use_Legit_Version in config. Adding default value (false).");
                        loadedConfigState.Use_Legit_Version = configState.Use_Legit_Version;
                        isConfigUpdated = true;
                    }
                    // Check Sleep Settings
                    ValidateSleepSettings(loadedConfigState, configState, ref isConfigUpdated);

                    // Check Positive Effect Settings
                    ValidatePositiveEffects(loadedConfigState, configState, ref isConfigUpdated);

                    // Check Event Settings
                    ValidateEventSettings(loadedConfigState, configState, ref isConfigUpdated);

                    // Speichere die aktualisierte Konfiguration, falls Änderungen vorgenommen wurden
                    if (isConfigUpdated)
                    {
                        ConfigManager.Save(loadedConfigState);
                        isConfigUpdated = false; // Reset after saving
                    }
                    result = loadedConfigState;
                }
                catch
                {
                    if (isFirstFailure)
                    {
                        MelonLogger.Warning("Failed to read MoreRealisticSleeping config. Recreating config file to fix structure. Please restart the game.");
                        isFirstFailure = false; // Set to false after the first failure

                        // Reset the configuration file
                        ResetConfig();
                        return result = MRSCore.Instance.config = Load(); // Try loading again after resetting
                    }
                    else
                    {
                        MelonLogger.Error("Failed to read MoreRealisticSleeping config. Please check the file structure.");
                        result = configState; // Return the default config state in case of failure
                    }
                }
            }
            return result;
        }

        private static void EnsureFieldExists<T>(dynamic parent, string fieldName, T defaultValue)
        {
            if (parent[fieldName] == null)
            {
                MelonLogger.Warning($"Field '{fieldName}' is missing in the config. Adding default value ({defaultValue}).");
                parent[fieldName] = defaultValue;

                // Setze isConfigUpdated auf true
                isConfigUpdated = true;
            }
            else if (defaultValue is not null && defaultValue.GetType().IsClass && !(defaultValue is string))
            {
                // Rekursive Überprüfung für verschachtelte Objekte
                foreach (var property in defaultValue.GetType().GetProperties())
                {
                    string subFieldName = property.Name;
                    var subDefaultValue = property.GetValue(defaultValue);

                    if (parent[fieldName][subFieldName] == null)
                    {
                        MelonLogger.Warning($"Field '{fieldName}.{subFieldName}' is missing in the config. Adding default value ({subDefaultValue}).");
                        parent[fieldName][subFieldName] = subDefaultValue;

                        // Setze isConfigUpdated auf true
                        isConfigUpdated = true;
                    }
                    else if (subDefaultValue is not null && subDefaultValue.GetType().IsClass && !(subDefaultValue is string))
                    {
                        // Rekursion für tiefere Ebenen
                        EnsureFieldExists(parent[fieldName], subFieldName, subDefaultValue);
                    }
                }
            }
        }

        public static void ResetConfig()
        {
            try
            {
                MRSCore.Instance.StopAllCoroutines();
                // Überprüfen, ob die Datei existiert
                if (File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                    MelonLogger.Msg("Existing MoreRealisticSleeping.json file deleted.");
                }

                // Erstellen einer neuen Datei mit Standardwerten
                ConfigState defaultConfig = new ConfigState();
                Save(defaultConfig);

                // Sicherstellen, dass die Datei vollständig geschrieben wurde
                if (!File.Exists(FilePath))
                {
                    throw new Exception("Failed to create the new configuration file.");
                }

                MelonLogger.Msg("New MoreRealisticSleeping.json file created with default values.");
                MRSCore.Instance.monitorTimeForSleepCoroutine = (Coroutine)MelonCoroutines.Start(MRSCore.Instance.MonitorTimeForSleep());
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Failed to reset MoreRealisticSleeping.json: {ex.Message}");
            }
        }

        public static void Save(ConfigState config)
        {
            MRSCore.Instance.StopAllCoroutines();
            try
            {
                string contents = JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(ConfigManager.FilePath, contents);
                MelonLogger.Msg("Configuration saved successfully.");
            }
            catch (Exception ex)
            {
                MelonLogger.Error("Failed to save MoreRealisticSleeping config: " + ex.Message);
            }
            MRSCore.Instance.monitorTimeForSleepCoroutine = (Coroutine)MelonCoroutines.Start(MRSCore.Instance.MonitorTimeForSleep());
        }
        private static void ValidateSleepSettings(ConfigState loadedConfigState, ConfigState configState, ref bool isConfigUpdated)
        {
            // Check if Enable Forced Sleep is a boolean
            if (loadedConfigState.SleepSettings.Enable_Forced_Sleep != true && loadedConfigState.SleepSettings.Enable_Forced_Sleep != false)
            {
                MelonLogger.Warning("Invalid Enable_Forced_Sleep in config. Adding default value (true).");
                loadedConfigState.SleepSettings.Enable_Forced_Sleep = configState.SleepSettings.Enable_Forced_Sleep;
                isConfigUpdated = true;
            }


            // Check if Cooldown Time is a float
            if (loadedConfigState.SleepSettings.Cooldown_Time < 59f || loadedConfigState.SleepSettings.Cooldown_Time > 600f)
            {
                MelonLogger.Warning("Invalid Cooldown_Time in config. Needs to be >= 60sec and <= 600. Adding default value (500sec).");
                loadedConfigState.SleepSettings.Cooldown_Time = configState.SleepSettings.Cooldown_Time;
                isConfigUpdated = true;
            }

            // Check if Force Sleep Delay is a float
            if (loadedConfigState.SleepSettings.Forced_Sleep_Delay < 0f || loadedConfigState.SleepSettings.Forced_Sleep_Delay > 1000f)
            {
                MelonLogger.Warning("Invalid Forced_Sleep_Delay in config. Needs to be >= 0sec and <= 1000sec. Adding default value (0sec).");
                loadedConfigState.SleepSettings.Forced_Sleep_Delay = configState.SleepSettings.Forced_Sleep_Delay;
                isConfigUpdated = true;
            }

            // Check if Force Instant Continue is a boolean
            if (loadedConfigState.SleepSettings.Auto_Skip_Daily_Summary != true && loadedConfigState.SleepSettings.Auto_Skip_Daily_Summary != false)
            {
                MelonLogger.Warning("Invalid Force_Instant_Continue in config. Adding default value (false).");
                loadedConfigState.SleepSettings.Auto_Skip_Daily_Summary = configState.SleepSettings.Auto_Skip_Daily_Summary;
                isConfigUpdated = true;
            }

            // Check if Enable Positive Effects is a boolean
            if (loadedConfigState.SleepSettings.Enable_Positive_Effects != true && loadedConfigState.SleepSettings.Enable_Positive_Effects != false)
            {
                MelonLogger.Warning("Invalid Enable_Positive_Effects in config. Adding default value (true).");
                loadedConfigState.SleepSettings.Enable_Positive_Effects = configState.SleepSettings.Enable_Positive_Effects;
                isConfigUpdated = true;
            }

            // Check if Positive Effects Probability is a float
            if (loadedConfigState.SleepSettings.Positive_Effects_Probability > 100f || loadedConfigState.SleepSettings.Positive_Effects_Probability < 1f)
            {
                MelonLogger.Warning("Invalid Positive_Effects_Probability in config. Needs to be between 1 and 100. Adding default value (25%).");
                loadedConfigState.SleepSettings.Positive_Effects_Probability = configState.SleepSettings.Positive_Effects_Probability;
                isConfigUpdated = true;
            }

            // Check if Positive Effects Duration is a float
            if (loadedConfigState.SleepSettings.Positive_Effects_Duration < 10f)
            {
                MelonLogger.Warning("Invalid Positive_Effects_Duration in config. Needs to be > 10sec. Adding default value (60sec).");
                loadedConfigState.SleepSettings.Positive_Effects_Duration = configState.SleepSettings.Positive_Effects_Duration;
                isConfigUpdated = true;
            }

            // Check if Enable Negative Effects is a boolean
            if (loadedConfigState.SleepSettings.Enable_Negative_Effects != true && loadedConfigState.SleepSettings.Enable_Negative_Effects != false)
            {
                MelonLogger.Warning("Invalid Enable_Negative_Effects in config. Adding default value (true).");
                loadedConfigState.SleepSettings.Enable_Negative_Effects = configState.SleepSettings.Enable_Negative_Effects;
                isConfigUpdated = true;
            }

            // Check if Negative Effects Probability is a float
            if (loadedConfigState.SleepSettings.Negative_Effects_Probability > 100f || loadedConfigState.SleepSettings.Negative_Effects_Probability < 1f)
            {
                MelonLogger.Warning("Invalid Negative_Effects_Probability in config. Needs to be between 1 and 100. Adding default value (50%).");
                loadedConfigState.SleepSettings.Negative_Effects_Probability = configState.SleepSettings.Negative_Effects_Probability;
                isConfigUpdated = true;
            }

            // Check if Negative Effects Duration is a float
            if (loadedConfigState.SleepSettings.Negative_Effects_Duration < 10f)
            {
                MelonLogger.Warning("Invalid Negative_Effects_Duration in config. Needs to be > 10sec. Adding default value (60sec).");
                loadedConfigState.SleepSettings.Negative_Effects_Duration = configState.SleepSettings.Negative_Effects_Duration;
                isConfigUpdated = true;
            }

            // Check if Enable Effect Notifications is a boolean
            if (loadedConfigState.SleepSettings.Enable_Effect_Notifications != true && loadedConfigState.SleepSettings.Enable_Effect_Notifications != false)
            {
                MelonLogger.Warning("Invalid Enable_Effect_Notifications in config. Adding default value (true).");
                loadedConfigState.SleepSettings.Enable_Effect_Notifications = configState.SleepSettings.Enable_Effect_Notifications;
                isConfigUpdated = true;
            }
        }

        private static void ValidatePositiveEffects(ConfigState loadedConfigState, ConfigState configState, ref bool isConfigUpdated)
        {
            // Check if Anti-Gravity is a boolean
            if (loadedConfigState.EffectSettings.PositiveEffectSettings.Anti_Gravity != true && loadedConfigState.EffectSettings.PositiveEffectSettings.Anti_Gravity != false)
            {
                MelonLogger.Warning("Invalid Anti_Gravity in config. Adding default value (true).");
                loadedConfigState.EffectSettings.PositiveEffectSettings.Anti_Gravity = configState.EffectSettings.PositiveEffectSettings.Anti_Gravity;
                isConfigUpdated = true;
            }

            // Check if Athletic is a boolean
            if (loadedConfigState.EffectSettings.PositiveEffectSettings.Athletic != true && loadedConfigState.EffectSettings.PositiveEffectSettings.Athletic != false)
            {
                MelonLogger.Warning("Invalid Athletic in config. Adding default value (true).");
                loadedConfigState.EffectSettings.PositiveEffectSettings.Athletic = configState.EffectSettings.PositiveEffectSettings.Athletic;
                isConfigUpdated = true;
            }

            // Check if Bright-Eyed is a boolean
            if (loadedConfigState.EffectSettings.PositiveEffectSettings.Bright_Eyed != true && loadedConfigState.EffectSettings.PositiveEffectSettings.Bright_Eyed != false)
            {
                MelonLogger.Warning("Invalid Bright_Eyed in config. Adding default value (true).");
                loadedConfigState.EffectSettings.PositiveEffectSettings.Bright_Eyed = configState.EffectSettings.PositiveEffectSettings.Bright_Eyed;
                isConfigUpdated = true;
            }

            // Check if Calming is a boolean
            if (loadedConfigState.EffectSettings.PositiveEffectSettings.Calming != true && loadedConfigState.EffectSettings.PositiveEffectSettings.Calming != false)
            {
                MelonLogger.Warning("Invalid Calming in config. Adding default value (true).");
                loadedConfigState.EffectSettings.PositiveEffectSettings.Calming = configState.EffectSettings.PositiveEffectSettings.Calming;
                isConfigUpdated = true;
            }

            // Check if Calorie-Dense is a boolean
            if (loadedConfigState.EffectSettings.PositiveEffectSettings.Calorie_Dense != true && loadedConfigState.EffectSettings.PositiveEffectSettings.Calorie_Dense != false)
            {
                MelonLogger.Warning("Invalid Calorie_Dense in config. Adding default value (true).");
                loadedConfigState.EffectSettings.PositiveEffectSettings.Calorie_Dense = configState.EffectSettings.PositiveEffectSettings.Calorie_Dense;
                isConfigUpdated = true;
            }

            // Check if Electrifying is a boolean
            if (loadedConfigState.EffectSettings.PositiveEffectSettings.Electrifying != true && loadedConfigState.EffectSettings.PositiveEffectSettings.Electrifying != false)
            {
                MelonLogger.Warning("Invalid Electrifying in config. Adding default value (true).");
                loadedConfigState.EffectSettings.PositiveEffectSettings.Electrifying = configState.EffectSettings.PositiveEffectSettings.Electrifying;
                isConfigUpdated = true;
            }

            // Check if Energizing is a boolean
            if (loadedConfigState.EffectSettings.PositiveEffectSettings.Energizing != true && loadedConfigState.EffectSettings.PositiveEffectSettings.Energizing != false)
            {
                MelonLogger.Warning("Invalid Energizing in config. Adding default value (true).");
                loadedConfigState.EffectSettings.PositiveEffectSettings.Energizing = configState.EffectSettings.PositiveEffectSettings.Energizing;
                isConfigUpdated = true;
            }

            // Check if Euphoric is a boolean
            if (loadedConfigState.EffectSettings.PositiveEffectSettings.Euphoric != true && loadedConfigState.EffectSettings.PositiveEffectSettings.Euphoric != false)
            {
                MelonLogger.Warning("Invalid Euphoric in config. Adding default value (true).");
                loadedConfigState.EffectSettings.PositiveEffectSettings.Euphoric = configState.EffectSettings.PositiveEffectSettings.Euphoric;
                isConfigUpdated = true;
            }

            // Check if Focused is a boolean
            if (loadedConfigState.EffectSettings.PositiveEffectSettings.Focused != true && loadedConfigState.EffectSettings.PositiveEffectSettings.Focused != false)
            {
                MelonLogger.Warning("Invalid Focused in config. Adding default value (true).");
                loadedConfigState.EffectSettings.PositiveEffectSettings.Focused = configState.EffectSettings.PositiveEffectSettings.Focused;
                isConfigUpdated = true;
            }

            // Check if Munchies is a boolean
            if (loadedConfigState.EffectSettings.PositiveEffectSettings.Munchies != true && loadedConfigState.EffectSettings.PositiveEffectSettings.Munchies != false)
            {
                MelonLogger.Warning("Invalid Munchies in config. Adding default value (true).");
                loadedConfigState.EffectSettings.PositiveEffectSettings.Munchies = configState.EffectSettings.PositiveEffectSettings.Munchies;
                isConfigUpdated = true;
            }

            // Check if Refreshing is a boolean
            if (loadedConfigState.EffectSettings.PositiveEffectSettings.Refreshing != true && loadedConfigState.EffectSettings.PositiveEffectSettings.Refreshing != false)
            {
                MelonLogger.Warning("Invalid Refreshing in config. Adding default value (true).");
                loadedConfigState.EffectSettings.PositiveEffectSettings.Refreshing = configState.EffectSettings.PositiveEffectSettings.Refreshing;
                isConfigUpdated = true;
            }

            // Check if Sneaky is a boolean
            if (loadedConfigState.EffectSettings.PositiveEffectSettings.Sneaky != true && loadedConfigState.EffectSettings.PositiveEffectSettings.Sneaky != false)
            {
                MelonLogger.Warning("Invalid Sneaky in config. Adding default value (true).");
                loadedConfigState.EffectSettings.PositiveEffectSettings.Sneaky = configState.EffectSettings.PositiveEffectSettings.Sneaky;
                isConfigUpdated = true;
            }
        }

        private static void ValidateNegativeEffects(ConfigState loadedConfigState, ConfigState configState, ref bool isConfigUpdated)
        {
            // Check if Balding is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Balding != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Balding != false)
            {
                MelonLogger.Warning("Invalid Balding in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Balding = configState.EffectSettings.NegativeEffectSettings.Balding;
                isConfigUpdated = true;
            }

            // Check if Bright-Eyed is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Bright_Eyed != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Bright_Eyed != false)
            {
                MelonLogger.Warning("Invalid Bright_Eyed in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Bright_Eyed = configState.EffectSettings.NegativeEffectSettings.Bright_Eyed;
                isConfigUpdated = true;
            }

            // Check if Calming is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Calming != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Calming != false)
            {
                MelonLogger.Warning("Invalid Calming in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Calming = configState.EffectSettings.NegativeEffectSettings.Calming;
                isConfigUpdated = true;
            }

            // Check if Calorie-Dense is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Calorie_Dense != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Calorie_Dense != false)
            {
                MelonLogger.Warning("Invalid Calorie_Dense in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Calorie_Dense = configState.EffectSettings.NegativeEffectSettings.Calorie_Dense;
                isConfigUpdated = true;
            }

            // Check if Cyclopean is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Cyclopean != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Cyclopean != false)
            {
                MelonLogger.Warning("Invalid Cyclopean in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Cyclopean = configState.EffectSettings.NegativeEffectSettings.Cyclopean;
                isConfigUpdated = true;
            }

            // Check if Disorienting is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Disorienting != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Disorienting != false)
            {
                MelonLogger.Warning("Invalid Disorienting in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Disorienting = configState.EffectSettings.NegativeEffectSettings.Disorienting;
                isConfigUpdated = true;
            }

            // Check if Electrifying is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Electrifying != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Electrifying != false)
            {
                MelonLogger.Warning("Invalid Electrifying in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Electrifying = configState.EffectSettings.NegativeEffectSettings.Electrifying;
                isConfigUpdated = true;
            }

            // Check if Explosive is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Explosive != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Explosive != false)
            {
                MelonLogger.Warning("Invalid Explosive in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Explosive = configState.EffectSettings.NegativeEffectSettings.Explosive;
                isConfigUpdated = true;
            }

            // Check if Foggy is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Foggy != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Foggy != false)
            {
                MelonLogger.Warning("Invalid Foggy in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Foggy = configState.EffectSettings.NegativeEffectSettings.Foggy;
                isConfigUpdated = true;
            }

            // Check if Gingeritis is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Gingeritis != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Gingeritis != false)
            {
                MelonLogger.Warning("Invalid Gingeritis in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Gingeritis = configState.EffectSettings.NegativeEffectSettings.Gingeritis;
                isConfigUpdated = true;
            }

            // Check if Glowing is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Glowing != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Glowing != false)
            {
                MelonLogger.Warning("Invalid Glowing in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Glowing = configState.EffectSettings.NegativeEffectSettings.Glowing;
                isConfigUpdated = true;
            }

            // Check if Jennerising is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Jennerising != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Jennerising != false)
            {
                MelonLogger.Warning("Invalid Jennerising in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Jennerising = configState.EffectSettings.NegativeEffectSettings.Jennerising;
                isConfigUpdated = true;
            }

            // Check if Laxative is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Laxative != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Laxative != false)
            {
                MelonLogger.Warning("Invalid Laxative in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Laxative = configState.EffectSettings.NegativeEffectSettings.Laxative;
                isConfigUpdated = true;
            }

            // Check if Lethal is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Lethal != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Lethal != false)
            {
                MelonLogger.Warning("Invalid Lethal in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Lethal = configState.EffectSettings.NegativeEffectSettings.Lethal;
                isConfigUpdated = true;
            }

            // Check if Long-Faced is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Long_Faced != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Long_Faced != false)
            {
                MelonLogger.Warning("Invalid Long_Faced in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Long_Faced = configState.EffectSettings.NegativeEffectSettings.Long_Faced;
                isConfigUpdated = true;
            }

            // Check if Paranoia is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Paranoia != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Paranoia != false)
            {
                MelonLogger.Warning("Invalid Paranoia in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Paranoia = configState.EffectSettings.NegativeEffectSettings.Paranoia;
                isConfigUpdated = true;
            }

            // Check if Schizophrenic is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Schizophrenic != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Schizophrenic != false)
            {
                MelonLogger.Warning("Invalid Schizophrenic in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Schizophrenic = configState.EffectSettings.NegativeEffectSettings.Schizophrenic;
                isConfigUpdated = true;
            }

            // Check if Sedating is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Sedating != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Sedating != false)
            {
                MelonLogger.Warning("Invalid Sedating in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Sedating = configState.EffectSettings.NegativeEffectSettings.Sedating;
                isConfigUpdated = true;
            }

            // Check if Seizure-Inducing is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Seizure_Inducing != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Seizure_Inducing != false)
            {
                MelonLogger.Warning("Invalid Seizure-Inducing in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Seizure_Inducing = configState.EffectSettings.NegativeEffectSettings.Seizure_Inducing;
                isConfigUpdated = true;
            }

            // Check if Shrinking is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Shrinking != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Shrinking != false)
            {
                MelonLogger.Warning("Invalid Shrinking in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Shrinking = configState.EffectSettings.NegativeEffectSettings.Shrinking;
                isConfigUpdated = true;
            }

            // Check if Slippery is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Slippery != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Slippery != false)
            {
                MelonLogger.Warning("Invalid Slippery in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Slippery = configState.EffectSettings.NegativeEffectSettings.Slippery;
                isConfigUpdated = true;
            }

            // Check if Smelly is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Smelly != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Smelly != false)
            {
                MelonLogger.Warning("Invalid Smelly in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Smelly = configState.EffectSettings.NegativeEffectSettings.Smelly;
                isConfigUpdated = true;
            }

            // Check if Spicy is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Spicy != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Spicy != false)
            {
                MelonLogger.Warning("Invalid Spicy in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Spicy = configState.EffectSettings.NegativeEffectSettings.Spicy;
                isConfigUpdated = true;
            }

            // Check if Thought-Provoking is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Thought_Provoking != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Thought_Provoking != false)
            {
                MelonLogger.Warning("Invalid Thought_Provoking in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Thought_Provoking = configState.EffectSettings.NegativeEffectSettings.Thought_Provoking;
                isConfigUpdated = true;
            }

            // Check if Toxic is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Toxic != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Toxic != false)
            {
                MelonLogger.Warning("Invalid Toxic in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Toxic = configState.EffectSettings.NegativeEffectSettings.Toxic;
                isConfigUpdated = true;
            }

            // Check if Tropic Thunder is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Tropic_Thunder != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Tropic_Thunder != false)
            {
                MelonLogger.Warning("Invalid Tropic_Thunder in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Tropic_Thunder = configState.EffectSettings.NegativeEffectSettings.Tropic_Thunder;
                isConfigUpdated = true;
            }

            // Check if Zombifying is a boolean
            if (loadedConfigState.EffectSettings.NegativeEffectSettings.Zombifying != true && loadedConfigState.EffectSettings.NegativeEffectSettings.Zombifying != false)
            {
                MelonLogger.Warning("Invalid Zombifying in config. Adding default value (true).");
                loadedConfigState.EffectSettings.NegativeEffectSettings.Zombifying = configState.EffectSettings.NegativeEffectSettings.Zombifying;
                isConfigUpdated = true;
            }
        }

        private static void ValidateEventSettings(ConfigState loadedConfigState, ConfigState configState, ref bool isConfigUpdated)
        {
            // Check if Enable Get Arrested Event is a boolean
            if (loadedConfigState.ArrestedEventSettings.Enable_GetArrested_Event != true && loadedConfigState.ArrestedEventSettings.Enable_GetArrested_Event != false)
            {
                MelonLogger.Warning("Invalid Enable_Arrested_Event in config. Adding default value (true).");
                loadedConfigState.ArrestedEventSettings.Enable_GetArrested_Event = configState.ArrestedEventSettings.Enable_GetArrested_Event;
                isConfigUpdated = true;
            }

            // Check if Enable_GetArrested_Event_SaveSpaces is a boolean
            if (loadedConfigState.ArrestedEventSettings.Enable_GetArrested_Event_SaveSpaces != true && loadedConfigState.ArrestedEventSettings.Enable_GetArrested_Event_SaveSpaces != false)
            {
                MelonLogger.Warning("Invalid Enable_GetArrested_Event_SaveSpaces in config. Adding default value (true).");
                loadedConfigState.ArrestedEventSettings.Enable_GetArrested_Event_SaveSpaces = configState.ArrestedEventSettings.Enable_GetArrested_Event_SaveSpaces;
                isConfigUpdated = true;
            }

            // Check if GetArrested_Event_Probability is a float
            if (loadedConfigState.ArrestedEventSettings.GetArrested_Event_Probability > 100f || loadedConfigState.ArrestedEventSettings.GetArrested_Event_Probability < 1f)
            {
                MelonLogger.Warning("Invalid GetArrested_Event_Probability in config. Needs to be between 1 and 100. Adding default value (10%).");
                loadedConfigState.ArrestedEventSettings.GetArrested_Event_Probability = configState.ArrestedEventSettings.GetArrested_Event_Probability;
                isConfigUpdated = true;
            }




            // Check if Enable Get Murdered Event is a boolean
            if (loadedConfigState.MurderedEventSettings.Enable_GetMurdered_Event != true && loadedConfigState.MurderedEventSettings.Enable_GetMurdered_Event != false)
            {
                MelonLogger.Warning("Invalid Enable_GetMurdered_Event in config. Adding default value (true).");
                loadedConfigState.MurderedEventSettings.Enable_GetMurdered_Event = configState.MurderedEventSettings.Enable_GetMurdered_Event;
                isConfigUpdated = true;
            }

            // Check if Enable_GetMurdered_Event_SaveSpaces is a boolean
            if (loadedConfigState.MurderedEventSettings.Enable_GetMurdered_Event_SaveSpaces != true && loadedConfigState.MurderedEventSettings.Enable_GetMurdered_Event_SaveSpaces != false)
            {
                MelonLogger.Warning("Invalid Enable_GetMurdered_Event_SaveSpaces in config. Adding default value (true).");
                loadedConfigState.MurderedEventSettings.Enable_GetMurdered_Event_SaveSpaces = configState.MurderedEventSettings.Enable_GetMurdered_Event_SaveSpaces;
                isConfigUpdated = true;
            }

            // Check if Allow_GetMurdered_Event_Respawning is a boolean
            if (loadedConfigState.MurderedEventSettings.Allow_GetMurdered_Event_Respawning != true && loadedConfigState.MurderedEventSettings.Allow_GetMurdered_Event_Respawning != false)
            {
                MelonLogger.Warning("Invalid Allow_GetMurdered_Event_Respawning in config. Adding default value (true).");
                loadedConfigState.MurderedEventSettings.Allow_GetMurdered_Event_Respawning = configState.MurderedEventSettings.Allow_GetMurdered_Event_Respawning;
                isConfigUpdated = true;
            }

            // Check if GetMurdered_Event_Probability is a float
            if (loadedConfigState.MurderedEventSettings.GetMurdered_Event_Probability > 100f || loadedConfigState.MurderedEventSettings.GetMurdered_Event_Probability < 1f)
            {
                MelonLogger.Warning("Invalid GetMurdered_Event_Probability in config. Needs to be between 1 and 100. Adding default value (10%).");
                loadedConfigState.MurderedEventSettings.GetMurdered_Event_Probability = configState.MurderedEventSettings.GetMurdered_Event_Probability;
                isConfigUpdated = true;
            }

        }

        public static bool GetNegativePropertyValue(ConfigState config, string propertyName)
        {
            if (config == null || config.EffectSettings == null || config.EffectSettings.NegativeEffectSettings == null)
            {
                MelonLogger.Warning("Config or NegativeEffectSettings is null. Cannot get property value.");
                return false;
            }

            propertyName = propertyName.Replace(" ", "").ToLower();
            switch (propertyName)
            {
                case "balding":
                    return config.EffectSettings.NegativeEffectSettings.Balding;
                case "brighteyed":
                case "bright_eyed":
                    return config.EffectSettings.NegativeEffectSettings.Bright_Eyed;
                case "calming":
                    return config.EffectSettings.NegativeEffectSettings.Calming;
                case "caloriedense":
                case "calorie_dense":
                    return config.EffectSettings.NegativeEffectSettings.Calorie_Dense;
                case "cyclopean":
                    return config.EffectSettings.NegativeEffectSettings.Cyclopean;
                case "disorienting":
                    return config.EffectSettings.NegativeEffectSettings.Disorienting;
                case "electrifying":
                    return config.EffectSettings.NegativeEffectSettings.Electrifying;
                case "explosive":
                    return config.EffectSettings.NegativeEffectSettings.Explosive;
                case "foggy":
                    return config.EffectSettings.NegativeEffectSettings.Foggy;
                case "gingeritis":
                    return config.EffectSettings.NegativeEffectSettings.Gingeritis;
                case "glowie":
                case "glowing":
                    return config.EffectSettings.NegativeEffectSettings.Glowing;
                case "jennerising":
                    return config.EffectSettings.NegativeEffectSettings.Jennerising;
                case "laxative":
                    return config.EffectSettings.NegativeEffectSettings.Laxative;
                case "lethal":
                    return config.EffectSettings.NegativeEffectSettings.Lethal;
                case "longfaced":
                case "long_faced":
                    return config.EffectSettings.NegativeEffectSettings.Long_Faced;
                case "paranoia":
                    return config.EffectSettings.NegativeEffectSettings.Paranoia;
                case "sedating":
                    return config.EffectSettings.NegativeEffectSettings.Sedating;
                case "seizureinducing":
                case "seizure_inducing":
                    return config.EffectSettings.NegativeEffectSettings.Seizure_Inducing;
                case "schizophrenic":
                    return config.EffectSettings.NegativeEffectSettings.Schizophrenic;
                case "shrinking":
                    return config.EffectSettings.NegativeEffectSettings.Shrinking;
                case "slippery":
                    return config.EffectSettings.NegativeEffectSettings.Slippery;
                case "smelly":
                    return config.EffectSettings.NegativeEffectSettings.Smelly;
                case "spicy":
                    return config.EffectSettings.NegativeEffectSettings.Spicy;
                case "thoughtprovoking":
                case "thought_provoking":
                    return config.EffectSettings.NegativeEffectSettings.Thought_Provoking;
                case "toxic":
                    return config.EffectSettings.NegativeEffectSettings.Toxic;
                case "tropicthunder":
                case "tropic_thunder":
                    return config.EffectSettings.NegativeEffectSettings.Tropic_Thunder;
                case "zombifying":
                    return config.EffectSettings.NegativeEffectSettings.Zombifying;
                default:
                    MelonLogger.Warning($"Property '{propertyName}' not found. Returning false.");
                    return false; // Default value if the property is not found
            }
        }

        public static bool GetPositivePropertyValue(ConfigState config, string propertyName)
        {
            if (config == null || config.EffectSettings == null || config.EffectSettings.PositiveEffectSettings == null)
            {
                MelonLogger.Warning("Config or PositiveEffectSettings is null. Cannot get property value.");
                return false;
            }

            propertyName = propertyName.Replace(" ", "").ToLower();

            switch (propertyName)
            {
                case "anti_gravity":
                case "antigravity":
                    return config.EffectSettings.PositiveEffectSettings.Anti_Gravity;
                case "athletic":
                    return config.EffectSettings.PositiveEffectSettings.Athletic;
                case "brighteyed":
                case "bright_eyed":
                    return config.EffectSettings.PositiveEffectSettings.Bright_Eyed;
                case "calming":
                    return config.EffectSettings.PositiveEffectSettings.Calming;
                case "caloriedense":
                case "calorie_dense":
                    return config.EffectSettings.PositiveEffectSettings.Calorie_Dense;
                case "electrifying":
                    return config.EffectSettings.PositiveEffectSettings.Electrifying;
                case "energizing":
                    return config.EffectSettings.PositiveEffectSettings.Energizing;
                case "euphoric":
                    return config.EffectSettings.PositiveEffectSettings.Euphoric;
                case "focused":
                    return config.EffectSettings.PositiveEffectSettings.Focused;
                case "munchies":
                    return config.EffectSettings.PositiveEffectSettings.Munchies;
                case "refreshing":
                    return config.EffectSettings.PositiveEffectSettings.Refreshing;
                case "sneaky":
                    return config.EffectSettings.PositiveEffectSettings.Sneaky;
                default:
                    MelonLogger.Warning($"Property '{propertyName}' not found. Returning false.");
                    return false; // Default value if the property is not found
            }
        }

        public static List<string> GetPositiveEffectNames()
        {
            return new List<string>
            {
            "Anti Gravity",
            "Athletic",
            "Bright Eyed",
            "Calming",
            "Calorie Dense",
            "Electrifying",
            "Energizing",
            "Euphoric",
            "Focused",
            "Munchies",
            "Refreshing",
            "Sneaky"
            };
        }

        public static List<string> GetNegativeEffectNames()
        {
            return new List<string>
            {
            "Balding",
            "Bright Eyed",
            "Calming",
            "Calorie Dense",
            "Cyclopean",
            "Disorienting",
            "Electrifying",
            "Explosive",
            "Foggy",
            "Gingeritis",
            "Glowing",
            "Jennerising",
            "Laxative",
            "Lethal",
            "Long Faced",
            "Paranoia",
            "Schizophrenic",
            "Sedating",
            "Seizure Inducing",
            "Shrinking",
            "Slippery",
            "Smelly",
            "Spicy",
            "Thought Provoking",
            "Toxic",
            "Tropic Thunder",
            "Zombifying"
            };
        }

        public static List<string> GetLethalEffectNames()
        {
            return new List<string>
            {
            "Explosive",
            "Lethal"
            };
        }

        public static List<string> GetUselessEffectNames()
        {
            return new List<string>
            {
            "Munchies",
            "Refreshing"
            };
        }
    }
}