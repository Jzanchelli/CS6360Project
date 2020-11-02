﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HorseControls : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public float speed = 1;
    private CharacterController characterController;
    private Coroutine rotateCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {   
            RotatePlayer(input.axis.x);              
    }

    public void RotatePlayer(float angle)
    {
        if (rotateCoroutine != null)
        {
            StopCoroutine(rotateCoroutine);
        }

        rotateCoroutine = StartCoroutine(UpdatePlayer(angle));
        
    }
    

    private IEnumerator UpdatePlayer(float angle)
    {
        Player player = Player.instance;
        
        Vector3 playerFeetOffset = player.trackingOriginTransform.position - player.feetPositionGuess;
        player.trackingOriginTransform.position -= playerFeetOffset;
        player.transform.Rotate(Vector3.up, angle);
        playerFeetOffset = Quaternion.Euler(0.0f, angle, 0.0f) * playerFeetOffset;
        player.trackingOriginTransform.position += playerFeetOffset;

        Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(0, 0, input.axis.y));
        characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, 9.81f, 0) * Time.deltaTime);

        return null;

    }

}
