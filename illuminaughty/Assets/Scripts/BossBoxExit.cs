using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBoxExit : MonoBehaviour
{

    public void DisableAnimator()
    {
        Destroy(gameObject);
    }
}
