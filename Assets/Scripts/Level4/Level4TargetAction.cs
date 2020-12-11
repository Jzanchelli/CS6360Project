using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4TargetAction : MonoBehaviour, TargetAction
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    public Level4GameController gameController;

    public bool PerformAction()
    {
        audioSource.PlayOneShot(audioClip); 
        gameController.TargetHit();
        
        return true;
    }
}
