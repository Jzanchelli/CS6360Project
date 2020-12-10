using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Target : MonoBehaviour
{
    public int scoreValue;

    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }
}
