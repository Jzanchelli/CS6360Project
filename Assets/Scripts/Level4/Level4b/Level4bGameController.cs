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
    public float subLevel1PerimeterWidth;
    public float subLevel1PerimeterHeight;
    public GameObject subLevel2TargetAreaCenter;
    public float subLevel2PerimeterWidth;
    public float subLevel2PerimeterHeight;
    public GameObject subLevel3TargetAreaCenter;
    public float subLevel3PerimeterWidth;
    public float subLevel3PerimeterHeight;
    public GameObject subLevel4TargetAreaCenter;
    public float subLevel4PerimeterWidth;
    public float subLevel4PerimeterHeight;
    public GameObject subLevel5TargetAreaCenter;
    public float subLevel5PerimeterWidth;
    public float subLevel5PerimeterHeight;
    public AudioSource audioSource;
    public GameObject player;
    public TextMeshPro timeText;
    public TextMeshPro scoreText;
    public TextMeshPro distanceText;
    private System.TimeSpan time;
    private System.DateTime startTime;
    private System.DateTime targetTime;
    private Vector3 targetPosition;
    private double distance;
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
                timeText.text = "Time: " + time.Minutes + ":" + time.Seconds + "." + time.Milliseconds;
            }
        
    }

    public void getTime()
    {
        targetTime =  System.DateTime.Now;
    }

    public void getDistance()
    {
        UnityEngine.Debug.Log(targetPosition);
        float xret = targetPosition.x - player.transform.position.x;
        float yret = targetPosition.y - player.transform.position.y;
        float zret = targetPosition.z - player.transform.position.z;
        UnityEngine.Debug.Log(xret + ","+yret+","+zret);
        distance = Mathf.Sqrt(xret * xret + yret * yret + zret * zret);
        UnityEngine.Debug.Log(distance);
        distanceText.text = "Distance: " + distance;
    }

    public void StartLevel()
    {
        this.audioSource.mute = true;
        this.isStart = true;
        this.score = 0;
        this.startTime = System.DateTime.Now;
        if (!this.gameStart)
        {
            switch (subSubLevel)
            {
                case 0:
                    this.gameStart = true;
                    this.subLevel = 0;
                    this.remainingTargets = 5;
                    this.spawnTargets();
                    break;
                case 1:
                    this.gameStart = true;
                    this.subLevel = 0;
                    this.remainingTargets = 10;
                    this.spawnTargets();
                    break;
                case 2:
                    this.gameStart = true;
                    this.subLevel = 0;
                    this.remainingTargets = 20;
                    this.spawnTargets();
                    break;
                case 3:
                    this.gameStart = true;
                    this.subLevel = 1;
                    this.remainingTargets = 5;
                    this.spawnTargets();
                    break;
                case 4:
                    this.gameStart = true;
                    this.subLevel = 1;
                    this.remainingTargets = 10;
                    this.spawnTargets();
                    break;
                case 5:
                    this.gameStart = true;
                    this.subLevel = 1;
                    this.remainingTargets = 20;
                    this.spawnTargets();
                    break;
                case 6:
                    this.gameStart = true;
                    this.subLevel = 2;
                    this.remainingTargets = 5;
                    this.spawnTargets();
                    break;
                case 7:
                    this.gameStart = true;
                    this.subLevel = 2;
                    this.remainingTargets = 10;
                    this.spawnTargets();
                    break;
                case 8:
                    this.gameStart = true;
                    this.subLevel = 2;
                    this.remainingTargets = 20;
                    this.spawnTargets();
                    break;
                case 9:
                    this.gameStart = true;
                    this.subLevel = 3;
                    this.remainingTargets = 5;
                    this.spawnTargets();
                    break;
                case 10:
                    this.gameStart = true;
                    this.subLevel = 3;
                    this.remainingTargets = 10;
                    this.spawnTargets();
                    break;
                case 11:
                    this.gameStart = true;
                    this.subLevel = 3;
                    this.remainingTargets = 20;
                    this.spawnTargets();
                    break;
                case 12:
                    this.gameStart = true;
                    this.subLevel = 4;
                    this.remainingTargets = 5;
                    this.spawnTargets();
                    break;
                case 13:
                    this.gameStart = true;
                    this.subLevel = 4;
                    this.remainingTargets = 10;
                    this.spawnTargets();
                    break;
                case 14:
                    this.gameStart = true;
                    this.subLevel = 4;
                    this.remainingTargets = 20;
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
        getDistance();
        getTime();
        score++;

        if (this.remainingTargets > 0)
        {
            spawnTargets();
        }
        else
        {
            this.isStart = false;
            this.gameStart = false;
            this.audioSource.mute = false;
        }
        

    }


    public void spawnTargets()
    {
        UnityEngine.Debug.Log("Creating Targets");
        GameObject tempTarget;
        switch (subLevel)
        {

            case 0:
                UnityEngine.Debug.Log("Creating Targets");
                targetPosition = new Vector3(subLevel1TargetAreaCenter.transform.position.x + Random.value * this.subLevel1PerimeterHeight * 2 - this.subLevel1PerimeterHeight, subLevel1TargetAreaCenter.transform.position.y, subLevel1TargetAreaCenter.transform.position.z + Random.value * this.subLevel1PerimeterWidth * 2 - this.subLevel1PerimeterWidth);
                tempTarget = Instantiate(target, targetPosition, Quaternion.identity);
                tempTarget.GetComponent<Level4bTargetAction>().gameController = this;
                break;
            case 1:
                UnityEngine.Debug.Log("Creating Targets");
                targetPosition = new Vector3(subLevel2TargetAreaCenter.transform.position.x + Random.value * this.subLevel2PerimeterHeight * 2 - this.subLevel2PerimeterHeight, subLevel2TargetAreaCenter.transform.position.y, subLevel2TargetAreaCenter.transform.position.z + Random.value * this.subLevel2PerimeterWidth * 2 - this.subLevel2PerimeterWidth);
                tempTarget = Instantiate(target, targetPosition, Quaternion.identity);
                tempTarget.GetComponent<Level4bTargetAction>().gameController = this;
                break;
            case 2:
                UnityEngine.Debug.Log("Creating Targets");
                targetPosition = new Vector3(subLevel3TargetAreaCenter.transform.position.x + Random.value * this.subLevel3PerimeterHeight * 2 - this.subLevel3PerimeterHeight, subLevel3TargetAreaCenter.transform.position.y, subLevel3TargetAreaCenter.transform.position.z + Random.value * this.subLevel3PerimeterWidth * 2 - this.subLevel3PerimeterWidth);
                tempTarget = Instantiate(target, targetPosition, Quaternion.identity);
                tempTarget.GetComponent<Level4bTargetAction>().gameController = this;
                break;
            case 3:
                UnityEngine.Debug.Log("Creating Targets");
                targetPosition = new Vector3(subLevel4TargetAreaCenter.transform.position.x + Random.value * this.subLevel4PerimeterHeight * 2 - this.subLevel4PerimeterHeight, subLevel4TargetAreaCenter.transform.position.y, subLevel4TargetAreaCenter.transform.position.z + Random.value * this.subLevel4PerimeterWidth * 2 - this.subLevel4PerimeterWidth);
                tempTarget = Instantiate(target, targetPosition, Quaternion.identity);
                tempTarget.GetComponent<Level4bTargetAction>().gameController = this;
                break;
            case 4:
                UnityEngine.Debug.Log("Creating Targets");
                targetPosition = new Vector3(subLevel5TargetAreaCenter.transform.position.x + Random.value * this.subLevel5PerimeterHeight * 2 - this.subLevel5PerimeterHeight, subLevel5TargetAreaCenter.transform.position.y, subLevel5TargetAreaCenter.transform.position.z + Random.value * this.subLevel5PerimeterWidth * 2 - this.subLevel5PerimeterWidth);
                tempTarget = Instantiate(target, targetPosition, Quaternion.identity);
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
