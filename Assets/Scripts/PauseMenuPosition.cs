using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuPosition : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform optionsObject;
    public Transform quitObject;
    public Transform menuObject;

    public Transform resumeObject;

    private Transform playerCam;
    void Start()
    {
        playerCam = GameObject.FindWithTag("Player").transform.GetChild(0).transform.GetChild(3).transform;

        Vector3 playerPos = playerCam.position;
        Vector3 playerDirection = playerCam.transform.forward;
        Quaternion playerRotation = playerCam.transform.rotation;

        float spawnDistance = 3f;

        Vector3 optionsPos = playerPos+ playerDirection*spawnDistance;
        optionsObject.position = optionsPos;
        Vector3 quitPos = optionsPos + optionsObject.TransformDirection(new Vector3(0, 1, -1));
        //Vector3 menuPos = 

        
        quitObject.position = new Vector3(playerCam.position.x+3, playerCam.position.y+3, playerCam.position.z);
        menuObject.position = new Vector3(playerCam.position.x-3, playerCam.position.y+3, playerCam.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
