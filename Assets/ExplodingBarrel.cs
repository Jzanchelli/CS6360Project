using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Inspiration from: https://www.youtube.com/watch?v=u7lwL7ubwKA
/// </summary>
public class ExplodingBarrel : MonoBehaviour
{
    public float radius = 5f;
    public AudioSource audioSource;
    GameObject GC = null;
    GameController GCScript = null;
    // Start is called before the first frame update

    public void Explode()
    {
        Debug.Log("We are in the explode method");
        ParticleSystem exp = GetComponent<ParticleSystem>();
        exp.Play();
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            if (hit.tag == "target")
            {
                audioSource.Play();
                GCScript.TargetShot();
                Destroy(hit.gameObject);
            }
        }
        //Change this later, looks like you did not hit the target if we don't immediately make mesh renderer false
        GetComponent<Renderer>().enabled = false;
        Destroy(gameObject, exp.main.duration);
    }
    void Start()
    {
        //ParticleSystem exp = GetComponent<ParticleSystem>();
       // exp.Stop();
        //Just incase a scene does not have a game controller.
        try
        {
            GC = GameObject.Find("GameController");
            GCScript = GC.GetComponent<GameController>();
        }
        catch
        {
            //nothing
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
