[center][size=6][b]MoreRealisticSleeping[/b][/size][/center]
[center][img]https://github.com/user-attachments/assets/8e48292b-4764-4c76-b02f-c211799fedfc[/img][/center]

[size=5][b]📖 About the Mod[/b][/size]

[i]MoreRealisticSleeping[/i] is a mod for the game *Schedule I* that enhances the sleeping mechanics, making them more realistic and engaging. It introduces features like forced sleep, positive and negative effects, and customizable settings for sleep durations and probabilities.


[size=5][b]⚠️ Known Incompatibilities / Issues[/b][/size]

[list]
[*][b]Incompatibility with other mods modifying the sleep system:[/b]  
Mods that alter the sleep mechanics or the `SleepCanvas` may conflict with [i]MoreRealisticSleeping[/i].
[*][b]Incompatibility with most translation mods:[/b]  
Mods that replace some of the names in the apps `Deliveries App` or `Products App` may conflict.
[*][b]Multiplayer Effects:[/b]
The effects are most likely client-side, meaning that it won't be visible in Mutliplayer
It might not work for Joining Players (please test this yourself)
[/list]

[size=5][b]🆕 Whats new in V1.0.1?[/b][/size]
[list]
[*][b]Delay after 4:00 AM:[/b]
Allow to add a new configurable delay, starting at 4:00 AM before the Forced Sleep will be triggered
[*][b]Automatic Sleep Animation Skipping (Multiplayer usage):[/b]
Added a new Checkbox to enable the automatic skipping
This can be used to automatically press the "Continue"-Buttons when the Sleep Canvas appears
This leads to no more "Waiting for host.." since the host will automatically continue when enabled
[/list]

[size=5][b]🚀 Planned Features[/b][/size]

[list]
[*][b]Custom Sleep Times:[/b]  
Allow players to set forced sleep time and wake-up times.
[*][b]Probability to wake up at the hospital or police station:[/b]  
Introduce more variations for after-sleep events, such as paying fees when caught or injured.
[*][b]Expanded Effect Settings:[/b]  
Add more granular control over positive and negative effects, including intensity and duration.
[/list]


[size=5][b]🛠️ Features[/b][/size]

[size=4][b]Legit Mode[/b][/size]
The `Legit Mode` feature ensures a fair and balanced gameplay experience by disabling custom app loading and enforcing stricter rules. This mode is particularly useful for players who want to avoid the temptation of cheating or ensure compatibility with other mods.

[list]
[*][b]Enable Legit Mode and Configure:[/b]  
Use the `MoreRealisticSleeping.json` configuration file to enable `Use_Legit_Version` and set it to `true`.  
[code]
{
    "Use_Legit_Version": true
}
[/code]
[*][b]Behavior:[/b]  
All settings will be loaded on startup using the JSON configuration.  
No custom app will be loaded in-game, ensuring a more authentic experience.
[*][b]Default Value:[/b]  
The default value for `Use_Legit_Version` is `false`.
[/list]

This mode may help resolve compatibility issues with certain other mods. Try it out to see if it improves your gameplay experience!


[size=4][b]Multiplayer Support[/b][/size]
The `Multiplayer Support` feature ensures a synchronized sleeping experience for all players in a multiplayer session. This feature requires all players to have the mod installed and configured with identical settings.

[list]
[*][b]Requirements:[/b]  
All players in the session must have [i]MoreRealisticSleeping[/i] installed.  
The `MoreRealisticSleeping.json` configuration file must be identical for all players to avoid desynchronization.
[*][b]Behavior:[/b]  
All players are forced to sleep simultaneously during the designated sleep hours.  
The game resumes once the host player has completed their sleep cycle.
[/list]

This feature enhances the cooperative gameplay experience by ensuring that all players adhere to the same sleep mechanics, maintaining balance and immersion in multiplayer sessions.


[size=4][b]Dynamic App Integration[/b][/size]
The `Dynamic App Integration` feature allows seamless interaction like other in-game apps, enhancing the overall gameplay experience. This feature ensures that the mod dynamically adapts to changes in the game environment.

[center][img]https://github.com/user-attachments/assets/3626252e-6224-42b2-a7ae-ac8a12bbebbb[/img][/center]

[list]
[*][b]Real-Time Updates:[/b]  
Changes made to the settings are applied instantly without requiring a game restart.
[*][b]Saved Configurations:[/b]  
Changes made to the settings are saved instantly to the `MoreRealisticSleeping.json`.  
Adjust the mod to your needs once and forget about it.
[/list]

[center][img]https://github.com/user-attachments/assets/20537cf9-3a3f-4bfd-bc9e-f29bd731aa23[/img][/center]

You can also modify these settings before game start via the `MoreRealisticSleeping.json` configuration file when using the `Legit Mode` to suit your preferences and gameplay style.

[center][img]https://github.com/user-attachments/assets/8e16302d-b54a-45aa-b374-ff1eab92ab6e[/img][/center]


[size=4][b]Post-Sleep Effects[/b][/size]
The `Post-Sleep Effects` feature introduces a variety of outcomes after the player wakes up, adding depth and unpredictability to the sleeping mechanics. These effects can be both positive and negative, depending on the player's actions and the configured probabilities.

