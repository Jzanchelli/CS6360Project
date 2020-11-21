﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTargetAction : MonoBehaviour, TargetAction
{
    public AudioClip glassShatterClip;
    public AudioSource audioSource;
    public Level4GameController gameController;

    public bool PerformAction()
    {
        UnityEngine.Debug.Log("performing target action");
        audioSource.clip = this.glassShatterClip;
        audioSource.Play();
        StartCoroutine("WaitToMoveOn");
        //audioSource.clip = this.glassShatterClip;
        //audioSource.Play();

        return false;
    }

    IEnumerator WaitToMoveOn()
    {
        // audioSource.clip = this.glassShatterClip;
        //audioSource.Play();
        yield return new WaitForSeconds(2);
        gameController.NextLevel();
    }


}