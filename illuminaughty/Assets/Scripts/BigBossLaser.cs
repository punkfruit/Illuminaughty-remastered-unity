using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossLaser : MonoBehaviour
{

    public SpriteRenderer fill; //full 0.75, 1.5 empty 0.75, 0.1 
    private Vector2 filled = new Vector2(0.75f, 1.5f), empty = new Vector2(0.75f, 0.1f);
    private Vector2 shortt = new Vector2(1f, 0.1f), longg = new Vector2(1f, 7.26f), holeLength = new Vector2(1f, 10f);
    private Vector2 bigShort = new Vector2(2.125f, 0.1f), bigLong = new Vector2(2.125f, 12.415f);
    public float fillSpeed, drainSpeed, fireSpeed, fireDuration, counter2;
    private float counter;
    public SpriteRenderer LaserSR;//HOLE = 10
    public Sprite mainLaser, cutLaser, big, bigCut;
    public GameObject obfht, whiteFade;
    public Color red, green;
    public bool full, drain, on, makeHolee, shrinkHole;

    public int damage, sfx;
    private bool fired = false;
    public KonyFace kony;

    public Button[] checks;
    private bool fireing;

    public Collider2D col;

    public bool toggle;
    public bool multiBool
    {
        get
        {
            for (int i = 0; i < checks.Length; i++)
            {
                if (!checks[i].poweredOn)
                {
                    return false;
                }
            }
            return true;
        }
    }
    void Start()
    {
        col.enabled = false;
        fill.size = empty;
        obfht.SetActive(false);
        LaserSR.size = shortt;
    }

    private void Update()
    {
        if (toggle)
        {
            MakeHole();
            toggle = false;
        }
        
        
        if (on && !full && multiBool)
        {
            FillMeter();
        }

        if (drain && fired) 
        {
            DrainMeter();
        }

        if(counter > 0)
        {
            counter -= Time.deltaTime;
            if(counter <= 0)
            {
                fireing = false;
            }
        }

        if (on)
        {
            if (fireing)
            {
                LaserSR.size = Vector2.MoveTowards(LaserSR.size, longg, fireSpeed * Time.deltaTime);
                if (LaserSR.size == longg)
                {
                    LaserSR.sprite = cutLaser;
                    obfht.SetActive(true);
                    kony.TakeDamage(damage, true);
                }
            }
            else
            {
                LaserSR.size = Vector2.MoveTowards(LaserSR.size, shortt, fireSpeed * Time.deltaTime);
                LaserSR.sprite = mainLaser;
                obfht.SetActive(false);
                if (LaserSR.size == shortt)
                {
                    if (makeHolee)
                    {
                        on = false;
                    }

                    col.enabled = false;
                    drain = true;

                    //drain = true;
                }
            }
        }
       

        if(makeHolee && counter2 > 0)
        {
            counter2 -= Time.deltaTime;
            if(counter2 <= 0)
            {
                shrinkHole = true;
            }
        }

        if (makeHolee && !shrinkHole)
        {
            LaserSR.sprite = big;
            LaserSR.size = Vector2.MoveTowards(LaserSR.size, bigLong, fireSpeed * Time.deltaTime);
            if(LaserSR.size == bigLong)
            {
                LaserSR.sprite = bigCut;
            }
        }
        else if (makeHolee && shrinkHole)
        {
            LaserSR.sprite = big;
            LaserSR.size = Vector2.MoveTowards(LaserSR.size, bigShort, fireSpeed * Time.deltaTime);
            if (LaserSR.size == bigShort)
            {
                LaserSR.enabled = false;
            }
        }

       
    }

    private void FillMeter()
    {
        fill.size = Vector2.MoveTowards(fill.size, filled, fillSpeed * Time.deltaTime);
        if(fill.size == filled)
        {
            full = true;
            fill.color = green;
        }
    }

    private void DrainMeter()
    {
        fill.color = red;
        fill.size = Vector2.MoveTowards(fill.size, empty, drainSpeed * Time.deltaTime);
        if (fill.size == empty)
        {
            full = false;
            drain = false;
            fired = false;

            for(int i = 0; i < checks.Length; i++)
            {
                checks[i].ButtUp();
            }
        }
    }

    public void Fire()
    {
        if (full && !fireing)
        {
            LaserSR.sprite = mainLaser;
            fireing = true;
            counter = fireDuration;
            AudioManager.instance.PlaySFX(sfx);
            fired = true;

            col.enabled = true;
            //extend laser with fire speed, enable obfht, keep it extended for a short time(fire duration), than retract
        }
    }

    public void MakeHole()
    {
        
        Instantiate(whiteFade);
        makeHolee = true;
        AudioManager.instance.PlaySFX(sfx);
    }
}
