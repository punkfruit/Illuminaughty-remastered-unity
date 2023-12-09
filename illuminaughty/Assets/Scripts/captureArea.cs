using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class captureArea : MonoBehaviour
{
    private int colorNumb;
    public SpriteRenderer mainArea, flag, pole;
    public Transform flagCenter;
    public PlayerController player;

    public Color[] colors;

    public bool test = false;
    public bool trigger = false, initialArea = false;

    public float percent, speed, rate, decreaseRate;
    public Slider slider;
    public Image sliderImage;
    public SummonBoss4 boss;

   // [Header("test stuff")]
   // public int test1, test2;

    void Start()
    {
        //player = FindObjectOfType<PlayerController>();
        boss = FindObjectOfType<SummonBoss4>();

        colorNumb = Random.Range(0, colors.Length);

        mainArea.color = colors[colorNumb];
        sliderImage.color = colors[colorNumb];
    }

    void Update()
    {
        if(player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }

        if(player.transform.position.y >= flagCenter.position.y)
        {
            flag.sortingLayerName = "Shooting";
            flag.sortingOrder = 6;
            pole.sortingLayerName = "Shooting";
            pole.sortingOrder = 6;
        }
        else
        {
            flag.sortingLayerName = "Player";
            flag.sortingOrder = 4;
            pole.sortingLayerName = "Player";
            pole.sortingOrder = 4;
        }

        if (test)
        {
            if(percent <= 100)
            {
                percent += rate * speed * Time.deltaTime;
            }
        }
        else
        {
            if(percent >= 0)
            {
                percent -= decreaseRate * speed * Time.deltaTime;
            }
        }

        slider.value = percent;
        if(percent >= 100)
        {
            BarFull();
        }

        /*
        if (trigger)
        {
            UIcontroller.instance.outOf.gameObject.SetActive(true);
            UIcontroller.instance.outOf.text =  test1.ToString() + "/" + test2.ToString();
            trigger = false;
        } */
    }


    public void BarFull()
    {
        if (initialArea)
        {
            //Debug.Log("test");
            boss.Initialize();
            Destroy(gameObject);

            return;
        }

        Destroy(gameObject);
        boss.CaptureArea();
        
    }



    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            test = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            test = false;
        }
    }

}
