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

    private SteamVR_LoadLevel instance;

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
        UnityEngine.Debug.Log("Quit called");
        instance.levelName = menuSceneName;
        //instance.loadAsync = false;
        instance.Trigger();
    }

    public void LaunchOptions()
    {
        //TODO: Options
    }

    public void Restart()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        char[] alt = {'A','l','t'};
        string sceneToLoad;
        //UnityEngine.Debug.Log("Current Scene: " + currentSceneName);
        if(currentSceneName.Contains("Alt"))
            sceneToLoad = currentSceneName.TrimEnd();
        else
            sceneToLoad = currentSceneName + "Alt";
        instance.levelName = sceneToLoad;
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
