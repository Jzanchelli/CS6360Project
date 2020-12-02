using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level4bGameController : MonoBehaviour
{
    private int subLevel;
    public AudioSource audioSource;
    public AudioClip holsterWeaponAudio;
    public AudioClip shooterReadyAudio;
    public AudioClip drawAudio;
    public GameObject target;
    public float[] subLevel1TargetPerimeter;
    public float[] subLevel2TargetPerimeter;
    public float[] subLevel3TargetPerimeter;
    public float[] subLevel4TargetPerimeter;
    public float[] subLevel5TargetPerimeter;
    public GameObject player;
    public TextMeshPro text;
    private System.TimeSpan time;
    private System.DateTime startTime;
    private IEnumerator coroutine;
    private bool isDisqualified;
    private bool isStart;
    private bool gameStart;
    private int remainingTargets;

    // Start is called before the first frame update
    void Start()
    {
        this.subLevel = 0;
        this.isDisqualified = false;
        this.isStart = false;
        this.gameStart = false;

    }

    // Update is called once per frame
    void Update()
    {
        /*if (targetContainer1.transform.childCount == 0 && targetContainer2.transform.childCount == 0 && targetContainer3.transform.childCount == 0)
        {
            this.isStart = false;
            this.gameStart = false;
            text.text = "Time: " + time.Seconds + "." + time.Milliseconds;
        }*/
        if (isDisqualified)
        {
            text.text = "Early Shot";
        }
        else
        {
            if (isStart)
            {
                time = System.DateTime.Now - startTime;
                text.text = "Time: " + time.Seconds + "." + time.Milliseconds;
            }
        }
    }

    public void getTime()
    {
        this.isStart = false;
        this.gameStart = false;
        text.text = "Time: " + time.Seconds + "." + time.Milliseconds;
    }

    public void StartLevel()
    {
        if (!this.gameStart)
        {
            switch (subLevel)
            {
                case 0:
                    this.gameStart = true;
                    //SubLevel1(5);
                    break;
                case 1:
                    this.gameStart = true;
                    //SubLevel1(10);
                    break;
                case 2:
                    this.gameStart = true;
                    //SubLevel1(15);
                    break;
                case 3:
                    this.gameStart = true;
                    //SubLevel1(20);
                    break;
                case 4:
                    this.gameStart = true;
                    //SubLevel1(30);
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
        if (subLevel > 0)
        {
            subLevel--;
        }
    }

    public void NextLevel()
    {
        if (subLevel < 10)
        {
            subLevel++;
        }
    }

    void TargetHit()
    {

    }


    public void spawnTargets(int level)
    {
        switch (level)
        {
            case 1:
                
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
