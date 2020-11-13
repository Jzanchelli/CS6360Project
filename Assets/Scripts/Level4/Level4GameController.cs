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
    private System.TimeSpan time;
    private System.DateTime startTime;
    private IEnumerator coroutine;
    private GameObject target1;
    private GameObject target2;
    private bool isDisqualified;
    private bool isStart;

    // Start is called before the first frame update
    void Start()
    {
        this.subLevel = 0;
        this.isDisqualified = false;
        this.isStart = false;

    }

    // Update is called once per frame
    void Update()
    {
        this.CheckAndClearTargetContainer1();
        this.CheckAndClearTargetContainer2();
        this.CheckAndClearTargetContainer3();
        if(targetContainer1.transform.childCount ==0 && targetContainer2.transform.childCount ==0 && targetContainer3.transform.childCount == 0)
        {
            this.isStart = false;
            text.text = "Time: " + time.Seconds + "." + time.Milliseconds;
        }
        if (isDisqualified)
        {
            text.text = "Early Shot";
        }
        else
        {
            if (isStart)
            {
                time = System.DateTime.Now - startTime;
                text.text = "Time: " + time.Seconds + "." +time.Milliseconds;
            }
        }
        
        
    }

    public void StartLevel()
    {
        switch (subLevel)
        {
            case 0:
                SubLevel1();
                break;
            case 1:
                SubLevel2();
                break;
            case 2:
                SubLevel3();
                break;
            case 3:
                SubLevel4();
                break;
            default:
                break;
        }
    }

    public void CheckAndClearTargetContainer1()
    {
        if (this.targetContainer1.transform.childCount > 0)
        {
            if (this.targetContainer1.transform.GetChild(0).transform.childCount < 2)
            {
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
                Destroy(this.targetContainer3.transform.GetChild(0).gameObject);
            }
        }
    }

    public void PreviousLevel()
    {
        if (subLevel > 0)
        {
            subLevel--;
        }
    }

    public void NextLevel()
    {
        subLevel++;
    }

    bool weaponsHolstered()
    {
        GameObject[] weapons = GameObject.FindGameObjectsWithTag("weapon");
        for (int i = 0; i < weapons.Length; i++)
        {
            if(weapons[i].transform.parent.tag != "holster")
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
        target1 = Instantiate(target, targetContainer1.transform.position, Quaternion.identity);
        target1.transform.localScale = new Vector3(5f, 5f, 0.2514884f);
        target1.transform.SetParent(targetContainer1.transform);
        TargetMovement tmovement = target1.transform.GetChild(0).GetComponent<TargetMovement>();
        tmovement.SetSpeed(0);
        AudioProgression();
    }

    void SubLevel3()
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
        }
        this.audioSource.PlayOneShot(this.drawAudio);
        text.text = "hey";
        this.isStart = true;
        this.startTime = System.DateTime.Now;
        
    }

    
}
