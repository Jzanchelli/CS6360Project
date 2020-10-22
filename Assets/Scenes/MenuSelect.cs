﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelect : MonoBehaviour
{
    // Start is called before the first frame update

    // private GameObject lvlSelectTrigger;
    // private GameObject optionsTrigger;
    // private GameObject quitTrigger;
    private int menuSceneIndex;
    private bool previousFramePressed;
    void Start()
    {
        //unloadMenu();
        menuSceneIndex = 0;
        previousFramePressed = false;
    }    
    
    // Update is called once per frame
    void Update()
    {
        //Debug.Log()
        // if (OVRInput.GetUp(OVRInput.Button.Start))
        // {
        //     if(!previousFramePressed)
        //     {
        //         previousFramePressed = true;
        //         UnityEngine.Debug.Log("Trying to turn menu on/off");
        //         if(!SceneManager.GetSceneByBuildIndex(menuSceneIndex).isLoaded)
        //         {
        //             LoadMenu();
        //         }
        //         else
        //         {
        //             UnloadMenu();
        //         }			

        //     }
        // }
        // else
        // {
        //     previousFramePressed = false;
        // }

        //This works, for some reason using the same button registers it for 2 frames
        if(OVRInput.GetDown(OVRInput.Button.Start))
        {
            UnityEngine.Debug.Log("Menu Pressed");
            if(!SceneManager.GetSceneByBuildIndex(menuSceneIndex).isLoaded)
            {
                LoadMenu();
            }
        }
        if(OVRInput.GetDown(OVRInput.Button.One))
        {
            UnityEngine.Debug.Log("A Pressed");
            if(SceneManager.GetSceneByBuildIndex(menuSceneIndex).isLoaded)
            {
                UnloadMenu();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            if(other.name == "Level")
            {
                UnityEngine.Debug.Log("Level Select Selected");
            }
            //This works!
            else if(other.name == "Quit")
            {
                UnityEngine.Debug.Log("Quit Selected");
                #if UNITY_EDITOR
            	UnityEditor.EditorApplication.isPlaying = false;
			    #else
            	Application.Quit();
			    #endif
            }
            else if(other.name == "Options")
            {
                UnityEngine.Debug.Log("Options Selected");
            }
        }
    }

    public void LoadMenu()
    {
        UnityEngine.Debug.Log("Trying to load menu");
        SceneManager.LoadScene(menuSceneIndex, LoadSceneMode.Additive);
    }

    public void UnloadMenu()
    {
        UnityEngine.Debug.Log("Trying to unload menu");
        SceneManager.UnloadSceneAsync(menuSceneIndex);
        Resources.UnloadUnusedAssets();
    }
}