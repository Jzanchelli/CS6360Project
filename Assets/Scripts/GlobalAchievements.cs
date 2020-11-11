﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum Achievement
{
    hit_bottle_from_50,
    travel_100,
    hit_target_while_riding,
    hit_target_25_while_riding,
    get_to_second_town,
    level_one_100_accuracy,
    level_one_score_1000,
    level_two_time_gold,
    level_two_time_silver,
    level_two_time_bronze
}

public class GlobalAchievements : MonoBehaviour
{
    // UI GameObjects
    public GameObject AchievementPanel;
    public Text AchievementTitle;
    public Text AchievementDescription;

    // Audio
    public static AudioSource AchievementSound;

    private const int N_ACHIEVEMENTS = 10;

    // Lists to track achievement state, ie whether each achievement has been achieved
    // or not.

    // This one must be public so other scripts can check achievement states.
    public bool[] AchStates = new bool[N_ACHIEVEMENTS];

    private string[] AchTitles =
    {
        "Eagle Eye",
        "Feet Tired Yet?",
        "Hit a target while riding",
        "Quick Sharpshooter",
        "Well Traveled",
        "Sharpshooter",
        "High Scorer",
        "Gold Rider",
        "Silver Rider",
        "Bronze Rider"
    };

    private string[] AchDescriptions =
    {
        "Hit a target from 50 units or more away.",
        "Travel 100 units on foot.",
        "Hit a target while riding your horse.",
        "Hit a target from 25 units or more away while riding your horse.",
        "Travel to the second town.",
        "Complete level 1 with 100% accuracy.",
        "Complete level 1 with a score of 1000 or more.",
        "Obtain a gold medal on level 2.",
        "Obtain a silver medal on level 2.",
        "Obtain a bronze medal on level 2.",
    };

    private void Start()
    {

    }

    public void SetState(Achievement ach, bool state)
    {
        AchStates[(int)ach] = state;
    }

    public bool GetState(Achievement ach)
    {
        return AchStates[(int)ach];
    }

    // Type is IEnumerator so this can be called by StartCoroutine(...)
    public IEnumerator TriggerAchievement(Achievement ach)
    {

        Debug.Log("Achievement Get!");
        Debug.Log(AchTitles[(int)ach]);

        AchievementTitle.text = AchTitles[(int)ach];
        AchievementDescription.text = AchDescriptions[(int)ach];

        AchievementPanel.SetActive(true);

        if (AchievementSound != null)
        {
            AchievementSound.Play();
        }

        // A state equal to true indicates the achievement has already been done.
        AchStates[(int)ach] = true;

        // Leave the UI up for 5 seconds.
        yield return new WaitForSeconds(5);

        // Reset UI
        AchievementTitle.text = "";
        AchievementDescription.text = "";
        AchievementPanel.SetActive(false);
    }

}