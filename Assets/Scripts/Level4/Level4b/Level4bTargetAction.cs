using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4bTargetAction : MonoBehaviour,TargetAction
{
    public AudioClip glassShatterClip;
    public AudioSource audioSource;
    public Level4bGameController gameController;

    public bool PerformAction()
    {
        UnityEngine.Debug.Log("performing Milk target action");
        gameController.TargetHit();
        return true;
    }
}