using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectCenter : MonoBehaviour
{
    private List<GameObject> toDestroy;
    // Start is called before the first frame update
    void Start()
    {
        toDestroy = new List<GameObject>();
        foreach(Transform child in this.transform)
            toDestroy.Add(child.gameObject);
    }    

    public void DestroyChildren() 
    {
        foreach(GameObject menuitem in toDestroy)
        {
            Destroy(menuitem);
        }
    }
}
