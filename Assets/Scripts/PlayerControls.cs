﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerControls : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public float speed = 1;
    private CharacterController characterController;

    public static bool enableAchievements = true;

    // Start is called before the first frame update
    public void Awake()
    {
        GameObject globalSpeed = GameObject.FindGameObjectWithTag("Global Settings");
        if(globalSpeed != null)
            speed = globalSpeed.GetComponent<OptionValues>().playerSpeed;
        else
        {
            speed = 1f;            
        }
        //UnityEngine.Debug.Log("Set Speed to: " + speed);
    }
    void Start()
    {        
        characterController = GetComponent<CharacterController>();
        GameObject globalOptions = GameObject.FindGameObjectWithTag("Global Settings");
        if(globalOptions != null)
            Destroy(globalOptions);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
        characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, 9.81f, 0) * Time.deltaTime);
    }
}

