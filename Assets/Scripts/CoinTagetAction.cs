using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTagetAction : MonoBehaviour
{

    public AudioClip ricochetSound;
    public AudioSource audioSource;

    public bool PerformAction()
    {
        //gameObject.transform.parent.gameObject.GetComponent<AudioSource>().PlayOneShot(this.ricochetSound);
        audioSource.PlayOneShot(this.ricochetSound);
        //StartCoroutine("WaitToMoveOn");
        //audioSource.clip = this.glassShatterClip;
        //audioSource.Play();

        return true;
    }
}
