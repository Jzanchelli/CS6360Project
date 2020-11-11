﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Valve.VR;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsCenterPrefab;

    public GameObject pointerPrefab;

    private GameObject optionsCenterInstance;
    private GameObject pointerInstance;
    private Transform playerCam;
    // Update is called once per frame
    public bool fromPause = false;

    private void Awake()
    {
        //menuActive = false;
        playerCam = GameObject.FindWithTag("Player").transform.GetChild(0).transform.GetChild(3).transform;
    }
    void Update()
    {
        // menuUp = SteamVR_Input.GetBooleanAction("LaunchMenu").lastStateDown;
        // menuDown = SteamVR_Input.GetBooleanAction("LaunchMenu").lastStateUp;

        // if(menuUp)
        // {            
        //     //UnityEngine.Debug.Assert(menuUp, "menu should be up" + name);
        //     LoadMenu();
        // }
        // else if(menuDown)
        // {
        //     //UnityEngine.Debug.Assert(menuUp, "menu should be down" + name);
        //     UnloadMenu();
        // }
    }

    public void LoadMenu()
    {

        UnityEngine.Debug.Log("Loading OptionsMenu " + name);
        Transform rightHand = GameObject.FindWithTag("Player").transform.GetChild(0).transform.GetChild(2).transform;

        Vector3 playerPos = playerCam.position;
        Vector3 playerDirection = playerCam.transform.forward;
        Quaternion playerRotation = playerCam.transform.rotation;
        float spawnDistance = 10f;
        Vector3 pausePos = playerPos+ playerDirection*spawnDistance;
        //Quaternion pauseRotation = Quaternion.(playerDirection, Vector3.up);

        optionsCenterInstance = Instantiate(optionsCenterPrefab, Vector3.zero, Quaternion.identity);
        optionsCenterInstance.transform.position = playerCam.position;
        optionsCenterInstance.transform.LookAt(playerCam, Vector3.up);    
        optionsCenterInstance.transform.Translate(Vector3.back * spawnDistance);        

        pointerInstance = Instantiate(pointerPrefab, rightHand.transform.position, rightHand.transform.rotation);
        pointerInstance.transform.parent = rightHand;
        pointerInstance.GetComponent<Pointer>().inputModule = GameObject.FindWithTag("Player").GetComponentInChildren<VRInputModule>();
        
        GameObject.FindWithTag("Player").GetComponentInChildren<VRInputModule>().pointer = pointerInstance.GetComponent<Pointer>();
        optionsCenterInstance.GetComponentInChildren<Canvas>().worldCamera = pointerInstance.GetComponent<Camera>();
        GameObject.FindWithTag("Player").GetComponentInChildren<VRInputModule>().initializePointer();
        optionsCenterInstance.GetComponent<OptionsMenuItem>().fromPause = fromPause;
        //optionsCenterInstance.GetComponentInChildren<VRInputModule>().eventSystem = EventSystem.current;
        Time.timeScale = 0;
    }

    public void UnloadMenu()
    {
        fromPause = false;
        Time.timeScale = 1f;
        // pauseCenter.SetActive(false);
        // pointer.SetActive(false);
        Destroy(optionsCenterInstance);
        UnityEngine.Debug.Log("destroyed optionsCenterInstance " + name);
        Destroy(pointerInstance);
    }
}