using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    int currentScore;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
    }

    [SerializeField]
    int test;
    [SerializeField]
    public TextMeshPro score;

    public void ItemRanInto(int value)
    {
        Debug.Log(value);
        currentScore += value;
        score.text = currentScore.ToString() + " pts";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
