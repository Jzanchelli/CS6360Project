using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemPosition : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform optionsObject;
    public Transform quitObject;
    public Transform levelObject;

    private Transform player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        Vector3 playerPos = player.position;
        Vector3 playerDirection = player.transform.forward;
        Quaternion playerRotation = player.transform.rotation;

        float spawnDistance = 3f;

        Vector3 optionsPos = playerPos + playerDirection*spawnDistance;

        optionsObject.position = optionsPos;
        quitObject.position = new Vector3(player.position.x+3, player.position.y+3, player.position.z);
        levelObject.position = new Vector3(player.position.x-3, player.position.y+3, player.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
