using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelect : MonoBehaviour
{
    // Start is called before the first frame update
    private static bool menuVisible;
    // private GameObject lvlSelectTrigger;
    // private GameObject optionsTrigger;
    // private GameObject quitTrigger;
    void Start()
    {
        menuVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.Start))
        {
            if(!menuVisible)
            {
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.UnloadSceneAsync("MainMenu");
            }			
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger)>0.5f)
        {
            if(other.name == "Level")
            {

            }
            else if(other.name == "Quit")
            {
                #if UNITY_EDITOR
            	UnityEditor.EditorApplication.isPlaying = false;
			    #else
            	Application.Quit();
			    #endif
            }
            else if(other.name == "optionsTrigger")
            {

            }
        }
    }
}
