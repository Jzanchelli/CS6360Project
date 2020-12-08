using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousLevelTargetAction : MonoBehaviour, TargetAction
{
    public AudioClip glassShatterClip;
    public AudioSource audioSource;
    public Level4GameController gameController;

    public bool PerformAction()
    {
        UnityEngine.Debug.Log("performing target action");
        gameController.PreviousLevel();
        //audioSource.clip = this.glassShatterClip;
        audioSource.PlayOneShot(this.glassShatterClip);
        //StartCoroutine("WaitToMoveOn");
        //audioSource.clip = this.glassShatterClip;
        //audioSource.Play();

        return false;
    }

    IEnumerator WaitToMoveOn()
    {
        // audioSource.clip = this.glassShatterClip;
        //audioSource.Play();
        yield return new WaitForSeconds(2);
    }
}
