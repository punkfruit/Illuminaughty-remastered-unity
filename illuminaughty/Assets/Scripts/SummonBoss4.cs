using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonBoss4 : MonoBehaviour
{
    //public GameObject
    public Transform[] spots;
    public Transform cameraMoveZone;
    public GameObject captureArea;
    public int areasCaptured, areasNeeded;
    public Animator anim, shiaAnim, bossExitAnim;
    public Shia shia;
    public GameObject fightCanvas;
    public string bossName;
    public int summonSND;

    public bool trigger = false, lastAreaCaptured;

    public float timeBetweenSpotSpawns;
    private float counter;
    // Start is called before the first frame update
    void Start()
    {
        counter = timeBetweenSpotSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastAreaCaptured)
        {
            if(counter >= 0)
            {
                counter -= Time.deltaTime;

                if(counter <= 0)
                {
                    lastAreaCaptured = false;
                    counter = timeBetweenSpotSpawns;
                    NewArea();
                }
            }
        }

        if (trigger)
        {
            NewArea();
            trigger = false;
        }
    }

    public void CaptureArea()
    {
        areasCaptured++;
        UIcontroller.instance.outOf.text = areasCaptured.ToString() + "/" + areasNeeded.ToString();
        if (areasCaptured >= areasNeeded)
        {
            AllAreasCaptured();
        }
        else
        {
            lastAreaCaptured = true;
        }
    }

    public void NewArea()
    {
        int spot = Random.Range(0, spots.Length);
        Instantiate(captureArea, spots[spot].transform);
    }

    public void AllAreasCaptured()
    {
        shia.Win();
        UIcontroller.instance.outOf.gameObject.SetActive(false);
        bossExitAnim.SetTrigger("blow");
    }

    public void Initialize()
    {
        AudioManager.instance.StopAllMusic();
        PlayerController.instance.canMove = false;
        UIcontroller.instance.gameObject.SetActive(false);
        UIcontroller.instance.outOf.text = areasCaptured.ToString() + "/" + areasNeeded.ToString();
        UIcontroller.instance.bossHealthSlider.maxValue = shia.maxHealth;
        UIcontroller.instance.bossHealthSlider.value = UIcontroller.instance.bossHealthSlider.maxValue;
        UIcontroller.instance.bossName.text = bossName;
        UIcontroller.instance.bossHealthText.text = UIcontroller.instance.bossHealthSlider.value + " / " + UIcontroller.instance.bossHealthSlider.maxValue;
        CameraController.instance.cameraMoveSpeed = 2f;
        CameraController.instance.SetGetCameraFollowPositionFunc(() => cameraMoveZone.position);
        anim.SetTrigger("summon");
    }

    public void ShowFightCan()
    {
        AudioManager.instance.PlayMainMusic(2);
        AudioManager.instance.constMusic = true;
        Instantiate(fightCanvas);
        anim.enabled = false;
    }

    public void SummonEnd()
    {


        UIcontroller.instance.gameObject.SetActive(true);
        UIcontroller.instance.bossHealthOBJ.SetActive(true);
        UIcontroller.instance.outOf.gameObject.SetActive(true); //boss4 specific
        PlayerController.instance.canMove = true;
        CameraController.instance.cameraMoveSpeed = 2000f;
        CameraController.instance.SetGetCameraFollowPositionFunc(() => PlayerController.instance.cameraTarget.position);
        //colider.enabled = false;
        anim.enabled = false;
        shiaAnim.enabled = true;
        if (shia != null)
        {
            shia.active = true;
            // shia.anim.SetTrigger("spin");
            shia.trigger = true;
        }

        NewArea();
    }

    public void PlaySND()
    {
        AudioManager.instance.PlaySFXnormal(summonSND);
    }
}
