using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Valve.VR;

public class OptionsMenuItem : MonoBehaviour//, IPointerClickHandler
{
    private string currentSceneName;
    private const string menuSceneName = "MainMenu";

    private Transform playerCam;

    private GameObject pauseCenterInstance;

    [SerializeField]private GameObject pauseCenterPrefab;
    public GameObject pointerPrefab;
    private GameObject pointerInstance;

    private SteamVR_LoadLevel instance;

    public bool fromPause = false;

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

    // public void setFromPause(bool pauseTriggered)
    // {
    //     fromPause = pauseTriggered;
    // }

    public void Return()
    {
        UnityEngine.Debug.Log("Returning. FromPause: " + fromPause);
        if(fromPause)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PauseMenu>().LoadMenu();
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<OptionsMenu>().UnloadMenu();
        }
    }

    public void QuitToMenu()
    {
        //Resume();
        Time.timeScale = 1f;
        UnityEngine.Debug.Log("Quit called");
        instance.levelName = menuSceneName;
        //instance.loadAsync = false;
        instance.Trigger();
        //SceneManager.LoadSceneAsync(menuSceneName);
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
}
