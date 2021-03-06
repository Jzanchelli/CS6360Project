﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GlobalAchievements achievements;

    public int shootingrangetime = 20;
    private int currentTime = 0;
    public TextMeshPro timeremainingtext;
    public TextMeshPro targetsShotText;
    private int targetsShot = 0;

    public int GetTime()
    {
        return currentTime;
    }

    public int GetTargetsShot()
    {
        return targetsShot;
    }
    public void StartTime()
    {
            StartCoroutine("OneSecond");
    }

    public void TargetShot()
    {
        targetsShot++;
        //Start time after first target is shot
        if(targetsShot == 1)
        {
            StartTime();
        }
    }
    void Update()
    {
        //Change texts to match targets and time remaining
        timeremainingtext.text = "Time Remaining: " + (shootingrangetime - GetTime());
        targetsShotText.text = "Targets Shot: " + GetTargetsShot();
        if (GetTime() >= shootingrangetime)
        {
            // Check for achievement
            if (targetsShot >= 10){
                StartCoroutine(achievements.TriggerAchievement(Achievement.level_one_score));
            }
        }
    }
    IEnumerator OneSecond()
    {
        for (int i = 1; shootingrangetime >= i; i++)
        {
            yield return new WaitForSeconds(1);
            currentTime += 1;
        }
    }
}
