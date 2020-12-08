using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWhiskeyBottleTargetAction : MonoBehaviour, TargetAction
{
    public AudioClip glassShatterClip;
    public AudioSource audioSource;
   

    public bool PerformAction()
    {
      
        audioSource.clip = this.glassShatterClip;
        audioSource.Play();
        StartCoroutine("WaitToMoveOn");
        
        return true;
    }

    IEnumerator WaitToMoveOn()
    {
        // audioSource.clip = this.glassShatterClip;
        //audioSource.Play();
        yield return new WaitForSeconds(2);
    }
}