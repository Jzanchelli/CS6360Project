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
    //private OVRGrabbable gabbable;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip= audio;
    }

    // Update is called once per frame
    void Update()
    {
        //if (grabbable.isGrabbed())
        //{
            if(gun.isGrabbed && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, gun.grabbedBy.GetController()))
            {
                Triggered();
            }
       // }
    }

    public void Triggered()
    {
        audioSource.Play();
        GameObject newBullet = Instantiate(bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;
        Rigidbody newBulletRigidbody = newBullet.GetComponent<Rigidbody>();
        newBulletRigidbody.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        Destroy(newBullet, 4.0f);
    }
}
