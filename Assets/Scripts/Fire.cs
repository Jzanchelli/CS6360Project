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

    public GlobalAchievements achievements;

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

    GameObject GC = null;
    GameController GCScript = null;
    void Start()
    {
        //Just incase a scene does not have a game controller.
        try
        {
            GC = GameObject.Find("GameController");
            GCScript = GC.GetComponent<GameController>();
        } catch
        {
            //nothing
        }

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
                if (remainingShots > 0)
                {
                    if (!bottomlessClip)
                    {
                        this.remainingShots--;
                    }
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

            if (hit.collider.gameObject.CompareTag("target"))
            {
                UnityEngine.Debug.Log("destroying! ");

                // Check for achievements
                if (achievements != null && PlayerControls.enableAchievements)
                {
                    if (!achievements.GetState(Achievement.hit_bottle_from_50))
                    {
                        if (hit.distance >= 50)
                            StartCoroutine(achievements.TriggerAchievement(Achievement.hit_bottle_from_50));
                    }

                    if (!achievements.GetState(Achievement.hit_target_25_while_riding))
                    {

                        bool isRiding = false;

                        if (hit.distance >= 25 && isRiding)
                            StartCoroutine(achievements.TriggerAchievement(Achievement.hit_target_25_while_riding));
                    }

                    if (!achievements.GetState(Achievement.hit_target_while_riding))
                    {
                        // Assume true for testing
                        // NOTE: Update for the actual game
                        bool isRiding = true;

                        if (isRiding)
                            StartCoroutine(achievements.TriggerAchievement(Achievement.hit_target_while_riding));
                    }
                }

                if (GCScript != null)
                {
                    UnityEngine.Debug.Log("We are in BULLET TARGET GCSCRIPT PART");
                    GCScript.TargetShot();
                }

                //get the target action and call the target PerformAction function.
                bool destroy = true;
                try
                {
                    TargetAction tAction = hit.collider.gameObject.GetComponent<TargetAction>();
                    destroy = tAction.PerformAction();
                }
                catch
                {

                }
                if (destroy)
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}