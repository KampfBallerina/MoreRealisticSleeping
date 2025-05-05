using Newtonsoft.Json;

namespace MoreRealisticSleeping.Config
{
    public class ConfigState
    {
        public bool Use_Legit_Version = false;
        public SleepSettings SleepSettings { get; set; } = new SleepSettings();
        public EffectSettings EffectSettings { get; set; } = new EffectSettings();
    }

    public class SleepSettings
    {
        public bool Enable_Forced_Sleep = true;
        [JsonConverter(typeof(CleanFloatConverter))]
        public float Cooldown_Time = 500f; // Cooldown time in seconds
        public bool Enable_Positive_Effects = true;
        [JsonConverter(typeof(CleanFloatConverter))]
        public float Positive_Effects_Probability = 25f; // Probability of getting a positive effect
        [JsonConverter(typeof(CleanFloatConverter))]
        public float Positive_Effects_Duration = 60f;
        public bool Enable_Negative_Effects = true;
        [JsonConverter(typeof(CleanFloatConverter))]
        public float Negative_Effects_Probability = 50f; // Probability of getting a negative effect
        [JsonConverter(typeof(CleanFloatConverter))]
        public float Negative_Effects_Duration = 60f;
        public bool Enable_Effect_Notifications = true;

    }
    public class EffectSettings
    {
        public PositiveEffectSettings PositiveEffectSettings { get; set; } = new PositiveEffectSettings();
        public NegativeEffectSettings NegativeEffectSettings { get; set; } = new NegativeEffectSettings();
    }

    public class PositiveEffectSettings
    {
        public bool Anti_Gravity = true;
        public bool Athletic = true;
        public bool Bright_Eyed = true;
        public bool Calming = true;
        public bool Calorie_Dense = true; //Caloriedense
        public bool Electrifying = true;
        public bool Energizing = true;
        public bool Euphoric = true;
        public bool Focused = true;
        public bool Munchies = false; //Currently no effect
        public bool Refreshing = false; // Currently no effec
        public bool Sneaky = true; // Causes the police "?" meter to fill up half as fast, and increases size of pickpocket success areas.
    }

    public class NegativeEffectSettings
    {
        public bool Balding = true;
        public bool Bright_Eyed = true;
        public bool Calming = true;
        public bool Calorie_Dense = true; //Caloriedense
        public bool Cyclopean = true;
        public bool Disorienting = true;
        public bool Electrifying = true;
        public bool Explosive = false;
        public bool Foggy = true;
        public bool Gingeritis = true; // Red Hair
        public bool Glowing = true;
        public bool Jennerising = true;
        public bool Laxative = true;
        public bool Lethal = false;
        public bool Long_Faced = true; //Giraffying - Causes user's neck and face to grow.
        public bool Paranoia = true; // Causes user to have a bad high. Also makes NPCs appear to stare at the user from long distances.
        public bool Schizophrenic = true; // Causes user to run backwards while saying "oh no" (muffled) and hear muffled voices. Loud heart beat, open mouth frown, and squinting eyes. User's vision will also randomly pulse.
        public bool Sedating = true; // Causes user to have a vignette around screen and mouse smoothing.
        public bool Seizure_Inducing = true; // Causes user to have a seizure and shake on the ground.
        public bool Shrinking = true;
        public bool Slippery = true;
        public bool Smelly = true; // Stinky Cloud
        public bool Spicy = true; // Sets Head on Fire
        public bool Thought_Provoking = true; // Head grow
        public bool Toxic = true; // Causes user to vomit
        public bool Tropic_Thunder = true; //Change skin color
        public bool Zombifying = true; // Causes user to have a green skin and zombie voice
    }
}
