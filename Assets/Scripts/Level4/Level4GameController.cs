using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level4GameController : MonoBehaviour
{
    private int subLevel;
    public AudioSource audioSource;
    public AudioClip holsterWeaponAudio;
    public AudioClip shooterReadyAudio;
    public AudioClip drawAudio;
    public GameObject target;
    public GameObject targetContainer1;
    public GameObject targetContainer2;
    public GameObject targetContainer3;
    public GameObject player;
    public TextMeshPro text;
    public TextMeshPro levelText;
    public dreamloLeaderBoard dl;
    public TextMeshPro[] TopScores;
    private System.TimeSpan time;
    private System.DateTime startTime;
    private IEnumerator coroutine;
    private GameObject target1;
    private GameObject target2;
    private bool isDisqualified;
    private bool isStart;
    private bool gameStart;
    private int targetCount;


    // Start is called before the first frame update
    void Start()
    {
        dl.privateCode = "7Fz75i73mU-DG2k3Sksw5geKQTBM_1qkCYPxBHklv_-A";
        dl.publicCode = "5fca6169eb36fd27143b8b60";
        this.subLevel = 0;
        this.targetCount = 0;
        this.isDisqualified = false;
        this.isStart = false;
        this.gameStart = false;
        dl.GetScores();
    }

    // Update is called once per frame
    void Update()
    {
        GetHighScores();
        this.CheckAndClearTargetContainer1();
        this.CheckAndClearTargetContainer2();
        this.CheckAndClearTargetContainer3();
        if (isDisqualified)
        {
            Destroy(target1);
            Destroy(target2);
            text.text = "Early Shot";
        }
        else
        {
            
            if (isStart)
            {
                time = System.DateTime.Now - startTime;
                text.text = "Time: " + time.Seconds + "." +time.Milliseconds;
            }
            /*else
            {
                if (targetContainer1.transform.childCount == 0 && targetContainer2.transform.childCount == 0 && targetContainer3.transform.childCount == 0)
                {

                    this.isStart = false;
                    this.gameStart = false;
                    text.text = "Time: " + time.Seconds + "." + time.Milliseconds;
                    dl.AddScore("QDL," + subLevel + "," + "Cowboy 1", System.Convert.ToInt32(-time.TotalMilliseconds), System.Convert.ToInt32(time.TotalMilliseconds), "QDL," + (subLevel + 1));
                    dl.GetScores();
                }
            }*/
            
        }
        //GetHighScores();

    }

    public void GetHighScores()
    {
        UnityEngine.Debug.Log("inGetHighScores");
        List<dreamloLeaderBoard.Score> scores = dl.ToListHighToLow();
        int count = 0;
        string[] tempStringArr;
        for (int i = 0; i < TopScores.Length; i++)
        {
            TopScores[i].text = (i + 1) + ". ";
        }
        foreach (dreamloLeaderBoard.Score s in scores)
        {
            if (s.shortText.Contains("QDL"))
            {
                tempStringArr = s.shortText.Split(',');
                if (tempStringArr.Length ==2 )
                {
                        string[] tempNameArr = s.playerName.Split(',');
                        //UnityEngine.Debug.Log(tempStringArr[1]);
                        if (tempNameArr.Length ==3)
                        {
                            //this.TopScores[count].text = (count+1) + ". " + tempNameArr[4] + "  Score: " + s.score + "  Time: " + hours + ":" + minutes + ":" + seconds +  "  Level: " + tempStringArr[2] + "  SubLevel: " + tempStringArr[3];
                            this.TopScores[count].text = (count + 1) + ". Time: " + (s.seconds/1000.0) + " Level: " + tempStringArr[1];
                            count++;
                        }
                        if (count >= TopScores.Length)
                        {
                            break;
                        }
                    
                }



            }
        }
    }

    public void TargetHit()
    {
        targetCount--;
        if (!isStart)
        {
            this.isDisqualified = true;
            this.gameStart = false;
            this.isStart = false;
        }
        else if(targetCount == 0)
        {
            this.isStart = false;
            this.gameStart = false;
            Destroy(target1);
            Destroy(target2);
            dl.AddScore("QDL," + subLevel + "," + "Cowboy 1", System.Convert.ToInt32(-time.TotalMilliseconds), System.Convert.ToInt32(time.TotalMilliseconds), "QDL," + (subLevel + 1));
            dl.GetScores();
        }
    }

    public void StartLevel()
    {
       // dl.GetScores();
        if (!this.gameStart)
        {
            isDisqualified = false;
            UnityEngine.Debug.Log(subLevel);
            switch (subLevel)
            {
                case 0:
                    this.gameStart = true;
                    SubLevel1();
                    break;
                case 1:
                    this.gameStart = true;
                    SubLevel2();
                    break;
                case 2:
                    this.gameStart = true;
                    SubLevel3();
                    break;
                case 3:
                    this.gameStart = true;
                    SubLevel4();
                    break;
                case 4:
                    this.gameStart = true;
                    SubLevel5();
                    break;
                case 5:
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
                    break;
                default:
                    break;
            }
        }
        else
        {
            this.gameStart = false;
            this.isStart = false;
            this.isDisqualified = false;
            Destroy(target1);
            Destroy(target2);
            text.text = "Time: -.---";
        }
    }

    public void CheckAndClearTargetContainer1()
    {
        if (this.targetContainer1.transform.childCount > 0)
        {
            if (this.targetContainer1.transform.GetChild(0).transform.childCount < 2)
            {
                UnityEngine.Debug.Log("Destroying1");
                Destroy(this.targetContainer1.transform.GetChild(0).gameObject);
            }
        }
    }

    public void CheckAndClearTargetContainer2()
    {
        if (this.targetContainer2.transform.childCount > 0)
        {
            if (this.targetContainer2.transform.GetChild(0).transform.childCount < 2)
            {
                UnityEngine.Debug.Log("Destroying2");
                Destroy(this.targetContainer2.transform.GetChild(0).gameObject);
            }
        }
    }

    public void CheckAndClearTargetContainer3()
    {
        if (this.targetContainer3.transform.childCount > 0)
        {
            if (this.targetContainer3.transform.GetChild(0).transform.childCount < 2)
            {
                UnityEngine.Debug.Log("Destroying3");
                Destroy(this.targetContainer3.transform.GetChild(0).gameObject);
            }
        }
    }

    public void PreviousLevel()
    {
        if (subLevel > 0)
        {
            subLevel--;
            levelText.text = (subLevel+1).ToString();
        }
    }

    public void NextLevel()
    {
        if (subLevel <9)
        {
            subLevel++;
            levelText.text = (subLevel + 1).ToString();
        }
    }

    bool weaponsHolstered()
    {
        GameObject[] weapons = GameObject.FindGameObjectsWithTag("weapon");
        for (int i = 0; i < weapons.Length; i++)
        {
            if(weapons[i].transform.parent!= null)
            {
                if (weapons[i].transform.parent.tag != "holster")
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    void AudioProgression()
    {
        this.audioSource.PlayOneShot(this.holsterWeaponAudio);
        coroutine = WaitForAudioShooterReady();
        StartCoroutine(coroutine);
    }

    void SubLevel1()
    {
        this.targetCount = 1;
        target1 = Instantiate(target, targetContainer1.transform.position, Quaternion.identity);
        target1.transform.localScale = new Vector3(5f, 5f, 0.2514884f);
        target1.transform.SetParent(targetContainer1.transform);
        target1.transform.GetChild(0).GetComponent<Level4TargetAction>().gameController = this;
        TargetMovement tmovement = target1.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        AudioProgression();
    }

    void SubLevel3()
    {
        this.targetCount = 1;
        target1 = Instantiate(target, targetContainer1.transform.position, Quaternion.identity);
        target1.transform.localScale = new Vector3(4f, 4f, 0.2514884f);
        target1.transform.SetParent(targetContainer1.transform);
        target1.transform.GetChild(0).GetComponent<Level4TargetAction>().gameController = this;
        TargetMovement tmovement = target1.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        AudioProgression();
    }

    void SubLevel2()
    {
        this.targetCount = 2;
        target1 = Instantiate(target, targetContainer3.transform.position, Quaternion.identity);
        target1.transform.localScale = new Vector3(5f, 5f, 0.2514884f);
        target1.transform.SetParent(targetContainer3.transform);
        target1.transform.GetChild(0).GetComponent<Level4TargetAction>().gameController = this;
        TargetMovement tmovement = target1.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        target2 = Instantiate(target, targetContainer2.transform.position, Quaternion.identity);
        target2.transform.localScale = new Vector3(5f, 5f, 0.2514884f);
        target2.transform.SetParent(targetContainer2.transform);
        target2.transform.GetChild(0).GetComponent<Level4TargetAction>().gameController = this;
        tmovement = target2.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        AudioProgression();
    }

    void SubLevel4()
    {
        this.targetCount = 2;
        target1 = Instantiate(target, targetContainer3.transform.position, Quaternion.identity);
        target1.transform.localScale = new Vector3(4f, 4f, 0.2514884f);
        target1.transform.SetParent(targetContainer3.transform);
        target1.transform.GetChild(0).GetComponent<Level4TargetAction>().gameController = this;
        TargetMovement tmovement = target1.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        target2 = Instantiate(target, targetContainer2.transform.position, Quaternion.identity);
        target2.transform.localScale = new Vector3(4f, 4f, 0.2514884f);
        target2.transform.SetParent(targetContainer2.transform);
        target2.transform.GetChild(0).GetComponent<Level4TargetAction>().gameController = this;
        tmovement = target2.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        AudioProgression();
    }

    void SubLevel5()
    {
        this.targetCount = 1;
        target1 = Instantiate(target, targetContainer1.transform.position, Quaternion.identity);
        target1.transform.localScale = new Vector3(3f, 3f, 0.2514884f);
        target1.transform.SetParent(targetContainer1.transform);
        target1.transform.GetChild(0).GetComponent<Level4TargetAction>().gameController = this;
        TargetMovement tmovement = target1.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        AudioProgression();
    }

    void SubLevel6()
    {
        this.targetCount = 2;
        target1 = Instantiate(target, targetContainer3.transform.position, Quaternion.identity);
        target1.transform.localScale = new Vector3(3f, 3f, 0.2514884f);
        target1.transform.SetParent(targetContainer3.transform);
        target1.transform.GetChild(0).GetComponent<Level4TargetAction>().gameController = this;
        TargetMovement tmovement = target1.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        target2 = Instantiate(target, targetContainer2.transform.position, Quaternion.identity);
        target2.transform.localScale = new Vector3(3f, 3f, 0.2514884f);
        target2.transform.SetParent(targetContainer2.transform);
        target2.transform.GetChild(0).GetComponent<Level4TargetAction>().gameController = this;
        tmovement = target2.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        AudioProgression();
    }

    void SubLevel7()
    {
        this.targetCount = 1;
        target1 = Instantiate(target, targetContainer1.transform.position, Quaternion.identity);
        target1.transform.localScale = new Vector3(2f, 2f, 0.2514884f);
        target1.transform.SetParent(targetContainer1.transform);
        target1.transform.GetChild(0).GetComponent<Level4TargetAction>().gameController = this;
        TargetMovement tmovement = target1.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        AudioProgression();
    }

    void SubLevel8()
    {
        this.targetCount = 2;
        target1 = Instantiate(target, targetContainer3.transform.position, Quaternion.identity);
        target1.transform.localScale = new Vector3(2f, 2f, 0.2514884f);
        target1.transform.SetParent(targetContainer3.transform);
        target1.transform.GetChild(0).GetComponent<Level4TargetAction>().gameController = this;
        TargetMovement tmovement = target1.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        target2 = Instantiate(target, targetContainer2.transform.position, Quaternion.identity);
        target2.transform.localScale = new Vector3(2f, 2f, 0.2514884f);
        target2.transform.SetParent(targetContainer2.transform);
        target2.transform.GetChild(0).GetComponent<Level4TargetAction>().gameController = this;
        tmovement = target2.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        AudioProgression();
    }

    void SubLevel9()
    {
        this.targetCount = 1;
        target1 = Instantiate(target, targetContainer1.transform.position, Quaternion.identity);
        target1.transform.localScale = new Vector3(1f, 1f, 0.2514884f);
        target1.transform.SetParent(targetContainer1.transform);
        target1.transform.GetChild(0).GetComponent<Level4TargetAction>().gameController = this;
        TargetMovement tmovement = target1.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        AudioProgression();
    }

    void SubLevel10()
    {
        this.targetCount = 2;
        target1 = Instantiate(target, targetContainer3.transform.position, Quaternion.identity);
        target1.transform.localScale = new Vector3(1f, 1f, 0.2514884f);
        target1.transform.SetParent(targetContainer3.transform);
        target1.transform.GetChild(0).GetComponent<Level4TargetAction>().gameController = this;
        TargetMovement tmovement = target1.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        target2 = Instantiate(target, targetContainer2.transform.position, Quaternion.identity);
        target2.transform.localScale = new Vector3(1f, 1f, 0.2514884f);
        target2.transform.SetParent(targetContainer2.transform);
        target2.transform.GetChild(0).GetComponent<Level4TargetAction>().gameController = this;
        tmovement = target2.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        AudioProgression();
    }

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

    IEnumerator WaitForAudioShooterReady()
    {
        
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        while(!weaponsHolstered())
        {
             yield return null;
        }

        if (!isDisqualified)
        {
            this.audioSource.PlayOneShot(this.shooterReadyAudio);

            while (audioSource.isPlaying)
            {
                yield return null;
            }
            yield return new WaitForSeconds(2);
            isDisqualified = false;
            if (!weaponsHolstered())
            {
                this.isDisqualified = true;
                this.isStart = false;
                this.gameStart = false;
            }
            if (!this.isDisqualified)
            {
                this.audioSource.PlayOneShot(this.drawAudio);
                this.isStart = true;
                this.startTime = System.DateTime.Now;
            }
        }
        
        
        
    }

    
}
