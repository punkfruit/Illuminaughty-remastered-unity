using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossSummoner : MonoBehaviour
{
    public Animator anim;
    public BoxCollider2D colider;
    public string bossName;
    public int bossHealth;
    public Transform cameraMoveZone;
    public BossController boss;
    public GameObject fightCanvas;
    public int summonSound;
    public bool initialized;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space)){
            if (initialized && PlayerPrefs.GetString("SeenSummonBoss1") == "true")
            {
                boss.transform.position = (new Vector3(22.09f, 26.63f, 0f));
                AudioManager.instance.StopSFX(summonSound);
                SummonEnd();
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
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
        initialized = true;
        AudioManager.instance.StopAllMusic();
        AudioManager.instance.PlaySFXnormal(summonSound);
        UIcontroller.instance.bossHealthSlider.maxValue = bossHealth;
        UIcontroller.instance.bossHealthSlider.value = UIcontroller.instance.bossHealthSlider.maxValue;
        UIcontroller.instance.bossName.text = bossName;
        UIcontroller.instance.bossHealthText.text = UIcontroller.instance.bossHealthSlider.value + " / " + UIcontroller.instance.bossHealthSlider.maxValue;
        PlayerController.instance.canMove = false;
        UIcontroller.instance.gameObject.SetActive(false);
        CameraController.instance.cameraMoveSpeed = 2f;
        CameraController.instance.SetGetCameraFollowPositionFunc(() => cameraMoveZone.position);
        anim.SetTrigger("summon");

    }

    public void SummonEnd()
    {
        PlayerPrefs.SetString("SeenSummonBoss1", "true");
        initialized = false;
        UIcontroller.instance.gameObject.SetActive(true);
        UIcontroller.instance.bossHealthOBJ.SetActive(true);
        PlayerController.instance.canMove = true;
        CameraController.instance.cameraMoveSpeed = 2000f;
        CameraController.instance.SetGetCameraFollowPositionFunc(() => PlayerController.instance.cameraTarget.position);
        colider.enabled = false;
        anim.enabled = false;
        if(boss != null)
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
}
