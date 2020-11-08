using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class LevelSelect : MonoBehaviour
{
    private Interactable interactable;
    public SteamVR_Action_Boolean GrabPinch;
    public GameObject mainMenu;
    private SteamVR_LoadLevel instance;
    private string Level1 = "SampleScene";
    private string Level2 = "SampleScene";
    private string Level3 = "SampleScene";
    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        instance = this.GetComponent<SteamVR_LoadLevel>();
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
                if (this.name == "Back")
                {
                    UnityEngine.Debug.Log("Main Menu Selected");
                    hand.DetachObject(this.gameObject);
                    this.transform.parent.gameObject.SetActive(false);
                    mainMenu.SetActive(true);                    
                }
                //This works!
                else if (this.name == "Level 1")
                {
                    UnityEngine.Debug.Log("Level 1 Selected");  
                    instance.levelName = Level1; 
                    instance.Trigger();     
                }
                else if (this.name == "Level 2")
                {
                    UnityEngine.Debug.Log("Level 2 Selected");  
                    instance.levelName = Level2;      
                    instance.Trigger();          
                }
                else if (this.name == "Level 3")
                {
                    UnityEngine.Debug.Log("Level 3 Selected");  
                    instance.levelName = Level3;   
                    instance.Trigger();               
                }
            }
        }
    }
}
