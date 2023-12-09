using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerPos : MonoBehaviour
{
    public Vector3 playerPosToSet;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            LevelManager.instance.SetPlayerPos(playerPosToSet.x, playerPosToSet.y);
            Destroy(gameObject);
        }
    }
}
