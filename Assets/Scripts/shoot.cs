using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Policy;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public OVRInput.Controller controller;
    private OVRGrabber parentObject;
    private GameObject gun;
    //private GrabbableProperties properties;

    // Start is called before the first frame update
    void Start()
    {
        
        //animation = GetComponent<ShootAnimation>();
        parentObject = this.GetComponent<OVRGrabber>();
        OVRGrabbable grabbed = parentObject.grabbedObject;
        gun = grabbed.transform.parent.gameObject;
        //properties = gun.GetComponent(typeof(GrabbableProperties)) as GrabbableProperties;
        //properties = gun.GetComponent<GrabbablePropeties>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (properties.isGun && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, this.controller));
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, this.controller))
        {
            Fire fire = gun.GetComponent(typeof(Fire)) as Fire;
            fire.Triggered();
            UnityEngine.Debug.Log("isTriggered");
            //GameOject newBullet = Instantiate(bullet, bulletEmitter.transform.position, gun.bulletEmitter.transform.rotation) as GameObject;
            //Rigidbody newBulletRigidbody = newBullet.GetComponent<Rigidbody>();
            //newBulletRigidbody.AddForce(transform.forward * bulletSpeed);
            //Destroy(newBullet, 10.0f);
        }
        else
        {
            UnityEngine.Debug.Log("is not Triggered");
        }
    }
}
