using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    GameObject GC = null;
    GameController2 GCScript = null;

    public int value;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            GC = GameObject.Find("GameController");
            GCScript = GC.GetComponent<GameController2>();
        }
        catch
        {

        }
    }


    [SerializeField]
    GameObject controller;

    private void OnCollisionEnter(Collision collision)
    {
        GCScript.ItemRanInto(value);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }
}
