using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaisWhiskeyBottleTargetAction : MonoBehaviour, TargetAction
{
    public AudioClip glassShatterClip;
    public AudioSource audioSource;
    //public Level4GameController gameController;

    public bool PerformAction()
    {
        //UnityEngine.Debug.Log("performing target action");
        //audioSource.clip = this.glassShatterClip;
       // audioSource.Play();
       // StartCoroutine("WaitToMoveOn");
        //gameController.StartLevel();
        //playAudio();
        //while (audioSource.isPlaying){}
        //gameController.StartLevel();
        return true;
    }

    IEnumerator WaitToMoveOn()
    {
        // audioSource.clip = this.glassShatterClip;
        //audioSource.Play();
        yield return new WaitForSeconds(2);
    }
}