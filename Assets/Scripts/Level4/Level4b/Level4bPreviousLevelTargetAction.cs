using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4bPreviousLevelTargetAction : MonoBehaviour, TargetAction
{
    public AudioClip glassShatterClip;
    public AudioSource audioSource;
    public Level4bGameController gameController;

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
        gameController.PreviousLevel();
    }
}
