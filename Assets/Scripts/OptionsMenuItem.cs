using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Valve.VR;

public class OptionsMenuItem : MonoBehaviour//, IPointerClickHandler
{
    private string currentSceneName;
    private const string menuSceneName = "MainMenu";

    private Transform playerCam;

    private GameObject pauseCenterInstance;

    [SerializeField]private GameObject pauseCenterPrefab;
    public TextMeshProUGUI scaleDisplay;
    public GameObject pointerPrefab;
    private GameObject pointerInstance;

    private SteamVR_LoadLevel instance;

    public bool fromPause = false;

    private void Awake()
    {
        //menuActive = false;
        playerCam = GameObject.FindWithTag("Player").transform.GetChild(0).transform.GetChild(3).transform;
        //display = scaleDisplay.GetComponentInChildren<TextMeshProUGUI>();
        //bottomless = false;
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
            Vector3 menuCenter = this.gameObject.transform.position;
            Quaternion menuCenterRotation = this.gameObject.transform.rotation;
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<OptionsMenu>().UnloadMenu();
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PauseMenu>().LoadMenu(menuCenter, menuCenterRotation);
            
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<OptionsMenu>().UnloadMenu();
        }
    }

    public void setPlayerSpeed()
    {
        float scalar = this.GetComponentInChildren<Slider>().value;  
        //UnityEngine.Debug.Log(scaleDisplay.name);      
        //UnityEngine.Debug.Log(display.name);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerControls>().speed = scalar;
        //GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<OptionsMenu>().playerSpeedScale = scalar;
        //UnityEngine.Debug.Log("Scalar:" + scalar + "Changed by: " + name);
        //scaleDisplay = this.gameObject.transform.GetChild(0).GetChild(3).GetChild(4)
        
        scaleDisplay.SetText("Value: "+scalar + "x");
        //UnityEngine.Debug.Log("Text set");
        
    }

    public void ToggleBottomless()
    {
        //UnityEngine.Debug.Log("Bottomless toggled" );
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<OptionsMenu>().bottomless = !GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<OptionsMenu>().bottomless;
    }

    public void ResetOptions()
    {
        this.GetComponentInChildren<Slider>().value = 1f;
        setPlayerSpeed();
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<OptionsMenu>().bottomless = false;
    }
}
