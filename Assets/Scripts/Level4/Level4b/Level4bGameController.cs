using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level4bGameController : MonoBehaviour
{
    private int subSubLevel;
    private int subLevel;
    public GameObject target;
    public GameObject subLevel1TargetAreaCenter;
    public float subLevel1PerimiterWidth;
    public float subLevel2PerimiterHeight;
    public GameObject subLevel2TargetAreaCenter;
    public GameObject subLevel3TargetAreaCenter;
    public GameObject subLevel4TargetAreaCenter;
    public GameObject subLevel5TargetAreaCenter;
    public GameObject player;
    public TextMeshPro timeText;
    public TextMeshPro scoreText;
    private System.TimeSpan time;
    private System.DateTime startTime;
    private IEnumerator coroutine;
    private bool isDisqualified;
    private bool isStart;
    private bool gameStart;
    private int score;
    private int remainingTargets;
    private GameObject[] targets;
    // Start is called before the first frame update
    void Start()
    {
        this.score = 0;
        this.subLevel = 0;
        this.subSubLevel = 0;
        this.isDisqualified = false;
        this.isStart = false;
        this.gameStart = false;

    }

    // Update is called once per frame
    void Update()
    {
        //timeText.text = "Time: " + time.Seconds + "." + time.Milliseconds;
        scoreText.text = "Score: " + score;
        /*if (targetContainer1.transform.childCount == 0 && targetContainer2.transform.childCount == 0 && targetContainer3.transform.childCount == 0)
        {
            this.isStart = false;
            this.gameStart = false;
            timeText.text = "Time: " + time.Seconds + "." + time.Milliseconds;
        }*/
       
            if (isStart)
            {
                time = System.DateTime.Now - startTime;
                timeText.text = "Time: " + time.Seconds + "." + time.Milliseconds;
            }
        
    }

    public void getTime()
    {
        this.isStart = false;
        this.gameStart = false;
        timeText.text = "Time: " + time.Seconds + "." + time.Milliseconds;
    }

    public void StartLevel()
    {
        this.isStart = true;
        this.score = 0;
        this.startTime = System.DateTime.Now;
        if (!this.gameStart)
        {
            switch (subSubLevel)
            {
                case 0:
                    this.gameStart = true;
                    this.remainingTargets = 5;
                    this.spawnTargets();
                    break;
                case 1:
                    this.gameStart = true;
                    this.remainingTargets = 10;
                    this.spawnTargets();
                    break;
                case 2:
                    this.gameStart = true;
                    this.remainingTargets = 15;
                    this.spawnTargets();
                    break;
                case 3:
                    this.gameStart = true;
                    this.remainingTargets = 20;
                    this.spawnTargets();
                    break;
                case 4:
                    this.gameStart = true;
                    this.remainingTargets = 30;
                    this.spawnTargets();
                    break;
                /*case 5:
                    this.gameStart = true;
                    SubLevel6();
                    break;
                case 6:
                    this.gameStart = true;
                    SubLevel7();
                    break;
                case 7:
                    this.gameStart = true;
                    SubLevel8();
                    break;
                case 8:
                    this.gameStart = true;
                    SubLevel9();
                    break;
                case 9:
                    this.gameStart = true;
                    SubLevel10();
                    break;*/
                default:
                    break;
            }
        }
    }

    /*public bool CheckAndClearSubLevel1TargetContainers(int index)
    {
        if (this.subLevel1TargetContainers[index].transform.childCount == 0)
        {
            Destroy(this.subLevel1TargetContainers[index].transform.GetChild(0).gameObject);
            return true;
        }
        return false;
    }*/

    public void PreviousLevel()
    {
        if (subSubLevel > 0)
        {
            subSubLevel--;
        }
    }

    public void NextLevel()
    {
        if (subSubLevel < 10)
        {
            subSubLevel++;
        }
    }

    public void TargetHit()
    {
        this.remainingTargets--;
        score++;
        if (this.remainingTargets > 0)
        {
            spawnTargets();
        }
        else
        {
            this.isStart = false;
        }
        

    }


    public void spawnTargets()
    {
        UnityEngine.Debug.Log("Creating Targets");
        switch (subSubLevel)
        {
            case 0:
                UnityEngine.Debug.Log("Creating Targets");
                GameObject tempTarget = Instantiate(target, subLevel1TargetAreaCenter.transform.position, Quaternion.identity);
                tempTarget.GetComponent<Level4bTargetAction>().gameController = this;
                break;
            default:
                break;
        }
    }


    /*void SubLevel1(int targets)
    {
        this.remainingTargets = targets;
        
        while (hitTargets < 10)
        {
            int index = Random.Range(0, targets);
            target = Instantiate(target, subLevel1TargetContainers[index].transform.position, Quaternion.identity);
            target.transform.localScale = new Vector3(1f, 1f, 1f);
            target.transform.SetParent(subLevel1TargetContainers[index].transform);
            while (SubLevel1TargetContainers[index].transform.childCount>0)
            {

            }
        }
        int index = Random.Range(0, targets);
        target = Instantiate(target, subLevel1TargetContainers[index].transform.position, Quaternion.identity);
        target.transform.localScale = new Vector3(1f, 1f, 1f);
        target.transform.SetParent(subLevel1TargetContainers[index].transform);
    }*/



    /*void SubLevel3()
    {
        target1 = Instantiate(target, targetContainer1.transform.position, Quaternion.identity);
        target1.transform.localScale = new Vector3(4f, 4f, 0.2514884f);
        target1.transform.SetParent(targetContainer1.transform);
        TargetMovement tmovement = target1.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        AudioProgression();
    }

    void SubLevel2()
    {
        target1 = Instantiate(target, targetContainer3.transform.position, Quaternion.identity);
        target1.transform.localScale = new Vector3(5f, 5f, 0.2514884f);
        target1.transform.SetParent(targetContainer3.transform);
        TargetMovement tmovement = target1.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        target2 = Instantiate(target, targetContainer2.transform.position, Quaternion.identity);
        target2.transform.localScale = new Vector3(5f, 5f, 0.2514884f);
        target2.transform.SetParent(targetContainer2.transform);
        tmovement = target2.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        AudioProgression();
    }

    void SubLevel4()
    {
        target1 = Instantiate(target, targetContainer3.transform.position, Quaternion.identity);
        target1.transform.localScale = new Vector3(4f, 4f, 0.2514884f);
        target1.transform.SetParent(targetContainer3.transform);
        TargetMovement tmovement = target1.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        target2 = Instantiate(target, targetContainer2.transform.position, Quaternion.identity);
        target2.transform.localScale = new Vector3(4f, 4f, 0.2514884f);
        target2.transform.SetParent(targetContainer2.transform);
        tmovement = target2.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        AudioProgression();
    }

    void SubLevel5()
    {
        target1 = Instantiate(target, targetContainer1.transform.position, Quaternion.identity);
        target1.transform.localScale = new Vector3(3f, 3f, 0.2514884f);
        target1.transform.SetParent(targetContainer1.transform);
        TargetMovement tmovement = target1.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        AudioProgression();
    }

    void SubLevel6()
    {
        target1 = Instantiate(target, targetContainer3.transform.position, Quaternion.identity);
        target1.transform.localScale = new Vector3(3f, 3f, 0.2514884f);
        target1.transform.SetParent(targetContainer3.transform);
        TargetMovement tmovement = target1.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        target2 = Instantiate(target, targetContainer2.transform.position, Quaternion.identity);
        target2.transform.localScale = new Vector3(3f, 3f, 0.2514884f);
        target2.transform.SetParent(targetContainer2.transform);
        tmovement = target2.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        AudioProgression();
    }

    void SubLevel7()
    {
        target1 = Instantiate(target, targetContainer1.transform.position, Quaternion.identity);
        target1.transform.localScale = new Vector3(2f, 2f, 0.2514884f);
        target1.transform.SetParent(targetContainer1.transform);
        TargetMovement tmovement = target1.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        AudioProgression();
    }

    void SubLevel8()
    {
        target1 = Instantiate(target, targetContainer3.transform.position, Quaternion.identity);
        target1.transform.localScale = new Vector3(2f, 2f, 0.2514884f);
        target1.transform.SetParent(targetContainer3.transform);
        TargetMovement tmovement = target1.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        target2 = Instantiate(target, targetContainer2.transform.position, Quaternion.identity);
        target2.transform.localScale = new Vector3(2f, 2f, 0.2514884f);
        target2.transform.SetParent(targetContainer2.transform);
        tmovement = target2.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        AudioProgression();
    }

    void SubLevel9()
    {
        target1 = Instantiate(target, targetContainer1.transform.position, Quaternion.identity);
        target1.transform.localScale = new Vector3(1f, 1f, 0.2514884f);
        target1.transform.SetParent(targetContainer1.transform);
        TargetMovement tmovement = target1.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        AudioProgression();
    }

    void SubLevel10()
    {
        target1 = Instantiate(target, targetContainer3.transform.position, Quaternion.identity);
        target1.transform.localScale = new Vector3(1f, 1f, 0.2514884f);
        target1.transform.SetParent(targetContainer3.transform);
        TargetMovement tmovement = target1.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        target2 = Instantiate(target, targetContainer2.transform.position, Quaternion.identity);
        target2.transform.localScale = new Vector3(1f, 1f, 0.2514884f);
        target2.transform.SetParent(targetContainer2.transform);
        tmovement = target2.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        AudioProgression();
    }*/

    void stopTime()
    {
        this.isStart = false;
    }

    IEnumerator Wait(int seconds)
    {
        for (int i = 1; seconds >= i; i++)
        {
            yield return new WaitForSeconds(1);
        }
    }

  

}
