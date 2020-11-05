using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PauseMenuItem : MonoBehaviour
{
    private Interactable interactable;
    public SteamVR_Action_Boolean GrabPinch;

    public PauseMenu other;
    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable.attachedToHand != null)
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;
            if (GrabPinch[source].stateDown)
            {
                if (this.name == "Menu")
                {
                    UnityEngine.Debug.Log("Level Select Selected");
                }
                //This works!
                else if (this.name == "Quit")
                {
                    UnityEngine.Debug.Log("Quit Selected");
                    #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                    #else
                    Application.Quit();
                    #endif
                }
                else if (this.name == "Options")
                {
                    UnityEngine.Debug.Log("Options Selected");
                }
                else if (this.name == "Resume")
                {
                    UnityEngine.Debug.Log("Resume Selected");
                    other.UnloadMenu();
                    Time.timeScale = 1;
                }
            }
        }
    }
}
