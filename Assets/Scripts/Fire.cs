using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject bulletEmitter;
    public GameObject bullet;
    public float bulletSpeed;
    public AudioClip audio;
    public AudioSource audioSource;
    public OVRGrabbable gun;

    // Start is called before the first frame update
    void Start()
    {
        this.audioSource.clip= audio;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gun.isGrabbed && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, this.gun.grabbedBy.GetController()))
        {
            Triggered(this.gun.grabbedBy.GetController());
        }
    }

    public void Triggered(OVRInput.Controller controller)
    {
        audioSource.Play();
        GameObject newBullet = Instantiate(bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;
        Rigidbody newBulletRigidbody = newBullet.GetComponent<Rigidbody>();
        newBulletRigidbody.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        Destroy(newBullet, 4.0f);
        byte[] noize = { 255 };
        if (controller == OVRInput.Controller.LTouch)
        {
            //OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
            OVRHaptics.LeftChannel.Preempt(new OVRHapticsClip(noize, 1));
        }
        else if (controller == OVRInput.Controller.RTouch)
        {
            OVRHaptics.RightChannel.Preempt(new OVRHapticsClip(noize, 1));
            //OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        }
        /*yield return new WaitForSeconds(0.5f);
        if (controller == OVRInput.Controller.LTouch)
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        }
        else if (controller == OVRInput.Controller.RTouch)
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        }*/

    }
}
