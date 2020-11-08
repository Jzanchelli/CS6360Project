using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Valve.VR.Extras;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    // public GameObject resumeTrigger;
    // public GameObject optionsTrigger;
    // public GameObject quitTrigger;
    // public GameObject menuTrigger;
    private int menuSceneIndex;

    private GameObject rightHand;

    private bool loaded;
    
    //public static SteamVR_LaserPointer rightPointer;
    void Start()
    {
        loaded = false;
        //UnloadMenu();
        menuSceneIndex = 0;
        //rightHand = this.gameObject.transform.Find("RightHand").gameObject;
    }    
    
    // Update is called once per frame
    void Update()
    {
                    //This works, for some reason using the same button registers it for 2 frames
            bool menuState = SteamVR_Input.GetState("LaunchMenu",SteamVR_Input_Sources.LeftHand);
            //UnityEngine.Debug.Log(loaded);
            if(menuState)
            {
                //UnityEngine.Debug.Log("menustate = true");
                if(!loaded)
                {
                    LoadMenu();
                    Time.timeScale = 0;
                }
            }
            else
            {
                //UnityEngine.Debug.Log("A Pressed");
                if(loaded)
                {
                    
                    UnloadMenu();
                    Time.timeScale = 1;
                }
            }        
        
    }

    public void LoadMenu()
    {
        //UnityEngine.Debug.Log("Trying to load menu");        
        SceneManager.LoadSceneAsync(menuSceneIndex, LoadSceneMode.Additive);
        loaded = true;
    }

    public void UnloadMenu()
    {
        //UnityEngine.Debug.Log("Trying to unload menu");
        SceneManager.UnloadSceneAsync(menuSceneIndex);
        Resources.UnloadAsset(this);
        loaded = false;
        //Resources.UnloadAsset(optionsTrigger);
        //Resources.UnloadAsset(quitTrigger);
        //Resources.UnloadAsset(menuTrigger);

    }
}