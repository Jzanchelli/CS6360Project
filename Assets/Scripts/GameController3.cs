using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController3 : MonoBehaviour
{
    public TextMeshPro score;
    int currentScore;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TargetShot(int value)
    {
        currentScore += value;
        score.text = currentScore.ToString() + " pts";
    }
}
