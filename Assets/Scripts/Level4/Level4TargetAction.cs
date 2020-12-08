using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4TargetAction : MonoBehaviour, TargetAction
{
    public AudioClip glassShatterClip;
    public AudioSource audioSource;
    public Level4GameController gameController;

    public bool PerformAction()
    {
        gameController.TargetHit();
        return true;
    }
}
