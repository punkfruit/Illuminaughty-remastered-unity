using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummon2 : MonoBehaviour
{
    public Animator anim;
    public BoxCollider2D colider;
    public string bossName;
    public int bossHealth;
    public Transform cameraMoveZone;
    public GameObject fightCanvas;
    public int summonSound;
    public KonyMain boss;

    public Button doorButton;
    public BigBossLaser laser;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Initialize();
        }
    }

    public void DeactivateAnimator()
    {
        anim.enabled = false;
    }

    public void Initialize()
    {
        AudioManager.instance.StopAllMusic();
        //AudioManager.instance.PlaySFXnormal(summonSound);
        UIcontroller.instance.bossHealthSlider.maxValue = bossHealth;
        UIcontroller.instance.bossHealthSlider.value = UIcontroller.instance.bossHealthSlider.maxValue;
        UIcontroller.instance.bossName.text = bossName;
        UIcontroller.instance.bossHealthText.text = UIcontroller.instance.bossHealthSlider.value + " / " + UIcontroller.instance.bossHealthSlider.maxValue;
        PlayerController.instance.canMove = false;
        UIcontroller.instance.gameObject.SetActive(false);
        CameraController.instance.cameraMoveSpeed = 2f;
        CameraController.instance.SetGetCameraFollowPositionFunc(() => cameraMoveZone.position);
        doorButton.ButtUp();
        laser.on = true;
        anim.SetTrigger("summon");

    }

    public void SummonEnd()
    {


        UIcontroller.instance.gameObject.SetActive(true);
        UIcontroller.instance.bossHealthOBJ.SetActive(true);
        PlayerController.instance.canMove = true;
        CameraController.instance.cameraMoveSpeed = 2000f;
        CameraController.instance.SetGetCameraFollowPositionFunc(() => PlayerController.instance.cameraTarget.position);
        colider.enabled = false;
        anim.enabled = false;
        if (boss != null)
        {
            boss.active = true;
        }
    }

    public void ShowFightCan()
    {
        AudioManager.instance.PlayMainMusic(2);
        AudioManager.instance.constMusic = true;
        Instantiate(fightCanvas);
        anim.enabled = false;
    }

    public void PlaySummonSound()
    {
        AudioManager.instance.PlaySFXnormal(summonSound);
    }
}
