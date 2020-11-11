using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Valve.VR;

public class PauseMenuItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject optionsButton;
    [SerializeField] private GameObject restartButton;

    private string currentSceneName;
    private SteamVR_LoadLevel instance;

    void Start()
    {
        //currentSceneName = SceneManager.GetActiveScene().name;
        instance = this.GetComponent<SteamVR_LoadLevel>();
    }

    public void OnPointerClick(PointerEventData data)
    {
        if(data.pointerPress.Equals(resumeButton))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PauseMenu>().UnloadMenu();
        }
        else if(data.pointerPress.Equals(quitButton))
        {
            //UnityEngine.Debug.Log("Quit Selected");
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
        else if(data.pointerPress.Equals(optionsButton))
        {
            //TODO: Options
        }
        else if(data.pointerPress.Equals(restartButton))
        {
            currentSceneName = SceneManager.GetActiveScene().name;
            instance.levelName = currentSceneName;
            instance.Trigger();
        }
    }
}
