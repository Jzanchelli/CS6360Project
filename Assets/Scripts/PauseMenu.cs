using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Valve.VR;

public class PauseMenu : MonoBehaviour
{
    //public SteamVR_Input_Sources m_Source = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Action_Boolean m_LaunchMenu;
    public GameObject pauseCenterPrefab;

    public GameObject pointerPrefab;

    private GameObject pauseCenterInstance;
    private GameObject pointerInstance;
    private bool menuUp;    

    private bool menuDown;
    //private bool menuActive;
    private Transform playerCam;
    // Update is called once per frame

    private void Awake()
    {
        //menuActive = false;
        playerCam = GameObject.FindWithTag("Player").transform.GetChild(0).transform.GetChild(3).transform;
    }
    void Update()
    {
        menuUp = SteamVR_Input.GetBooleanAction("LaunchMenu").lastStateDown;
        menuDown = SteamVR_Input.GetBooleanAction("LaunchMenu").lastStateUp;

        if(menuUp)
        {            
            //UnityEngine.Debug.Assert(menuUp, "menu should be up" + name);
            LoadMenu();
        }
        else if(menuDown)
        {
            //UnityEngine.Debug.Assert(menuUp, "menu should be down" + name);
            UnloadMenu();
        }
    }

    public void LoadMenu()
    {
        Transform rightHand = GameObject.FindWithTag("Player").transform.GetChild(0).transform.GetChild(2).transform;

        Vector3 playerPos = playerCam.position;
        Vector3 playerDirection = playerCam.transform.forward;
        Quaternion playerRotation = playerCam.transform.rotation;
        float spawnDistance = 10f;
        Vector3 pausePos = playerPos+ playerDirection*spawnDistance;
        //Quaternion pauseRotation = Quaternion.(playerDirection, Vector3.up);

        pauseCenterInstance = Instantiate(pauseCenterPrefab, Vector3.zero, Quaternion.identity);
        pauseCenterInstance.transform.position = playerCam.position;
        pauseCenterInstance.transform.LookAt(playerCam, Vector3.up);    
        pauseCenterInstance.transform.Translate(Vector3.back * spawnDistance);        

        pointerInstance = Instantiate(pointerPrefab, rightHand.transform.position, rightHand.transform.rotation);
        pointerInstance.transform.parent = rightHand;
        pointerInstance.GetComponent<Pointer>().inputModule = GameObject.FindWithTag("Player").GetComponentInChildren<VRInputModule>();
        
        GameObject.FindWithTag("Player").GetComponentInChildren<VRInputModule>().pointer = pointerInstance.GetComponent<Pointer>();
        pauseCenterInstance.GetComponentInChildren<Canvas>().worldCamera = pointerInstance.GetComponent<Camera>();
        GameObject.FindWithTag("Player").GetComponentInChildren<VRInputModule>().initializePointer();
        //pauseCenterInstance.GetComponentInChildren<VRInputModule>().eventSystem = EventSystem.current;
        Time.timeScale = 0;
    }

    public void UnloadMenu()
    {
        Time.timeScale = 1f;
        // pauseCenter.SetActive(false);
        // pointer.SetActive(false);
        Destroy(pauseCenterInstance);
        //UnityEngine.Debug.Log("destroyed pauseCenterInstance");
        Destroy(pointerInstance);
    }
}