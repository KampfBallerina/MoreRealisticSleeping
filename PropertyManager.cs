using Il2CppScheduleOne;
using Il2CppScheduleOne.PlayerScripts;
using Il2CppScheduleOne.Properties;
using MelonLoader;
using MoreRealisticSleeping;
using MoreRealisticSleeping.Config;
using UnityEngine;
using System.Collections;
using Il2CppLiquidVolumeFX;
using MelonLoader.Utils;
using Il2CppScheduleOne.Networking;
using Il2CppFluffyUnderware.DevTools.Extensions;
using Il2CppJetBrains.Annotations;

public class PropertyManager
{
    private readonly Dictionary<string, Action<Player>> negativeProperties;
    private readonly Dictionary<string, Action<Player>> positiveProperties;
    public string appliedEffect = null;
    public PropertyManager()

    {
        positiveProperties = new Dictionary<string, Action<Player>>
        {
            { "Anti_Gravity", player => new AntiGravity().ApplyToPlayer(player) },
            { "Athletic", player => new Athletic().ApplyToPlayer(player) },
            { "Bright_Eyed", player => new BrightEyed().ApplyToPlayer(player) },
            { "Calming", player => new Calming().ApplyToPlayer(player) },
            { "Calorie_Dense", player => new CalorieDense().ApplyToPlayer(player) },
            { "Electrifying", player => new Electrifying().ApplyToPlayer(player) },
            { "Energizing", player => new Energizing().ApplyToPlayer(player) },
            { "Euphoric", player => new Euphoric().ApplyToPlayer(player) },
            { "Focused", player => new Focused().ApplyToPlayer(player) },
            { "Munchies", player => new Munchies().ApplyToPlayer(player) },
            { "Refreshing", player => new Refreshing().ApplyToPlayer(player) },
            { "Sneaky", player => new Sneaky().ApplyToPlayer(player) },
        };

        negativeProperties = new Dictionary<string, Action<Player>>
        {
            { "Balding", player => new Balding().ApplyToPlayer(player) },
            { "Bright_Eyed", player => new BrightEyed().ApplyToPlayer(player) },
            { "Calming", player => new Calming().ApplyToPlayer(player) },
            { "Calorie_Dense", player => new CalorieDense().ApplyToPlayer(player) },
            { "Cyclopean", player => new Cyclopean().ApplyToPlayer(player) },
            { "Disorienting", player => new Disorienting().ApplyToPlayer(player) },
            { "Electrifying", player => new Electrifying().ApplyToPlayer(player) },
            { "Explosive", player => new Explosive().ApplyToPlayer(player) },
            { "Foggy", player => new Foggy().ApplyToPlayer(player) },
            { "Gingeritis", player => new Gingeritis().ApplyToPlayer(player) },
            { "Glowing", player => new Glowie().ApplyToPlayer(player) },
            { "Laxative", player => new Laxative().ApplyToPlayer(player) },
            { "Long_Faced", player => new LongFaced().ApplyToPlayer(player) },
            { "Paranoia", player => new Paranoia().ApplyToPlayer(player) },
            { "Sedating", player => new Sedating().ApplyToPlayer(player) },
            { "Seizure_Inducing", player => new Seizure().ApplyToPlayer(player) },
            { "Shrinking", player => new Shrinking().ApplyToPlayer(player) },
            { "Slippery", player => new Slippery().ApplyToPlayer(player) },
            { "Smelly", player => new Smelly().ApplyToPlayer(player) },
            { "Spicy", player => new Spicy().ApplyToPlayer(player) },
            { "Lethal", player => new Lethal().ApplyToPlayer(player) },
            { "Jennerising", player => new Jennerising().ApplyToPlayer(player) },
            { "Schizophrenic", player => new Schizophrenic().ApplyToPlayer(player) },
            { "Thought_Provoking", player => new ThoughtProvoking().ApplyToPlayer(player) },
            { "Toxic", player => new Toxic().ApplyToPlayer(player) },
            { "Tropic_Thunder", player => new TropicThunder().ApplyToPlayer(player) },
            { "Zombifying", player => new Zombifying().ApplyToPlayer(player) }
        };
    }

