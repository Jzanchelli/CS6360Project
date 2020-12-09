using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Inspiration from: https://www.youtube.com/watch?v=u7lwL7ubwKA
/// </summary>
public class Level4bExplodingBarrelTargetAction : MonoBehaviour, TargetAction
{
    public float radius = 5f;
    public AudioSource audioSource;
    public Level4bGameController gameController;
    public ParticleSystem particleSystem;
    GameObject GC = null;
    GameController GCScript = null;
    // Start is called before the first frame update

    public bool PerformAction()
    {
        StartCoroutine(ExplodeTarget());
        return false;
    }

    IEnumerator ExplodeTarget()
    {
        particleSystem.Play();
        yield return new WaitForSeconds(particleSystem.main.duration);
        gameController.TargetHit();
    }
}
