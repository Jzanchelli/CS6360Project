using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class Reins : MonoBehaviour
{
    private Interactable interactable;
    Vector3 startPos;
    Vector3 worldStartPos;
    Vector3 currentPos;
    Vector3 posDiff;
    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        startPos = this.transform.localPosition;
        worldStartPos = this.transform.position;
        Debug.Log(startPos);
    }

    public Vector3 getDiff()
    {
        return posDiff;

    }


    // Update is called once per frame
    void Update()
    {
        if (interactable.attachedToHand != null)
        {
            Hand currentHand = null;
            foreach(Hand hand in Player.instance.hands)
            {
                foreach(Hand.AttachedObject held in hand.AttachedObjects){
                    if(held.attachedObject.tag == "reins")
                    {
                        currentHand = hand;
                        break;
                    }
                }
            }
            currentPos = currentHand.transform.localPosition;
            //player offset in PlayerOnHorse
            posDiff = (currentPos+ new Vector3(0f,0f, 0.383f)) - startPos;
            Debug.Log(startPos);
            Debug.Log(currentPos);
            Debug.Log(posDiff);
        }
        else
        {
            this.transform.localPosition = startPos;
            worldStartPos = this.transform.position;
            posDiff = new Vector3(0f,0f,0f);
        }
    }
}
