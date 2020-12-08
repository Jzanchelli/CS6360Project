using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaloonPianoScript : MonoBehaviour
{
    public AudioClip[] saloonSongs;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        this.audioSource.clip = saloonSongs[Random.Range(0, saloonSongs.Length)%3];
        this.audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
