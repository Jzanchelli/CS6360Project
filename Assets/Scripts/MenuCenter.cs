using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCenter : MonoBehaviour
{
    public List<GameObject> toDestroy;
    // Start is called before the first frame update
    void Start()
    {
        toDestroy = new List<GameObject>();
        foreach(Transform child in this.transform)
            toDestroy.Add(child.gameObject);
    }    

    void OnDestroy() 
    {
        foreach(GameObject menuitem in toDestroy)
        {
            Destroy(menuitem);
        }
    }
}
