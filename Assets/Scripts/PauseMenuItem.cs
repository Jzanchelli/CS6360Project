using System.Collections;
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
        //UnityEngine.Debug.Log("Quit called");
        instance.levelName = menuSceneName;
        saveOptions();
        //instance.loadAsync = false;
        instance.Trigger();
        //SceneManager.LoadSceneAsync(menuSceneName);
    }

    public void LaunchOptions()
    {
        Vector3 pauseCenter = this.gameObject.transform.position;
        Quaternion pauseCenterRotation = this.gameObject.transform.rotation;
        Resume();
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<OptionsMenu>().fromPause = true;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<OptionsMenu>().LoadMenu(pauseCenter, pauseCenterRotation);
        //pauseCenterInstance.GetComponentInChildren<VRInputModule>().eventSystem = EventSystem.current;
        Time.timeScale = 0;
    }

    public void Restart()
    {
        //Resume();
        Time.timeScale = 1f;
        currentSceneName = SceneManager.GetActiveScene().name;
        saveOptions();
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

    private void saveOptions()
    {
        GameObject optionValuesInstance = Instantiate(OptionsValuesPrefab, Vector3.zero, Quaternion.identity);
        optionValuesInstance.GetComponent<OptionValues>().bottomlessClip = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<OptionsMenu>().bottomless;
        if(!GameObject.FindGameObjectWithTag("Player").name.Equals("PlayerOnHorse"))
        {
            optionValuesInstance.GetComponent<OptionValues>().playerSpeed = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerControls>().speed;
        }
        else
        {
            optionValuesInstance.GetComponent<OptionValues>().playerSpeed = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<HorseControls>().speed;
        }
        DontDestroyOnLoad(optionValuesInstance);
    }
    
}
