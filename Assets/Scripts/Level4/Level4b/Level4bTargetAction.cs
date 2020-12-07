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
       //audioSource.clip = this.glassShatterClip;
       //audioSource.Play();
        //StartCoroutine("WaitToMoveOn");
        gameController.TargetHit();
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
        //gameController.StartLevel();
    }
}