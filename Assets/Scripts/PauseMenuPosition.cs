using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuPosition : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform PauseParent;
    private Transform playerCam;
    private Vector3 pausePos;
    void Start()
    {
        playerCam = GameObject.FindWithTag("Player").transform.GetChild(0).transform.GetChild(3).transform;

        Vector3 playerPos = playerCam.position;
        Vector3 playerDirection = playerCam.transform.forward;
        Quaternion playerRotation = playerCam.transform.rotation;
        float spawnDistance = 3f;
         pausePos = playerPos+ playerDirection*spawnDistance;
        //Quaternion pauseRot = new Quaternion(-playerRotation.x, );

        

        
        //optionsObject.position = optionsPos;
        //Vector3 quitPos = optionsPos + optionsObject.TransformDirection(new Vector3(0, 1, -1));
        //Vector3 menuPos = 

        PauseParent.position = new Vector3 (pausePos.x, playerPos.y, pausePos.z);
        PauseParent.LookAt(playerCam, Vector3.up);

        
        //quitObject.position = quitPos;
        //menuObject.position = new Vector3(playerCam.position.x-3, playerCam.position.y+3, playerCam.position.z);
    }
}
