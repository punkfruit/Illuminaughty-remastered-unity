using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButPlayArmRot : MonoBehaviour
{
    public BattleCanvas bat;
    public int deg;

    private void OnEnable()
    {
        bat.TempGunArmRotate(deg);
        this.enabled = false;

        //Debug.Log("succcc");
    }

    public void testtt()
    {

    }

}