    public void ApplyNegativePropertyToPlayer(Player player, string propertyName = null)
    {
        if (player == null)
        {
            MelonLogger.Warning("Player is null. Cannot apply property.");
            return;
        }

        if (MRSCore.Instance.config == null || MRSCore.Instance.config.EffectSettings == null)
        {
            MelonLogger.Warning("Config or EffectSettings is null. Cannot apply property.");
            return;
        }

        try
        {
            // Wenn keine Property angegeben ist, wähle eine zufällige aktivierte Property
            if (string.IsNullOrEmpty(propertyName))
            {
                var enabledProperties = new List<string>();

                foreach (string property in negativeProperties.Keys)
                {
                    // Verwende GetNegativePropertyValue, um den Wert der Property zu prüfen
                    if (ConfigManager.GetNegativePropertyValue(MRSCore.Instance.config, property))
                    {
                        enabledProperties.Add(property);
                    }
                }

                if (enabledProperties.Count == 0)
                {
                    MelonLogger.Warning("No enabled negative properties found in the config.");
                    return;
                }

                // Wähle eine zufällige Property aus den aktivierten Properties
                var random = new System.Random();
                propertyName = enabledProperties[random.Next(enabledProperties.Count)];
                // MelonLogger.Msg($"No property specified. Randomly selected '{propertyName}'.");
            }

            // Überprüfen, ob die Property in der Config aktiviert ist
            if (!ConfigManager.GetNegativePropertyValue(MRSCore.Instance.config, propertyName))
            {
                MelonLogger.Msg($"Property '{propertyName}' is disabled in the config. Skipping.");
                return;
            }

            // Überprüfen, ob bereits eine Property angewendet wurde
            if (appliedEffect != null)
            {
                //MelonLogger.Msg($"Property '{appliedEffect}' is already applied to player: {player.name}. Skipping.");
                return;
            }

            // Wende die Property mit AddPropertyToPlayer an
            MelonCoroutines.Start(AddPropertyToPlayer(player, propertyName));
            MelonLogger.Msg($"Applied '{propertyName}' to player: {player.name}");
            appliedEffect = propertyName; // Store the applied effect

            float duration = MRSCore.Instance.config.SleepSettings.Negative_Effects_Duration;
            if (duration <= 0f || float.IsNaN(duration))
            {
                duration = 60f;
            }

            if (MRSCore.Instance.config.SleepSettings.Enable_Effect_Notifications)
            {
                string displayName = FormatPropertyName(propertyName);
                Sprite sprite = LoadSpriteFromUserData(displayName);
                MRSCore.Instance.notificationsManager.SendNotification("Negative Effect", $"{displayName}", sprite, duration, true);
            }

            removeEffectCoroutine = (Coroutine)MelonCoroutines.Start(RemovePropertyFromPlayerAfterTime(player, duration, appliedEffect));
        }
        catch (Exception ex)
        {
            MelonLogger.Error("Error while applying property: " + ex.Message + "\n" + ex.StackTrace);
        }
    }

    public void ApplyPositivePropertyToPlayer(Player player, string propertyName = null)
    {
        if (player == null)
        {
            MelonLogger.Warning("Player is null. Cannot apply property.");
            return;
        }

        if (MRSCore.Instance.config == null || MRSCore.Instance.config.EffectSettings == null)
        {
            MelonLogger.Warning("Config or EffectSettings is null. Cannot apply property.");
            return;
        }

        try
        {
            // Wenn keine Property angegeben ist, wähle eine zufällige aktivierte Property
            if (string.IsNullOrEmpty(propertyName))
            {
                var enabledProperties = new List<string>();

                foreach (string property in positiveProperties.Keys)
                {
                    // Verwende GetPositivePropertyValue, um den Wert der Property zu prüfen
                    if (ConfigManager.GetPositivePropertyValue(MRSCore.Instance.config, property))
                    {
                        enabledProperties.Add(property);
                    }
                }

                if (enabledProperties.Count == 0)
                {
                    MelonLogger.Warning("No enabled positive properties found in the config.");
                    return;
                }

                // Wähle eine zufällige Property aus den aktivierten Properties
                var random = new System.Random();
                propertyName = enabledProperties[random.Next(enabledProperties.Count)];
                // MelonLogger.Msg($"No property specified. Randomly selected '{propertyName}'.");
            }

            // Überprüfen, ob die Property in der Config aktiviert ist
            if (!ConfigManager.GetPositivePropertyValue(MRSCore.Instance.config, propertyName))
            {
                MelonLogger.Warning($"Property '{propertyName}' is disabled in the config. Skipping.");
                return;
            }

            // Überprüfen, ob bereits eine Property angewendet wurde
            if (appliedEffect != null)
            {
                MelonLogger.Warning($"Property '{appliedEffect}' is already applied to player: {player.name}. Skipping.");
                return;
            }

            // Wende die Property mit AddPropertyToPlayer an
            MelonCoroutines.Start(AddPropertyToPlayer(player, propertyName));
            appliedEffect = propertyName; // Store the applied effect

            float duration = MRSCore.Instance.config.SleepSettings.Positive_Effects_Duration;
            if (duration <= 0f || float.IsNaN(duration))
            {
                duration = 60f;
            }

            MelonLogger.Msg($"Applied {propertyName} to player: {player.name} for {duration} seconds.");
            if (MRSCore.Instance.config.SleepSettings.Enable_Effect_Notifications)
            {
                string displayName = FormatPropertyName(propertyName);
                Sprite sprite = LoadSpriteFromUserData(displayName);
                MRSCore.Instance.notificationsManager.SendNotification("Positive Effect", $"{displayName}", sprite, duration, true);
            }

            MelonCoroutines.Start(RemovePropertyFromPlayerAfterTime(player, duration, appliedEffect));
        }
        catch (Exception ex)
        {
            MelonLogger.Error("Error while applying property: " + ex.Message + "\n" + ex.StackTrace);
        }
    }


