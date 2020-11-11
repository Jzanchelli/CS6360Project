using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Permissions;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine;

public class Fire : MonoBehaviour
{
    //public GameObject bulletEmitter;
    public Transform gunBarrel;
    //public GameObject bulletHit;
    //public float bulletSpeed;
    public SteamVR_Action_Boolean fireAction;
    public SteamVR_Action_Boolean reloadAction;
    public AudioClip shotAudio;
    public AudioClip reloadAudio;
    public AudioClip dryFireAudio;
    public AudioSource audioSource;
    public int numberOfShots;
    public float range;
    private int remainingShots;
    private Interactable interactable;

    public bool bottomlessClip = false;

    //private GameObject newBulletHit;
    // public LineRenderer ray;

    // Start is called before the first frame update
    void Start()
    {
        this.remainingShots = numberOfShots;
        interactable = GetComponent<Interactable>();
        //ray = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bottomlessClip = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<OptionsMenu>().bottomless;
        if(interactable != null)
        if (interactable.attachedToHand != null)
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;
            if (fireAction[source].stateDown)
            {
                if (remainingShots > 0 || bottomlessClip)
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
            else if (remainingShots == 0 && reloadAction[source].stateDown)
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