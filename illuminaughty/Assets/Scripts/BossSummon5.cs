using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummon5 : MonoBehaviour
{

    public Collider2D colid;
    public Animator anim;
    public Transform cameraMoveSpot, playerMoveSpot;
    public GameObject battleCanvas, fightCanvas, rainGen;
    public bool movePlayer;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movePlayer)
        {
            PlayerController.instance.transform.position = Vector3.MoveTowards(PlayerController.instance.transform.position, playerMoveSpot.position, speed * Time.deltaTime);
            PlayerController.instance.anim.SetBool("isMoving", true);

            if(PlayerController.instance.transform.position == playerMoveSpot.transform.position)
            {
                movePlayer = false;
                PlayerController.instance.anim.SetBool("isMoving", false);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Initialize();
            movePlayer = true;
        }
    }

    public void Initialize()
    {
        //AudioManager.instance.StopAllMusic();
        AudioManager.instance.PlayMainMusic(7);
        PlayerController.instance.canMove = false;
        UIcontroller.instance.gameObject.SetActive(false);
        CameraController.instance.cameraMoveSpeed = 2f;
        CameraController.instance.SetGetCameraFollowPositionFunc(() => cameraMoveSpot.position);
        anim.SetTrigger("summon");

        rainGen.SetActive(true);
        colid.enabled = false;
    }

    public void ShowFightCan()
    {
        //AudioManager.instance.PlayMainMusic(2);
        AudioManager.instance.constMusic = true;
        Instantiate(fightCanvas);
        anim.enabled = false;
    }

    public void summonEnd()
    {
        battleCanvas.SetActive(true);
    }
}
