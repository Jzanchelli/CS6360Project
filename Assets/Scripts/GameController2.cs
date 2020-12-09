using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController2 : MonoBehaviour
{

    public GlobalAchievements achievements;

    int currentScore;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
    }

    [SerializeField]
    int test;
    [SerializeField]
    public TextMeshPro score;

    public void ItemRanInto(int value)
    {
        Debug.Log(value);
        currentScore += value;
        score.text = currentScore.ToString() + " pts";

        // Check for achievement
        if (currentScore >= 200){
            StartCoroutine(achievements.TriggerAchievement(Achievement.level_two_gold));
        } else if (currentScore >= 100){
            StartCoroutine(achievements.TriggerAchievement(Achievement.level_two_silver));
        } else if (currentScore >= 50){
            StartCoroutine(achievements.TriggerAchievement(Achievement.level_two_bronze));
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
