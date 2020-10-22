using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Permissions;
using UnityEngine;

public class Fire : MonoBehaviour
{
    //public GameObject bulletEmitter;
    public Transform gunBarrel;
    //public GameObject bulletHit;
    //public float bulletSpeed;
    public AudioClip shotAudio;
    public AudioClip reloadAudio;
    public AudioClip dryFireAudio;
    public AudioSource audioSource;
    public OVRGrabbable gun;
    public int numberOfShots;
    public float range;
    private int remainingShots;
    //private GameObject newBulletHit;
    // public LineRenderer ray;

    // Start is called before the first frame update
    void Start()
    {
        this.remainingShots = numberOfShots;
        //ray = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gun.isGrabbed)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, this.gun.grabbedBy.GetController()))
            {
                if (remainingShots > 0)
                {
                    this.remainingShots--;
                    this.audioSource.clip = shotAudio;
                    audioSource.Play();
                    RaycastGun();
                }
                else if (remainingShots == 0)
                {
                    this.audioSource.clip = dryFireAudio;
                    audioSource.Play();
                }
            }
            else if (remainingShots == 0 && OVRInput.GetDown(OVRInput.Button.One, this.gun.grabbedBy.GetController()))
            {
                this.remainingShots = this.numberOfShots;
                this.audioSource.clip = this.reloadAudio;
                audioSource.Play();
            }
        }
    }

    private void RaycastGun()
    {
        RaycastHit hit;
        UnityEngine.Debug.Log("shooting: Position: " + gunBarrel.position + " Rotation: " + gunBarrel.transform.forward);
        if (Physics.Raycast(gunBarrel.position, gunBarrel.transform.forward, out hit, this.range))
        {
            UnityEngine.Debug.Log("destroying! ");
            if (hit.collider.gameObject.CompareTag("target"))
            {
                UnityEngine.Debug.Log("destroying! ");

                Destroy(hit.collider.gameObject);

            }
        }
    }
}