using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    // private GameObject lvlSelectTrigger;
    // private GameObject optionsTrigger;
    // private GameObject quitTrigger;
    private int menuSceneIndex;
    
    
    void Start()
    {
        //unloadMenu();
        menuSceneIndex = 0;
        
    }    
    
    // Update is called once per frame
    void Update()
    {
                //This works, for some reason using the same button registers it for 2 frames
        bool menuState = SteamVR_Input.GetState("LaunchMenu",SteamVR_Input_Sources.LeftHand);
        if(menuState)
        {
            //UnityEngine.Debug.Log("Menu Pressed");
            if(!SceneManager.GetSceneByBuildIndex(menuSceneIndex).isLoaded)
            {
                LoadMenu();
                //Time.timeScale = 0;
            }
        }
        else
        {
            //UnityEngine.Debug.Log("A Pressed");
            if(SceneManager.GetSceneByBuildIndex(menuSceneIndex).isLoaded)
            {
                UnloadMenu();
                //Time.timeScale = 1;
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