using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTagetAction : MonoBehaviour
{

    public AudioClip ricochetSound;
    public AudioSource audioSource;

    public bool PerformAction()
    {
        audioSource.clip = this.ricochetSound;
        audioSource.Play();
        //StartCoroutine("WaitToMoveOn");
        //audioSource.clip = this.glassShatterClip;
        //audioSource.Play();

        return true;
    }
}
