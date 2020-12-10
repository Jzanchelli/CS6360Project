using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class LevelSelect : MonoBehaviour
{
    private Interactable interactable;
    public SteamVR_Action_Boolean GrabPinch;
    private SteamVR_LoadLevel instance;
    private string Level1 = "Level_1";
    private string Level2 = "Level2";
    private string Level3 = "Level3";

    private string OpenWorld = "OpenWorldLevel";

    public GameObject levelSelectCenter;

    public GameObject mainMenuPrefab;
    public GameObject OptionsValuesPrefab;
    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        instance = this.GetComponent<SteamVR_LoadLevel>();
    }

    // Update is called once per frame
    void saveOptions()
    {
        GameObject optionValuesInstance = Instantiate(OptionsValuesPrefab, Vector3.zero, Quaternion.identity);
        optionValuesInstance.GetComponent<OptionValues>().bottomlessClip = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<OptionsMenu>().bottomless;
        optionValuesInstance.GetComponent<OptionValues>().playerSpeed = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerControls>().speed;
        DontDestroyOnLoad(optionValuesInstance);
    }
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
                   // UnityEngine.Debug.Log("Main Menu Selected");
                    hand.DetachObject(this.gameObject);
                    //Resources.UnloadAsset(this.transform.parent.gameObject);
                    levelSelectCenter.GetComponent<LevelSelectCenter>().DestroyChildren();
                    Destroy(levelSelectCenter);
                    Instantiate(mainMenuPrefab, new Vector3(-2.955297f, 0.671f, -12.256f), Quaternion.identity);                  
                }
                //This works!
                else if (this.name == "Level 1")
                {
                    //UnityEngine.Debug.Log("Level 1 Selected");  
                    instance.levelName = Level1; 
                    hand.DetachObject(this.gameObject);
                    saveOptions();
                    instance.Trigger();     
                }
                else if (this.name == "Level 2")
                {
                    //UnityEngine.Debug.Log("Level 2 Selected");  
                    instance.levelName = Level2;  
                    hand.DetachObject(this.gameObject);
                    saveOptions();
                    instance.Trigger();          
                }
                else if (this.name == "Level 3")
                {
                    //UnityEngine.Debug.Log("Level 3 Selected");  
                    instance.levelName = Level3;  
                    hand.DetachObject(this.gameObject);
                    saveOptions();
                    instance.Trigger();               
                }
                else if (this.name == "OpenWorld")
                {
                    //UnityEngine.Debug.Log("Level 3 Selected");  
                    instance.levelName = OpenWorld;  
                    hand.DetachObject(this.gameObject);
                    saveOptions();
                    instance.Trigger();               
                }
            }
        }
    }
}