[list]
[*][b]Positive Effects:[/b]
[list]
[*][b]Choose Positive Effects:[/b]  
Players can select from a variety of positive effects to be added to the randomized pool.
[center][img]https://github.com/user-attachments/assets/68c5bb72-14f4-4f92-8e5d-98af21d6a518[/img][/center]
[*][b]Configurable Duration:[/b]  
Adjust the duration of positive effects to suit your gameplay style.
[*][b]Adjustable Probability:[/b]  
Modify the probability of triggering a positive post-sleep effect.
[*][b]Condition for Positive Effects:[/b]  
Positive effects are only triggered if the player goes to sleep early, encouraging better sleep habits in the game.
[/list]

[*][b]Negative Effects:[/b]
[list]
[*][b]Choose Negative Effects:[/b]  
Players can select from a variety of negative effects to be added to the randomized pool.
[center][img]https://github.com/user-attachments/assets/c246f726-c5fa-4e8e-9dcf-d670ca757614[/img][/center]
[*][b]Configurable Duration:[/b]  
Adjust the duration of negative effects to suit your gameplay style.
[*][b]Adjustable Probability:[/b]  
Modify the probability of triggering a negative post-sleep effect.
[*][b]Condition for Negative Effects:[/b]  
Negative effects are only triggered if the player is forced to sleep.
[/list]
[/list]

This feature adds an element of strategy to the sleeping mechanics, encouraging players to carefully manage their sleep schedules and actions to maximize benefits while minimizing risks.


[size=4][b]Visual Effect Indicators[/b][/size]
The `Visual Effect Indicators` feature enhances the gameplay experience by providing a visual representation of active post-sleep effects. This feature can be toggled on or off based on player preference.

[list]
[*][b]Enable Visual Indicators:[/b]  
Players can enable or disable visual indicators for post-sleep effects via the `MoreRealisticSleeping.json` configuration file or the in-game app.
[*][b]Behavior:[/b]  
When enabled, a visual indicator will be displayed on the screen for the entire duration of the active effect.  
The indicator will vary depending on whether the effect is positive or negative, providing clear feedback to the player.
[center][img]https://github.com/user-attachments/assets/741f7a93-d061-47f2-a3b4-cec66e2c7219[/img][/center]
[*][b]Configuration Example:[/b]
[code]
{
    "Enable_Effect_Notifications": true ### When true, the Post-Sleep Effects are visualized by a notification for the whole duration of the effect
}
[/code]
[/list]

This feature adds an additional layer of immersion and clarity, allowing players to easily track the effects influencing their gameplay.


[size=5][b]⚙️ Configuration[/b][/size]

The mod's settings can be customized via the `MoreRealisticSleeping.json` configuration file, located in the game's `UserData` directory or the `SleepDose App`.

[code]
{
    "Use_Legit_Version": false, ### When enabled, no ingame app will be generated
    "SleepSettings": {
        "Enable_Forced_Sleep": true, ### When true, the player will be forced to sleep at ~ 4 AM
        "Cooldown_Time": 500, ### Specifies the cooldown before another sleep can be forced - leave this at 500, it's more like a debug feature
        "Forced_Sleep_Delay": 0, ### Specifies the delay after 4AM before Sleep actually is triggered
        "Auto_Skip_Daily_Summary": false, ### Will skip all Sleep Canvas and Daily Summary screens automatically (use as host if afk)
        "Enable_Positive_Effects": true, ### When true, positive Post-Sleep Effects can accur
        "Positive_Effects_Probability": 25,  ### Probability/Change in % for positive effects to accur
        "Positive_Effects_Duration": 60, ### Duration in seconds for positive effects
        "Enable_Negative_Effects": true, ### When true, negative Post-Sleep Effects can accur
        "Negative_Effects_Probability": 50, ### Probability/Change in % for negative effects to accur
        "Negative_Effects_Duration": 60, ### Duration in seconds for negative effects
        "Enable_Effect_Notifications": true ### When true, the Post-Sleep Effects are visualized by a notification for the whole duration of the effect
    },
    "EffectSettings": {
        "PositiveEffectSettings": { ### When true, the related effect is added to the pool of randomzied poistive effects that can happen
            "Anti_Gravity": true,
            "Athletic": true,
            "Bright_Eyed": true,
            "Calming": true,
            "Calorie_Dense": true,
            "Electrifying": true,
            "Energizing": true,
            "Euphoric": true,
            "Focused": true,
            "Munchies": false,
            "Refreshing": false,
            "Sneaky": true
        },
        "NegativeEffectSettings": { ### When true, the related effect is added to the pool of randomzied negative effects that can happen
            "Balding": true,
            "Bright_Eyed": true,
            "Calming": true,
            "Calorie_Dense": true,
            "Cyclopean": true,
            "Disorienting": true,
            "Electrifying": true,
            "Explosive": false,
            "Foggy": true,
            "Gingeritis": true,
            "Glowing": true,
            "Jennerising": true,
            "Laxative": true,
            "Lethal": false,
            "Long_Faced": true,
            "Paranoia": true,
            "Schizophrenic": true,
            "Sedating": true,
            "Seizure_Inducing": true,
            "Shrinking": true,
            "Slippery": true,
            "Smelly": true,
            "Spicy": true,
            "Thought_Provoking": true,
            "Toxic": true,
            "Tropic_Thunder": true,
            "Zombifying": true
        }
    }
}
[/code]


[size=5]Download Mirrors[/size]

[url=https://github.com/KampfBallerina/MoreRealisticSleeping][b]GitHub[/b][/url]
[url=https://thunderstore.io/c/schedule-i/p/KampfBallerina/MoreRealisticSleeping/][b]ThunderStore[/b][/url]