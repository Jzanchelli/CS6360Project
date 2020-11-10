using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private bool menuActive;
    private Transform playerCam;
    // Update is called once per frame

    private void Awake()
    {
        menuActive = false;
    }
    void Update()
    {
        menuUp = SteamVR_Input.GetBooleanAction("LaunchMenu").state;

        if(menuUp)
        {            
            UnityEngine.Debug.Assert(menuUp, "menu should be up" + name);
            LoadMenu();
        }
        else
        {
            //UnityEngine.Debug.Assert(menuUp, "menu should be down" + name);
            UnloadMenu();
        }
    }

    public void LoadMenu()
    {
        if(!menuActive)
        {
            Time.timeScale = 0;
            // pauseCenter.SetActive(true);
            // pointer.SetActive(true);
            playerCam = GameObject.FindWithTag("Player").transform.GetChild(0).transform.GetChild(3).transform;

            Vector3 playerPos = playerCam.position;
            Vector3 playerDirection = playerCam.transform.forward;
            Quaternion playerRotation = playerCam.transform.rotation;
            float spawnDistance = 1f;
            Vector3 pausePos = playerPos+ playerDirection*spawnDistance;


            pauseCenterInstance = Instantiate(pauseCenterPrefab, pausePos, Quaternion.Euler(0,180,0));
            pauseCenterPrefab.transform.LookAt(playerCam, Vector3.up);


            menuActive = true;
        }
    }

    public void UnloadMenu()
    {
        if(menuActive)
        {
            Time.timeScale = 1f;
            // pauseCenter.SetActive(false);
            // pointer.SetActive(false);
            Destroy(pauseCenterInstance);
            menuActive = false;
        }
    }
}