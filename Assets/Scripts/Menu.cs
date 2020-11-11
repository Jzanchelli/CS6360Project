using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Menu : MonoBehaviour
{
    private Interactable interactable;
    public SteamVR_Action_Boolean GrabPinch;
    public GameObject menuCenter;

    public GameObject levelSelectPrefab;

    //private const string levelSelectPath= @"Menu Elements\Level Selection Menu";    
    
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
            Hand hand = interactable.attachedToHand;
            if (GrabPinch[source].stateDown)
            {
                if (this.name == "LevelSelect")
                {
                    UnityEngine.Debug.Log("Level Select Selected");
                    hand.DetachObject(this.gameObject);
                    
                   // levelSelect.SetActive(true);
                    Destroy(menuCenter);
                    //Resources.Load(levelSelectPath);
                    Instantiate(levelSelectPrefab, new Vector3(0,1,1), Quaternion.Euler(0,180,0));
                    //this.transform.parent.gameObject.SetActive(false);
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
                    hand.DetachObject(this.gameObject);
                }
            }
        }
    }
}
