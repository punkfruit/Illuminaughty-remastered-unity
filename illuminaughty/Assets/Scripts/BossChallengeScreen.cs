using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossChallengeScreen : MonoBehaviour
{

    public Animator anim;
    public BossSummoner bossSummon;
    public BossSummon2 bosum2;
    public BossSummon3 bosum3;
    public SummonBoss4 bosum4;
    public BossSummon5 bosum5;
    public BossSummon6 bosum6;

    private void Start()
    {
        bossSummon = FindObjectOfType<BossSummoner>();
        bosum2 = FindObjectOfType<BossSummon2>();
        bosum3 = FindObjectOfType<BossSummon3>();
        bosum4 = FindObjectOfType<SummonBoss4>();
        bosum5 = FindObjectOfType<BossSummon5>();
        bosum6 = FindObjectOfType<BossSummon6>();
    }


    public void SelfDestruct()
    {
        Destroy(gameObject);

        if(bossSummon != null)
        {
            bossSummon.SummonEnd();
        }

        if(bosum2 != null)
        {
            bosum2.SummonEnd();
        }
        
        if(bosum3 != null)
        {
            bosum3.SummonEnd();
        }

        if(bosum4 != null)
        {
            bosum4.SummonEnd();
        }

        if(bosum5 != null)
        {
            bosum5.summonEnd();
        }

        if(bosum6 != null)
        {
            bosum6.SummonEnd();
        }
    }
}
