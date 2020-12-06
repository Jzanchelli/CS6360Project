using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level4bGameController : MonoBehaviour
{
    private int subSubLevel;
    private int subLevel;
    private int levelSelect;
    public GameObject targetPrefab1;
    public GameObject targetPrefab2;
    public GameObject targetPrefab3;
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
    public TextMeshPro levelText;
    public TextMeshPro subLevelText;

    public TextMeshPro[] TopScores;

    public Light goLight;

    private List<dreamloLeaderBoard.Score> highScores;
    private System.TimeSpan time;
    private System.DateTime startTime;
    private System.DateTime targetTime;

    private Color greenGoLight;
    private Color whiteGoLight;
    private GameObject tempTarget;
    private Vector3 targetPosition;
    private float distance;
    private IEnumerator coroutine;
    private bool isDisqualified;
    private bool isStart;
    private bool gameStart;
    private int score;
    private int remainingTargets;
    private GameObject[] targets;
    
    public dreamloLeaderBoard dl;
    // Start is called before the first frame update
    void Start()
    {
        dl.privateCode = "7Fz75i73mU-DG2k3Sksw5geKQTBM_1qkCYPxBHklv_-A";
        dl.publicCode = "5fca6169eb36fd27143b8b60";
        greenGoLight = Color.green;
        whiteGoLight = Color.white;
        this.goLight.enabled = false;
        this.score = 0;
        this.levelSelect = 0;
        this.subLevel = 0;
        this.subSubLevel = 0;
        this.isDisqualified = false;
        this.isStart = false;
        this.gameStart = false;
        levelText.text = System.Convert.ToString((subSubLevel / 3) + 1);
        subLevelText.text = System.Convert.ToString((subSubLevel % 3) + 1);
        dl.GetScores();
        //GetHighScores();
    }

    // Update is called once per frame
    void Update()
    {
        
        scoreText.text = "Score: " + score;
        

        if (isStart)
            {
                time = System.DateTime.Now - startTime;
                timeText.text = "Time: " + time.Minutes + ":" + time.Seconds + "." + time.Milliseconds;
            }
        GetHighScores();

    }

    public void GetTime()
    {
        targetTime =  System.DateTime.Now;
    }

    public void GetScore(int TargetMultiplier)
    {
        int distanceMultiplier = (System.Convert.ToInt32(distance) / 100)+1;
        int timeMultiplier=1;
        System.TimeSpan tempTime = System.DateTime.Now - targetTime;
        if (tempTime.TotalSeconds < 15)
        {
            timeMultiplier = 3;
        }
        else if (tempTime.TotalSeconds < 30)
        {
            timeMultiplier = 2;
        }
        
        this.score += distanceMultiplier * timeMultiplier*TargetMultiplier;
    }

    public void GetHighScores()
    {
        UnityEngine.Debug.Log("inGetHighScores");
        //dl.GetScores();
        List<dreamloLeaderBoard.Score> scores = dl.ToListHighToLow();
        //UnityEngine.Debug.Log(scores.Count);
        int count = 0;
        string[] tempStringArr;
        foreach (dreamloLeaderBoard.Score s in scores)
        {
            //UnityEngine.Debug.Log(s.shortText);
            if (s.shortText.Contains("SL"))
            {
                UnityEngine.Debug.Log("inSL");
                tempStringArr = s.shortText.Split(',');
                UnityEngine.Debug.Log(tempStringArr.Length);
                if (tempStringArr.Length > 3)
                {
                    //UnityEngine.Debug.Log(tempStringArr[1]);
                    //bool scoreBottmlessClipIndicator = System.Convert.ToBoolean(System.Convert.ToInt32(tempStringArr[1]));
                    bool bottomlessClip = player.GetComponentInChildren<OptionsMenu>().bottomless;
                    UnityEngine.Debug.Log(bottomlessClip);
                    UnityEngine.Debug.Log(tempStringArr[1]);
                    if ((!bottomlessClip && tempStringArr[1].Equals("False")) || (bottomlessClip && tempStringArr[1].Equals("True")))
                    {
                        int hours = s.seconds / 36000;
                        int minutes = s.seconds / 60;
                        int seconds = s.seconds % 60;
                        UnityEngine.Debug.Log(tempStringArr[1]);
                        this.TopScores[count].text = s.playerName + "  " + s.score + "  " + hours + ":" + minutes + ":" + seconds;
                        count++;
                        if(count > 5)
                        {
                            break;
                        }
                    }
                }
                
                
               
            }
        }
    }

    public void GetDistance()
    {
        float xret = targetPosition.x - player.transform.position.x;
        float yret = targetPosition.y - player.transform.position.y;
        float zret = targetPosition.z - player.transform.position.z;
        UnityEngine.Debug.Log(xret + ","+yret+","+zret);
        distance = Mathf.Sqrt(xret * xret + yret * yret + zret * zret);
        distanceText.text = "Distance: " + Mathf.Round(distance*100)/100;
    }

    public IEnumerator StartLevel()
    {
        dl.GetScores();
        this.goLight.enabled = true;
        this.goLight.color = whiteGoLight;
        yield return new WaitForSeconds(0.5f);
        this.goLight.enabled = false;
        yield return new WaitForSeconds(0.5f);
        this.goLight.enabled = true;
        this.goLight.color = whiteGoLight;
        yield return new WaitForSeconds(0.5f);
        this.goLight.enabled = false;
        yield return new WaitForSeconds(0.5f);
        this.goLight.enabled = true;
        this.goLight.color = greenGoLight;
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
                    this.remainingTargets = 5;
                    this.SpawnTargets();
                    break;
                case 1:
                    this.gameStart = true;
                    this.remainingTargets = 5;
                    this.SpawnTargets();
                    break;
                case 2:
                    this.gameStart = true;
                    this.remainingTargets = 5;
                    this.SpawnTargets();
                    break;
                case 3:
                    this.gameStart = true;
                    this.remainingTargets = 10;
                    this.SpawnTargets();
                    break;
                case 4:
                    this.gameStart = true;
                    this.remainingTargets = 10;
                    this.SpawnTargets();
                    break;
                case 5:
                    this.gameStart = true;
                    this.remainingTargets = 10;
                    this.SpawnTargets();
                    break;
                case 6:
                    this.gameStart = true;
                    this.remainingTargets = 5;
                    this.SpawnTargets();
                    break;
                case 7:
                    this.gameStart = true;
                    this.remainingTargets = 5;
                    this.SpawnTargets();
                    break;
                case 8:
                    this.gameStart = true;
                    this.remainingTargets = 5;
                    this.SpawnTargets();
                    break;
                case 9:
                    this.gameStart = true;
                    this.remainingTargets = 10;
                    this.SpawnTargets();
                    break;
                case 10:
                    this.gameStart = true;
                    this.remainingTargets = 10;
                    this.SpawnTargets();
                    break;
                case 11:
                    this.gameStart = true;
                    this.remainingTargets = 10;
                    this.SpawnTargets();
                    break;
                case 12:
                    this.gameStart = true;
                    this.remainingTargets = 5;
                    this.SpawnTargets();
                    break;
                case 13:
                    this.gameStart = true;
                    this.remainingTargets = 5;
                    this.SpawnTargets();
                    break;
                case 14:
                    this.gameStart = true;
                    this.remainingTargets = 5;
                    this.SpawnTargets();
                    break;
                case 15:
                    this.gameStart = true;
                    this.remainingTargets = 10;
                    this.SpawnTargets();
                    break;
                case 16:
                    this.gameStart = true;
                    this.remainingTargets = 10;
                    this.SpawnTargets();
                    break;
                case 17:
                    this.gameStart = true;
                    this.remainingTargets = 10;
                    this.SpawnTargets();
                    break;
                case 18:
                    this.gameStart = true;
                    this.remainingTargets = 5;
                    this.SpawnTargets();
                    break;
                case 19:
                    this.gameStart = true;
                    this.remainingTargets = 5;
                    this.SpawnTargets();
                    break;
                case 20:
                    this.gameStart = true;
                    this.remainingTargets = 5;
                    this.SpawnTargets();
                    break;
                case 21:
                    this.gameStart = true;
                    this.remainingTargets = 10;
                    this.SpawnTargets();
                    break;
                case 22:
                    this.gameStart = true;
                    this.remainingTargets = 10;
                    this.SpawnTargets();
                    break;
                case 23:
                    this.gameStart = true;
                    this.remainingTargets = 10;
                    this.SpawnTargets();
                    break;
                case 24:
                    this.gameStart = true;
                    this.remainingTargets = 5;
                    this.SpawnTargets();
                    break;
                case 25:
                    this.gameStart = true;
                    this.remainingTargets = 5;
                    this.SpawnTargets();
                    break;
                case 26:
                    this.gameStart = true;
                    this.remainingTargets = 5;
                    this.SpawnTargets();
                    break;
                case 27:
                    this.gameStart = true;
                    this.remainingTargets = 10;
                    this.SpawnTargets();
                    break;
                case 28:
                    this.gameStart = true;
                    this.remainingTargets = 10;
                    this.SpawnTargets();
                    break;
                case 29:
                    this.gameStart = true;
                    this.remainingTargets = 10;
                    this.SpawnTargets();
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

    public void PreviousLevel()
    {
        if (this.levelSelect > 0)
        {
            this.levelSelect--;
            subLevel = this.levelSelect / 6;
            subSubLevel = this.levelSelect % 6;
            levelText.text = System.Convert.ToString(this.subLevel+1);
            subLevelText.text = System.Convert.ToString(this.subSubLevel+1);
        }
    }

    public void NextLevel()
    {
        if (this.levelSelect < 30)
        {
            this.levelSelect++;
            subLevel = this.levelSelect / 6;
            subSubLevel = this.levelSelect % 6;
            levelText.text = System.Convert.ToString(this.subLevel + 1);
            subLevelText.text = System.Convert.ToString(this.subSubLevel + 1);
        }
    }

    public void TargetHit()
    {
        this.remainingTargets--;
        Destroy(tempTarget);
        GetDistance();
        GetScore((subSubLevel%3)+1);
        GetTime();
        dl.GetScores();

        if (this.remainingTargets > 0)
        {
            SpawnTargets();
        }
        else
        {
            bool bottomlessClip = player.GetComponentInChildren<OptionsMenu>().bottomless;
            dl.AddScore("SL," + bottomlessClip.ToString() + "," + subLevel + "," + subSubLevel+","+"Cowboy 1", this.score, time.Seconds, "SL," + bottomlessClip.ToString() + "," + subLevel + "," + subSubLevel);
            this.isStart = false;
            this.gameStart = false;
            this.audioSource.mute = false;
            this.GetHighScores();
        }
        

    }


    public void SpawnTargets()
    {
        UnityEngine.Debug.Log("Creating Targets");
        
        int tempTargetSelect = this.subSubLevel % 3;
        switch (subLevel)
        {

            case 0:
                if (tempTargetSelect==0)
                {
                    UnityEngine.Debug.Log("Creating Targets");
                    targetPosition = new Vector3(subLevel1TargetAreaCenter.transform.position.x + Random.value * this.subLevel1PerimeterWidth * 2 - this.subLevel1PerimeterWidth, subLevel1TargetAreaCenter.transform.position.y, subLevel1TargetAreaCenter.transform.position.z + Random.value * this.subLevel1PerimeterHeight * 2 - this.subLevel1PerimeterHeight);
                    tempTarget = Instantiate(targetPrefab1, targetPosition, Quaternion.Euler(0,-90,0));
                    tempTarget.GetComponent<Level4bTargetAction>().gameController = this;
                }
                else if (tempTargetSelect==1)
                {
                    UnityEngine.Debug.Log("Creating Targets");
                    targetPosition = new Vector3(subLevel1TargetAreaCenter.transform.position.x + Random.value * this.subLevel1PerimeterWidth * 2 - this.subLevel1PerimeterWidth, subLevel1TargetAreaCenter.transform.position.y, subLevel1TargetAreaCenter.transform.position.z + Random.value * this.subLevel1PerimeterHeight * 2 - this.subLevel1PerimeterHeight);
                    tempTarget = Instantiate(targetPrefab2, targetPosition, Quaternion.identity);
                    tempTarget.transform.GetChild(0).GetComponent<Level4bTargetAction>().gameController = this;
                }
                else if (tempTargetSelect==2)
                {
                    UnityEngine.Debug.Log("Creating Targets");
                    targetPosition = new Vector3(subLevel1TargetAreaCenter.transform.position.x + Random.value * this.subLevel1PerimeterWidth * 2 - this.subLevel1PerimeterWidth, subLevel1TargetAreaCenter.transform.position.y, subLevel1TargetAreaCenter.transform.position.z + Random.value * this.subLevel1PerimeterHeight * 2 - this.subLevel1PerimeterHeight);
                    tempTarget = Instantiate(targetPrefab3, targetPosition, Quaternion.identity);
                    tempTarget.GetComponent<Level4bTargetAction>().gameController = this;
                }
                break;
            case 1:
                if (tempTargetSelect==0)
                {
                    UnityEngine.Debug.Log("Creating Targets");
                    targetPosition = new Vector3(subLevel2TargetAreaCenter.transform.position.x + Random.value * this.subLevel2PerimeterWidth * 2 - this.subLevel2PerimeterWidth, subLevel2TargetAreaCenter.transform.position.y, subLevel2TargetAreaCenter.transform.position.z + Random.value * this.subLevel2PerimeterHeight * 2 - this.subLevel2PerimeterHeight);
                    tempTarget = Instantiate(targetPrefab1, targetPosition, Quaternion.Euler(0, -90, 0));
                    tempTarget.GetComponent<Level4bTargetAction>().gameController = this;
                }
                else if (tempTargetSelect==1)
                {
                    UnityEngine.Debug.Log("Creating Targets");
                    targetPosition = new Vector3(subLevel2TargetAreaCenter.transform.position.x + Random.value * this.subLevel2PerimeterWidth * 2 - this.subLevel2PerimeterWidth, subLevel2TargetAreaCenter.transform.position.y, subLevel2TargetAreaCenter.transform.position.z + Random.value * this.subLevel2PerimeterHeight * 2 - this.subLevel2PerimeterHeight);
                    tempTarget = Instantiate(targetPrefab2, targetPosition, Quaternion.identity);
                    tempTarget.transform.GetChild(0).GetComponent<Level4bTargetAction>().gameController = this;
                }
                else if (tempTargetSelect==2)
                {
                    UnityEngine.Debug.Log("Creating Targets");
                    targetPosition = new Vector3(subLevel2TargetAreaCenter.transform.position.x + Random.value * this.subLevel2PerimeterWidth * 2 - this.subLevel2PerimeterWidth, subLevel2TargetAreaCenter.transform.position.y, subLevel2TargetAreaCenter.transform.position.z + Random.value * this.subLevel2PerimeterHeight * 2 - this.subLevel2PerimeterHeight);
                    tempTarget = Instantiate(targetPrefab3, targetPosition, Quaternion.identity);
                    tempTarget.GetComponent<Level4bTargetAction>().gameController = this;
                }
                break;
            case 2:
                if (tempTargetSelect==0)
                {
                    UnityEngine.Debug.Log("Creating Targets");
                    targetPosition = new Vector3(subLevel3TargetAreaCenter.transform.position.x + Random.value * this.subLevel3PerimeterWidth * 2 - this.subLevel3PerimeterWidth, subLevel3TargetAreaCenter.transform.position.y, subLevel3TargetAreaCenter.transform.position.z + Random.value * this.subLevel3PerimeterHeight * 2 - this.subLevel3PerimeterHeight);
                    tempTarget = Instantiate(targetPrefab1, targetPosition, Quaternion.Euler(0, -90, 0));
                    tempTarget.GetComponent<Level4bTargetAction>().gameController = this;
                }
                else if (tempTargetSelect==1)
                {
                    UnityEngine.Debug.Log("Creating Targets");
                    targetPosition = new Vector3(subLevel3TargetAreaCenter.transform.position.x + Random.value * this.subLevel3PerimeterWidth * 2 - this.subLevel3PerimeterWidth, subLevel3TargetAreaCenter.transform.position.y, subLevel3TargetAreaCenter.transform.position.z + Random.value * this.subLevel3PerimeterHeight * 2 - this.subLevel3PerimeterHeight);
                    tempTarget = Instantiate(targetPrefab2, targetPosition, Quaternion.identity);
                    tempTarget.transform.GetChild(0).GetComponent<Level4bTargetAction>().gameController = this;
                }
                else if (tempTargetSelect==2)
                {
                    UnityEngine.Debug.Log("Creating Targets");
                    targetPosition = new Vector3(subLevel3TargetAreaCenter.transform.position.x + Random.value * this.subLevel3PerimeterWidth * 2 - this.subLevel3PerimeterWidth, subLevel3TargetAreaCenter.transform.position.y, subLevel3TargetAreaCenter.transform.position.z + Random.value * this.subLevel3PerimeterHeight * 2 - this.subLevel3PerimeterHeight);
                    tempTarget = Instantiate(targetPrefab3, targetPosition, Quaternion.identity);
                    tempTarget.GetComponent<Level4bTargetAction>().gameController = this;
                }
                break;
            case 3:
                if (tempTargetSelect==0)
                {
                    UnityEngine.Debug.Log("Creating Targets");
                    targetPosition = new Vector3(subLevel4TargetAreaCenter.transform.position.x + Random.value * this.subLevel4PerimeterWidth * 2 - this.subLevel4PerimeterWidth, subLevel4TargetAreaCenter.transform.position.y, subLevel4TargetAreaCenter.transform.position.z + Random.value * this.subLevel4PerimeterHeight * 2 - this.subLevel4PerimeterHeight);
                    tempTarget = Instantiate(targetPrefab1, targetPosition, Quaternion.Euler(0, -90, 0));
                    tempTarget.GetComponent<Level4bTargetAction>().gameController = this;
                }
                else if (tempTargetSelect==1)
                {
                    UnityEngine.Debug.Log("Creating Targets");
                    targetPosition = new Vector3(subLevel4TargetAreaCenter.transform.position.x + Random.value * this.subLevel4PerimeterWidth * 2 - this.subLevel4PerimeterWidth, subLevel4TargetAreaCenter.transform.position.y, subLevel4TargetAreaCenter.transform.position.z + Random.value * this.subLevel4PerimeterHeight * 2 - this.subLevel4PerimeterHeight);
                    tempTarget = Instantiate(targetPrefab2, targetPosition, Quaternion.identity);
                    tempTarget.transform.GetChild(0).GetComponent<Level4bTargetAction>().gameController = this;
                }
                else if (tempTargetSelect==2)
                {
                    UnityEngine.Debug.Log("Creating Targets");
                    targetPosition = new Vector3(subLevel4TargetAreaCenter.transform.position.x + Random.value * this.subLevel4PerimeterWidth * 2 - this.subLevel4PerimeterWidth, subLevel4TargetAreaCenter.transform.position.y, subLevel4TargetAreaCenter.transform.position.z + Random.value * this.subLevel4PerimeterHeight * 2 - this.subLevel4PerimeterHeight);
                    tempTarget = Instantiate(targetPrefab3, targetPosition, Quaternion.identity);
                    tempTarget.GetComponent<Level4bTargetAction>().gameController = this;
                }
                break;
            case 4:
                if (tempTargetSelect==0)
                {
                    UnityEngine.Debug.Log("Creating Targets");
                    targetPosition = new Vector3(subLevel5TargetAreaCenter.transform.position.x + Random.value * this.subLevel5PerimeterWidth * 2 - this.subLevel5PerimeterWidth, subLevel5TargetAreaCenter.transform.position.y, subLevel5TargetAreaCenter.transform.position.z + Random.value * this.subLevel5PerimeterHeight * 2 - this.subLevel5PerimeterHeight);
                    tempTarget = Instantiate(targetPrefab1, targetPosition, Quaternion.Euler(0, -90, 0));
                    tempTarget.GetComponent<Level4bTargetAction>().gameController = this;
                }
                else if (tempTargetSelect==1)
                {
                    UnityEngine.Debug.Log("Creating Targets");
                    targetPosition = new Vector3(subLevel5TargetAreaCenter.transform.position.x + Random.value * this.subLevel5PerimeterWidth * 2 - this.subLevel5PerimeterWidth, subLevel5TargetAreaCenter.transform.position.y, subLevel5TargetAreaCenter.transform.position.z + Random.value * this.subLevel5PerimeterHeight * 2 - this.subLevel5PerimeterHeight);
                    tempTarget = Instantiate(targetPrefab2, targetPosition, Quaternion.identity);
                    tempTarget.transform.GetChild(0).GetComponent<Level4bTargetAction>().gameController = this;
                }
                else if (tempTargetSelect==2)
                {
                    UnityEngine.Debug.Log("Creating Targets");
                    targetPosition = new Vector3(subLevel5TargetAreaCenter.transform.position.x + Random.value * this.subLevel5PerimeterWidth * 2 - this.subLevel5PerimeterWidth, subLevel5TargetAreaCenter.transform.position.y, subLevel5TargetAreaCenter.transform.position.z + Random.value * this.subLevel5PerimeterHeight * 2 - this.subLevel5PerimeterHeight);
                    tempTarget = Instantiate(targetPrefab3, targetPosition, Quaternion.identity);
                    tempTarget.GetComponent<Level4bTargetAction>().gameController = this;
                }
                break;
            default:
                break;
        }
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

  

}
