using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GunStart : MonoBehaviour
{
    public Throwable tScript;
    // Start is called before the first frame update
    void Start()
    {
        tScript.HolsterWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
