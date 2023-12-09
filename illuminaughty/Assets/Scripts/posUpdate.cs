using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class posUpdate : MonoBehaviour
{
    public string levelName;
    public Vector3 playerPosToSet;
    public Collider2D colid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")  
        {
            playerPosToSet = PlayerController.instance.transform.position;

            LevelManager.instance.SetPlayerPos(playerPosToSet.x, playerPosToSet.y);
            LevelManager.instance.tempPos = playerPosToSet;
            SaveLoad.instance.Save(levelName);

            colid.enabled = false;
        }
    }
}
