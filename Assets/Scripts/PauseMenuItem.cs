﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Valve.VR;

public class PauseMenuItem : MonoBehaviour//, IPointerClickHandler
{
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject optionsButton;
    [SerializeField] private GameObject restartButton;

    private string currentSceneName;
    private const string menuSceneName = "MainMenu";

    private Transform playerCam;

    private GameObject optionsCenterInstance;

    [SerializeField]private GameObject optionsCenterPrefab;
    public GameObject pointerPrefab;
    private GameObject pointerInstance;

    public GameObject OptionsValuesPrefab;

    private SteamVR_LoadLevel instance;

    private void Awake()
    {
        //menuActive = false;
        playerCam = GameObject.FindWithTag("Player").transform.GetChild(0).transform.GetChild(3).transform;
    }

    void Start()
    {
        //currentSceneName = SceneManager.GetActiveScene().name;
        instance = this.GetComponent<SteamVR_LoadLevel>();
        //resumeButton.GetComponent<Button>()
    }

    public void Resume()
    {
        //UnityEngine.Debug.Log("Calling Item Resume");
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PauseMenu>().UnloadMenu();
    }

    public void QuitToMenu()
    {
        //Resume();
        Time.timeScale = 1f;
        UnityEngine.Debug.Log("Quit called");
        instance.levelName = menuSceneName;
        GameObject optionValuesInstance = Instantiate(OptionsValuesPrefab, Vector3.zero, Quaternion.identity);
        optionValuesInstance.GetComponent<OptionValues>().bottomlessClip = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<OptionsMenu>().bottomless;
        optionValuesInstance.GetComponent<OptionValues>().playerSpeed = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerControls>().speed;
        DontDestroyOnLoad(optionValuesInstance);
        //instance.loadAsync = false;
        instance.Trigger();
        //SceneManager.LoadSceneAsync(menuSceneName);
    }

    public void LaunchOptions()
    {
        Resume();
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<OptionsMenu>().fromPause = true;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<OptionsMenu>().LoadMenu();
        //pauseCenterInstance.GetComponentInChildren<VRInputModule>().eventSystem = EventSystem.current;
        Time.timeScale = 0;
    }

    public void Restart()
    {
        //Resume();
        Time.timeScale = 1f;
        currentSceneName = SceneManager.GetActiveScene().name;
        // char[] alt = {'A','l','t'};
        // string sceneToLoad;
        // //UnityEngine.Debug.Log("Current Scene: " + currentSceneName);
        // if(currentSceneName.Contains("Alt"))
        //     sceneToLoad = currentSceneName.TrimEnd();
        // else
        //     sceneToLoad = currentSceneName + "Alt";
        // instance.levelName = sceneToLoad;
        instance.levelName = currentSceneName;
        //SceneManager.LoadScene(currentSceneName);
        //instance.loadAsync = false;
        instance.Trigger();

    }
    // public void OnPointerClick(PointerEventData data)
    // {
    //     UnityEngine.Debug.Log("Click called");
    //     if(data.pointerPress.Equals(resumeButton))
    //     {
    //         GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PauseMenu>().UnloadMenu();
    //     }
    //     else if(data.pointerPress.Equals(quitButton))
    //     {
    //         //UnityEngine.Debug.Log("Quit Selected");
    //         instance.levelName = menuSceneName;
    //         instance.Trigger();
            
    //     }
    //     else if(data.pointerPress.Equals(optionsButton))
    //     {
    //         //TODO: Options
    //     }
    //     else if(data.pointerPress.Equals(restartButton))
    //     {
    //         currentSceneName = SceneManager.GetActiveScene().name;
    //         instance.levelName = currentSceneName;
    //         instance.Trigger();
    //     }
    // }
}
