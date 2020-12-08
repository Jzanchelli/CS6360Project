using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HorseControls : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public float speed = 1;
    //public Animation anim;
    private CharacterController characterController;
    private Coroutine rotateCoroutine;
    private GameObject gameObj;
    private GameObject horse;
    private Animator trot;
    private Reins reins;
    private Vector3 reinsDiff;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        gameObj = GameObject.FindGameObjectWithTag("reins");
        reins = gameObj.GetComponent<Reins>();
    }

    // Update is called once per frame
    void Update()
    {
        reinsDiff = reins.getDiff();
        RotatePlayer(reinsDiff.x *2);
    }

    public void RotatePlayer(float angle)
    {
        if (rotateCoroutine != null)
        {
            StopCoroutine(rotateCoroutine);
        }

        rotateCoroutine = StartCoroutine(UpdatePlayer(angle));
        
    }
    

    private IEnumerator UpdatePlayer(float angle)
    {
        Player player = Player.instance;
        
        Vector3 playerFeetOffset = player.trackingOriginTransform.position - player.feetPositionGuess;
        player.trackingOriginTransform.position -= playerFeetOffset;
        player.transform.Rotate(Vector3.up, angle);
        playerFeetOffset = Quaternion.Euler(0.0f, angle, 0.0f) * playerFeetOffset;
        player.trackingOriginTransform.position += playerFeetOffset;

        horse = GameObject.FindGameObjectWithTag("horse");
        Vector3 direction = horse.transform.TransformDirection(new Vector3(reinsDiff.z *4, 0, 0));
        characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, 9.81f, 0) * Time.deltaTime);
        trot = horse.GetComponent<Animator>();
        trot.SetFloat("movement", reinsDiff.z * 4);

        yield return this;

    }

}
