using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class UtilityBelt : MonoBehaviour
{
    public GameObject anchor;
    public float hipTurnAngle;
    public float turnSpeed;
    public float beltDistanceFromHead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(anchor.transform.position.x, anchor.transform.position.y - beltDistanceFromHead, anchor.transform.position.z);

        float rotationSpeed = turnSpeed;
        float angleDiff = Math.Abs(anchor.transform.eulerAngles.y - gameObject.transform.eulerAngles.y);
        if (angleDiff > 100)
        {
            rotationSpeed = rotationSpeed * 2;
        }
        else if(angleDiff<80 && angleDiff > 60)
        {
            rotationSpeed = rotationSpeed / 2;
        }
        else if(angleDiff<60 && angleDiff > 40)
        {
            rotationSpeed = rotationSpeed / 4;
        }
        else if (angleDiff < 40)
        {
            rotationSpeed = 0.0f;
        }
        var step = rotationSpeed * Time.deltaTime;
        gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.Euler(0, anchor.transform.eulerAngles.y, 0), step);        
    }
}
