using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    /// <summary>
    /// Reference : https://www.youtube.com/watch?v=4R_AdDK25kQ
    /// </summary>
    // Start is called before the first frame update
    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nextPos;
    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform childTransform;

    [SerializeField]
    private Transform endPosition;

    void Start()
    {
        posA = childTransform.localPosition;
        posB = endPosition.localPosition;
        nextPos = posB;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPos, speed * Time.deltaTime);

        if (Vector3.Distance(childTransform.localPosition, nextPos) <= 0.1)
        {
            swap();
        }
    }

    private void swap()
    {
        nextPos = nextPos != posA ? posA : posB;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
