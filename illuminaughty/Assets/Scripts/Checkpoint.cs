using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public Collider2D colid;
    public string levelToSet;
    public float playerPosx, playerPosy;

    public bool turnOn = true;
    public int music;
    // Start is called before the first frame update


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (turnOn)
            {
                PlayerPrefs.SetFloat("PlayerPos_X", playerPosx);
                PlayerPrefs.SetFloat("PlayerPos_Y", playerPosy);

                PlayerPrefs.SetInt("SCORE", ScoreController.instance.score);
                PlayerPrefs.SetFloat("SCORE_TO_LEVEL", ScoreController.instance.scoreToLevel);
                PlayerPrefs.SetInt("LEVEL", ScoreController.instance.level);
                PlayerPrefs.SetInt("HEALTH", PlayerHealthController.instance.currentHealth);
                PlayerPrefs.SetInt("MAX_HEALTH", PlayerHealthController.instance.maxHealth);
                PlayerPrefs.SetString("LAST_SCENE", levelToSet);
                PlayerPrefs.SetInt("CurrentMusic", music);

                //GeneralManager.instance.checkpointReached = true;
                PlayerPrefs.SetString("CHECKPOINT", "true");
                colid.enabled = false;
            }
            else
            {
                //GeneralManager.instance.checkpointReached = false;
                PlayerPrefs.SetString("CHECKPOINT", "false");
                colid.enabled = false;
            }
           
        }
    }
}