    public static string FormatPropertyName(string propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            return string.Empty;
        }

        // Ersetze Unterstriche durch Leerzeichen
        string formattedName = propertyName.Replace("_", " ");

        // Konvertiere den ersten Buchstaben jedes Wortes in Großbuchstaben
        formattedName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(formattedName.ToLower());

        return formattedName;
    }
    public IEnumerator RemovePropertyFromPlayerAfterTime(Player player, float timeInSeconds, string propertyName)
    {
        if (player == null)
        {
            MelonLogger.Warning("Player is null. Cannot remove property.");
            yield break;
        }
        //MelonLogger.Msg($"Coroutine started for player: {player.name}. Waiting for {timeInSeconds} seconds.");
        yield return new WaitForSeconds(timeInSeconds);
        appliedEffect = null;
        switch (propertyName.ToLower())
        {
            case "antigravitiy":
            case "anti_gravity":
                if (antiGravity == null) antiGravity = new AntiGravity();
                if (antiGravity != null) antiGravity.ClearFromPlayer(player);
                break;
            case "athletic":
                if (athletic == null) athletic = new Athletic();
                if (athletic != null) athletic.ClearFromPlayer(player);
                break;
            case "balding":
                if (balding == null) balding = new Balding();
                if (balding != null) balding.ClearFromPlayer(player);
                break;
            case "brighteyed":
            case "bright_eyed":
                if (brightEyed == null) brightEyed = new BrightEyed();
                if (brightEyed != null) brightEyed.ClearFromPlayer(player);
                break;
            case "calming":
                if (calming == null) calming = new Calming();
                if (calming != null) calming.ClearFromPlayer(player);
                break;
            case "caloriedense":
            case "calorie_dense":
                if (calorieDense == null) calorieDense = new CalorieDense();
                if (calorieDense != null) calorieDense.ClearFromPlayer(player);
                break;
            case "cyclopean":
                if (cyclopean == null) cyclopean = new Cyclopean();
                if (cyclopean != null) cyclopean.ClearFromPlayer(player);
                break;
            case "disorienting":
                if (disorienting == null) disorienting = new Disorienting();
                if (disorienting != null) disorienting.ClearFromPlayer(player);
                break;
            case "electrifying":
                if (electrifying == null) electrifying = new Electrifying();
                if (electrifying != null) electrifying.ClearFromPlayer(player);
                break;
            case "energizing":
                if (energizing == null) energizing = new Energizing();
                if (energizing != null) energizing.ClearFromPlayer(player);
                break;
            case "euphoric":
                if (euphoric == null) euphoric = new Euphoric();
                if (euphoric != null) euphoric.ClearFromPlayer(player);
                break;
            case "explosive":
                if (explosive == null) explosive = new Explosive();
                if (explosive != null) explosive.ClearFromPlayer(player);
                break;
            case "foggy":
                if (foggy == null) foggy = new Foggy();
                if (foggy != null) foggy.ClearFromPlayer(player);
                break;
            case "focused":
                if (focused == null) focused = new Focused();
                if (focused != null) focused.ClearFromPlayer(player);
                break;
            case "gingeritis":
                if (gingeritis == null) gingeritis = new Gingeritis();
                if (gingeritis != null) gingeritis.ClearFromPlayer(player);
                break;
            case "glowing":
            case "glowie":
                if (glowie == null) glowie = new Glowie();
                if (glowie != null) glowie.ClearFromPlayer(player);
                break;
            case "jennerising":
                if (jennerising == null) jennerising = new Jennerising();
                if (jennerising != null) jennerising.ClearFromPlayer(player);
                break;
            case "laxative":
                if (laxative == null) laxative = new Laxative();
                if (laxative != null) laxative.ClearFromPlayer(player);
                break;
            case "lethal":
                if (lethal == null) lethal = new Lethal();
                if (lethal != null) lethal.ClearFromPlayer(player);
                break;
            case "longfaced":
            case "long_faced":
                if (longFaced == null) longFaced = new LongFaced();
                if (longFaced != null) longFaced.ClearFromPlayer(player);
                break;
            case "munchies":
                if (munchies == null) munchies = new Munchies();
                if (munchies != null) munchies.ClearFromPlayer(player);
                break;
            case "paranoia":
                if (paranoia == null) paranoia = new Paranoia();
                if (paranoia != null) paranoia.ClearFromPlayer(player);
                break;
            case "refreshing":
                if (refreshing == null) refreshing = new Refreshing();
                if (refreshing != null) refreshing.ClearFromPlayer(player);
                break;
            case "schizophrenic":
                if (schizophrenic == null) schizophrenic = new Schizophrenic();
                if (schizophrenic != null) schizophrenic.ClearFromPlayer(player);
                break;
            case "sedating":
                if (sedating == null) sedating = new Sedating();
                if (sedating != null) sedating.ClearFromPlayer(player);
                break;
            case "seizure_inducing":
            case "seizureinducing":
            case "seizure":
                if (seizure == null) seizure = new Seizure();
                if (seizure != null) seizure.ClearFromPlayer(player);
                break;
            case "shrinking":
                if (shrinking == null) shrinking = new Shrinking();
                if (shrinking != null) shrinking.ClearFromPlayer(player);
                break;
            case "slippery":
                if (slippery == null) slippery = new Slippery();
                if (slippery != null) slippery.ClearFromPlayer(player);
                break;
            case "smelly":
                if (smelly == null) smelly = new Smelly();
                if (smelly != null) smelly.ClearFromPlayer(player);
                break;
            case "sneaky":
                if (sneaky == null) sneaky = new Sneaky();
                if (sneaky != null) sneaky.ClearFromPlayer(player);
                break;
            case "spicy":
                if (spicy == null) spicy = new Spicy();
                if (spicy != null) spicy.ClearFromPlayer(player);
                break;
            case "thoughtprovoking":
            case "thought_provoking":
                if (thoughtProvoking == null) thoughtProvoking = new ThoughtProvoking();
                if (thoughtProvoking != null) thoughtProvoking.ClearFromPlayer(player);
                break;
            case "toxic":
                if (toxic == null) toxic = new Toxic();
                if (toxic != null) toxic.ClearFromPlayer(player);
                break;
            case "tropicthunder":
            case "tropic_thunder":
                if (tropicThunder == null) tropicThunder = new TropicThunder();
                if (tropicThunder != null) tropicThunder.ClearFromPlayer(player);
                break;
            case "zombifying":
                if (zombifying == null) zombifying = new Zombifying();
                if (zombifying != null) zombifying.ClearFromPlayer(player);
                break;
            default:
                MelonLogger.Warning($"Property '{propertyName}' not found in the available properties.");
                break;
        }
    }

    public IEnumerator AddPropertyToPlayer(Player player, string propertyName)
    {
        if (player == null)
        {
            MelonLogger.Warning("Player is null. Cannot remove property.");
            yield break;
        }

        if (propertyName == null)
        {
            MelonLogger.Warning("Property name is null. Cannot remove property.");
            yield break;
        }

        switch (propertyName.ToLower())
        {
            case "antigravitiy":
            case "anti_gravity":
                if (antiGravity == null) antiGravity = new AntiGravity();
                if (antiGravity != null) antiGravity.ApplyToPlayer(player);
                break;
            case "athletic":
                if (athletic == null) athletic = new Athletic();
                if (athletic != null) athletic.ApplyToPlayer(player);
                break;
            case "balding":
                if (balding == null) balding = new Balding();
                if (balding != null) balding.ApplyToPlayer(player);
                break;
            case "brighteyed":
            case "bright_eyed":
                if (brightEyed == null) brightEyed = new BrightEyed();
                if (brightEyed != null) brightEyed.ApplyToPlayer(player);
                break;
            case "calming":
                if (calming == null) calming = new Calming();
                if (calming != null) calming.ApplyToPlayer(player);
                break;
            case "caloriedense":
            case "calorie_dense":
                if (calorieDense == null) calorieDense = new CalorieDense();
                if (calorieDense != null) calorieDense.ApplyToPlayer(player);
                break;
            case "cyclopean":
                if (cyclopean == null) cyclopean = new Cyclopean();
                if (cyclopean != null) cyclopean.ApplyToPlayer(player);
                break;
            case "disorienting":
                if (disorienting == null) disorienting = new Disorienting();
                if (disorienting != null) disorienting.ApplyToPlayer(player);
                break;
            case "electrifying":
                if (electrifying == null) electrifying = new Electrifying();
                if (electrifying != null) electrifying.ApplyToPlayer(player);
                break;
            case "energizing":
                if (energizing == null) energizing = new Energizing();
                if (energizing != null) energizing.ApplyToPlayer(player);
                break;
            case "euphoric":
                if (euphoric == null) euphoric = new Euphoric();
                if (euphoric != null) euphoric.ApplyToPlayer(player);
                break;
            case "explosive":
                if (explosive == null) explosive = new Explosive();
                if (explosive != null) explosive.ApplyToPlayer(player);
                break;
            case "foggy":
                if (foggy == null) foggy = new Foggy();
                if (foggy != null) foggy.ApplyToPlayer(player);
                break;
            case "focused":
                if (focused == null) focused = new Focused();
                if (focused != null) focused.ApplyToPlayer(player);
                break;
            case "gingeritis":
                if (gingeritis == null) gingeritis = new Gingeritis();
                if (gingeritis != null) gingeritis.ApplyToPlayer(player);
                break;
            case "glowing":
            case "glowie":
                if (glowie == null) glowie = new Glowie();
                if (glowie != null) glowie.ApplyToPlayer(player);
                break;
            case "jennerising":
                if (jennerising == null) jennerising = new Jennerising();
                if (jennerising != null) jennerising.ApplyToPlayer(player);
                break;
            case "laxative":
                if (laxative == null) laxative = new Laxative();
                if (laxative != null) laxative.ApplyToPlayer(player);
                break;
            case "lethal":
                if (lethal == null) lethal = new Lethal();
                if (lethal != null) lethal.ApplyToPlayer(player);
                break;
            case "longfaced":
            case "long_faced":
                if (longFaced == null) longFaced = new LongFaced();
                if (longFaced != null) longFaced.ApplyToPlayer(player);
                break;
            case "munchies":
                if (munchies == null) munchies = new Munchies();
                if (munchies != null) munchies.ApplyToPlayer(player);
                break;
            case "paranoia":
                if (paranoia == null) paranoia = new Paranoia();
                if (paranoia != null) paranoia.ApplyToPlayer(player);
                break;
            case "refreshing":
                if (refreshing == null) refreshing = new Refreshing();
                if (refreshing != null) refreshing.ApplyToPlayer(player);
                break;
            case "schizophrenic":
                if (schizophrenic == null) schizophrenic = new Schizophrenic();
                if (schizophrenic != null) schizophrenic.ApplyToPlayer(player);
                break;
            case "sedating":
                if (sedating == null) sedating = new Sedating();
                if (sedating != null) sedating.ApplyToPlayer(player);
                break;
            case "seizure_inducing":
            case "seizureinducing":
            case "seizure":
                if (seizure == null) seizure = new Seizure();
                if (seizure != null) seizure.ApplyToPlayer(player);
                break;
            case "shrinking":
                if (shrinking == null) shrinking = new Shrinking();
                if (shrinking != null) shrinking.ApplyToPlayer(player);
                break;
            case "slippery":
                if (slippery == null) slippery = new Slippery();
                if (slippery != null) slippery.ApplyToPlayer(player);
                break;
            case "smelly":
                if (smelly == null) smelly = new Smelly();
                if (smelly != null) smelly.ApplyToPlayer(player);
                break;
            case "sneaky":
                if (sneaky == null) sneaky = new Sneaky();
                if (sneaky != null) sneaky.ApplyToPlayer(player);
                break;
            case "spicy":
                if (spicy == null) spicy = new Spicy();
                if (spicy != null) spicy.ApplyToPlayer(player);
                break;
            case "thoughtprovoking":
            case "thought_provoking":
                if (thoughtProvoking == null) thoughtProvoking = new ThoughtProvoking();
                if (thoughtProvoking != null) thoughtProvoking.ApplyToPlayer(player);
                break;
            case "toxic":
                if (toxic == null) toxic = new Toxic();
                if (toxic != null) toxic.ApplyToPlayer(player);
                break;
            case "tropicthunder":
            case "tropic_thunder":
                if (tropicThunder == null) tropicThunder = new TropicThunder();
                if (tropicThunder != null) tropicThunder.ApplyToPlayer(player);
                break;
            case "zombifying":
                if (zombifying == null) zombifying = new Zombifying();
                if (zombifying != null) zombifying.ApplyToPlayer(player);
                break;
            default:
                MelonLogger.Warning($"Property '{propertyName}' not found in the available properties.");
                break;
        }
    }
    static Sprite LoadSpriteFromUserData(string fileName)
    {
        string imagePath = Path.Combine(EffectImagesFolder, $"{fileName}.png");
        if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
        {
            byte[] imageData = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(imageData))
            {
                Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                return newSprite;
            }
        }
        else
        {
            if (!appIconSprite)
            {
                if (!string.IsNullOrEmpty(FilePath) && File.Exists(FilePath))
                {
                    byte[] imageData = File.ReadAllBytes(FilePath);
                    Texture2D texture = new Texture2D(2, 2);
                    if (texture.LoadImage(imageData))
                    {
                        Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                        appIconSprite = newSprite;
                        return appIconSprite;
                    }
                }
            }
            else
            {
                return appIconSprite;
            }
        }
        return null;
    }

    public AntiGravity antiGravity = new AntiGravity();
    public Athletic athletic = new Athletic();
    public Balding balding = new Balding();
    public BrightEyed brightEyed = new BrightEyed();
    public Calming calming = new Calming();
    public CalorieDense calorieDense = new CalorieDense();
    public Cyclopean cyclopean = new Cyclopean();
    public Disorienting disorienting = new Disorienting();
    public Electrifying electrifying = new Electrifying();
    public Energizing energizing = new Energizing();
    public Euphoric euphoric = new Euphoric();
    public Explosive explosive = new Explosive();
    public Focused focused = new Focused();
    public Foggy foggy = new Foggy();
    public Gingeritis gingeritis = new Gingeritis();
    public Glowie glowie = new Glowie();
    public Jennerising jennerising = new Jennerising();
    public Laxative laxative = new Laxative();
    public Lethal lethal = new Lethal();
    public LongFaced longFaced = new LongFaced();
    public Munchies munchies = new Munchies();
    public Paranoia paranoia = new Paranoia();
    public Refreshing refreshing = new Refreshing();
    public Schizophrenic schizophrenic = new Schizophrenic();
    public Sedating sedating = new Sedating();
    public Seizure seizure = new Seizure();
    public Shrinking shrinking = new Shrinking();
    public Slippery slippery = new Slippery();
    public Smelly smelly = new Smelly();
    public Sneaky sneaky = new Sneaky();
    public Spicy spicy = new Spicy();
    public ThoughtProvoking thoughtProvoking = new ThoughtProvoking();
    public Toxic toxic = new Toxic();
    public TropicThunder tropicThunder = new TropicThunder();
    public Zombifying zombifying = new Zombifying();
    private static readonly string ConfigFolder = Path.Combine(MelonEnvironment.UserDataDirectory, "MoreRealisticSleeping");
    private static readonly string EffectImagesFolder = Path.Combine(ConfigFolder, "EffectImages");
    private static readonly string FilePath = Path.Combine(ConfigFolder, "SleepingAppIcon.png");
    public static Sprite appIconSprite;
    public Coroutine removeEffectCoroutine = null;


}