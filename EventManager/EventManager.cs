using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Il2CppScheduleOne.Law;
using Il2CppScheduleOne.PlayerScripts;
using Il2CppScheduleOne.UI;
using MelonLoader;
using UnityEngine;

namespace MoreRealisticSleeping.EventManager
{
    /*
ASSAULT_FINE: 75
ATTEMPT_TO_SELL_FINE: 150
BRANDISHING_FINE: 50
CONTROLLED_SUBSTANCE_FINE: 5
DEADLY_ASSAULT_FINE: 150
DISCHARGE_FIREARM_FINE: 50
EVADING_ARREST_FINE: 50
FAILURE_TO_COMPLY_FINE: 50
HIGH_SEVERITY_DRUG_FINE: 30
MED_SEVERITY_DRUG_FINE: 20
LOW_SEVERITY_DRUG_FINE: 10
THEFT_FINE: 50
VANDALISM_FINE: 50
VIOLATING_CURFEW_TIME: 100


        */


    public class EventManager
    {
        private DeathScreen cachedDeathScreen;
        private class SaveProperty
        {
            public Vector3 Position { get; }
            public float Radius { get; }

            public SaveProperty(Vector3 position, float radius)
            {
                Position = position;
                Radius = radius;
            }
        }

        public IEnumerator MurderPlayer()
        {
            if (MRSCore.Instance.localPlayer != null)
            {
                MRSCore.Instance.localPlayer.Health.TakeDamage(100f);
                DeathScreen deathScreen = UnityEngine.Object.FindObjectOfType<DeathScreen>();
                if (deathScreen != null)
                {
                    cachedDeathScreen = deathScreen;
                    MelonLogger.Msg("Death screen found.");
                    while (!deathScreen.Container.gameObject.activeSelf)
                    {
                        yield return new WaitForSeconds(1f); // Wait until the container becomes visible
                    }

                    if (MRSCore.Instance.config.MurderedEventSettings.Allow_GetMurdered_Event_Respawning)
                    {
                        deathScreen.RespawnClicked();
                    }
                }
                else
                {
                    MelonLogger.Warning("Death screen is not found.");
                }
            }
        }


        public void AddNewPublicSleepingCrime()
        {
            int amountCrimes = 1;
            Vandalism crime = new Vandalism();
            crime.CrimeName = crimeNames[UnityEngine.Random.Range(0, crimeNames.Length)];
            MRSCore.Instance.localPlayerCrimeData.Crimes.Add(crime, amountCrimes);
        }

        public bool IsPlayerNearSaveProperty(float tolerance = 10.0f)
        {
            if (MRSCore.Instance.localPlayer != null && MRSCore.Instance.localPlayer.transform != null)
            {
                Vector3 playerPosition = MRSCore.Instance.localPlayer.transform.position;

                foreach (var property in SaveProperties)
                {
                    float distance = Vector3.Distance(playerPosition, property.Value.Position);
                    //MelonLogger.Msg($"Distance to {property.Key}: {distance}");

                    if (distance <= property.Value.Radius + tolerance)
                    {
                        return true;
                    }
                }

                return false;
            }

            MelonLogger.Warning("Local player or player transform is null.");
            return false;
        }


        public string[] crimeNames =
        {
        "Street Snoozing",
        "Sidewalk Slumber",
        "Public Napping",
        "Unauthorized Resting",
        "Curbside Coma",
        "Dreaming on Duty",
        "Concrete Cuddling",
        "Nap Dealing",
        "Loitering Horizontal",
        "Dozing Without Permit",
        "Urban Hibernation",
        "Bench Bedtime",
        "Crash Landing",
        "Sleep Trafficking",
        "Illegal REM Cycle",
        "Pavement Pilgrimage",
        "Vagrancy Nap Offense",
        "Blocking the park bench",
        "Passing Out Publicly",
        "Slumber Under Surveillance",
        "Unauthorized Zoning",
        "Resting in Restricted Area",
        "Curbside Nap Offense",
        "Public Resting Violation",
        "Unauthorized Siesta",
        "Street Slumbering",
        "Sidewalk Snoozing",
        "Public Dozing",
        "Unauthorized Napping",
        "Curbside Sleeping",
        "Dreaming on the Job",
        "Concrete Sleeping",
        "Nap Trafficking",
        "Loitering in a Horizontal Position",
        "Dozing Without a Permit",
        "Urban Sleeping",
        "Bench Sleeping",
        "Crash Sleeping",
        "Sleep Smuggling",
        "Illegal REM Offense",
        "Pavement Sleeping",
        "Vagrancy Sleeping Offense",
        };

        private Dictionary<string, SaveProperty> SaveProperties = new Dictionary<string, SaveProperty>
        {
            { "Laundromat", new SaveProperty(new Vector3(-28.689548f, 1.5649998f, 25.043943f), 6f) },
            { "Post Office", new SaveProperty(new Vector3(47.377815f, 1.1149997f, 0.7196727f), 7f) },
            { "Car Wash", new SaveProperty(new Vector3(-4.6848783f, 1.215f, -18.462233f), 5f) },
            { "Taco Ticklers", new SaveProperty(new Vector3(-30.715487f, 1.065f, 73.0847f), 12f) },
            { "RV", new SaveProperty(new Vector3(13.743962f, 2.040004f, -83.97922f), 5f) },
            { "Storage Unit", new SaveProperty(new Vector3(-2.3448243f, 1.0649998f, 103.91907f), 10f) },
            { "Motel Room", new SaveProperty(new Vector3(-71.374756f, 2.8194952f, 84.27474f), 5f) },
            { "Sweatshop", new SaveProperty(new Vector3(-61.0836f, 0.8124299f, 141.54001f), 6.5f) },
            { "Bungalow", new SaveProperty(new Vector3(-171.36903f, -2.7349997f, 115.365295f), 10f) },
            { "Barn", new SaveProperty(new Vector3(190.93658f, 1.0649998f, -10.902634f), 20f) },
            { "Docks Warehouse", new SaveProperty(new Vector3(-90.65439f, -1.2549964f, -51.6541f), 15f) },
            { "Manor", new SaveProperty(new Vector3(163.3927f, 11.464999f, -57.151375f), 25f) }
        };

    }
}