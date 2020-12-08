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
    public Transform gunBarrel;
    public SteamVR_Action_Boolean fireAction;
    public SteamVR_Action_Boolean reloadAction;
    public AudioClip shotAudio;
    public AudioClip reloadAudio;
    public AudioClip dryFireAudio;
    public AudioSource audioSource;
    public ParticleSystem muzzleFlash;
    public int numberOfShots;
    public float range;
    private int remainingShots;
    private Interactable interactable;
    private bool canShoot = true;
    public bool bottomlessClip = false;
    public bool shotSpread = false;
    public double maxSpreadAngle;
    public double numberOfPellets;

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
            if (fireAction[source].stateDown && canShoot)
            {
                if (remainingShots > 0)
                {
                    if (!bottomlessClip)
                    {
                        this.remainingShots--;
                    }
                    StartCoroutine(playFireAudio());
                    muzzleFlash.Play();
                    RaycastGun();
                    if (shotSpread) 
                    {
                        for (int i = 0; i < this.numberOfPellets; i++)
                        {
                            RaycastPellet(i);
                        }
                    }
                    
                }
                else if (remainingShots == 0)
                {
                    StartCoroutine(playDryFireAudio());
                }
            }
            else if (remainingShots == 0 && reloadAction[source].stateDown && canShoot)
            {
                this.remainingShots = this.numberOfShots;
                    this.canShoot = false;
                    StartCoroutine(playReloadAudio());
                }
        }
    }

    IEnumerator playDryFireAudio()
    {
        audioSource.PlayOneShot(dryFireAudio);
        yield return new WaitForSeconds(this.dryFireAudio.length);
    }

    IEnumerator playFireAudio()
    {
        audioSource.PlayOneShot(shotAudio);
        yield return new WaitForSeconds(this.shotAudio.length);
    }

    IEnumerator playReloadAudio()
    {
        audioSource.PlayOneShot(this.reloadAudio);

        yield return new WaitForSeconds(this.reloadAudio.length);
        this.canShoot = true;
    }

    private void RaycastGun()
    {
        RaycastHit hit;
        UnityEngine.Debug.Log("shooting: Position: " + gunBarrel.position + " Rotation: " + gunBarrel.transform.forward);
        if (Physics.Raycast(gunBarrel.position, gunBarrel.transform.forward, out hit, this.range))
        {
            if (hit.collider.gameObject.CompareTag("barrel"))
            {
                ExplodingBarrel eb = hit.collider.gameObject.GetComponent<ExplodingBarrel>();
                eb.Explode();

            }
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

    private void RaycastPellet(int seed)
    {
        System.Random rand = new System.Random(seed);
        float spread = Convert.ToSingle((rand.NextDouble()*this.maxSpreadAngle*2)-this.maxSpreadAngle);
        RaycastHit hit;
        //UnityEngine.Debug.DrawRay(gunBarrel.position, Quaternion.Euler(spread, spread, 0) * gunBarrel.transform.forward * this.range, Color.yellow);
        //UnityEngine.Debug.Log("shooting Pellet: Position: " + gunBarrel.position + " Rotation: " + Quaternion.Euler(spread, spread, 0) * gunBarrel.transform.forward);
        if (Physics.Raycast(gunBarrel.position, Quaternion.Euler(spread, spread, 0)*gunBarrel.transform.forward, out hit, this.range))
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