using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4bStartLevelTargetAction : MonoBehaviour, TargetAction
{
    public AudioClip glassShatterClip;
    public AudioSource audioSource;
    //public GameObject gameController;
    public Level4bGameController gameControllerScript;

    public bool PerformAction()
    {
        //gameControllerScript = gameController.GetComponent<Level4bGameController>();
        UnityEngine.Debug.Log("performing target action");
        gameControllerScript.StartLevel();
        audioSource.clip = this.glassShatterClip;
        audioSource.Play();
        StartCoroutine("WaitToMoveOn");
        //gameController.StartLevel();
        //playAudio();
        //while (audioSource.isPlaying){}
        //gameController.StartLevel();
        return false;
    }

    IEnumerator WaitToMoveOn()
    {
        // audioSource.clip = this.glassShatterClip;
        //audioSource.Play();
        yield return new WaitForSeconds(2);
        //gameControllerScript.StartLevel();
    }
}
