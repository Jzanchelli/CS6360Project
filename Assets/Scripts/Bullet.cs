using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 250))
        {
            UnityEngine.Debug.Log("hit " + hit.collider.name + " - distance: " + hit.distance);
            Destroy(gameObject);
            if (hit.collider.tag == "target")
            {
                
                    Destroy(hit.collider.gameObject);
                
            }
        }
            
    }
    /*private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        UnityEngine.Debug.Log("hit " + other.name + "!");
        if (other.isTrigger)
        {
            if (other.name != "OVRPlayerController")
            {
                Destroy(other.gameObject);
            }
        }        
    }*/
}
