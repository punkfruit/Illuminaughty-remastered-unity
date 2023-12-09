using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public bool disablePlayer, playSound;
    public string sceneToLoad;
    public Vector3 nextScenePlayerPos;
    public int soundToPlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            UIcontroller.instance.ShowLoadingScreen();
            PlayerController.instance.canMove = false;
            //LevelManager.instance.SetPlayerPos(nextScenePlayerPos.x, nextScenePlayerPos.y);
            LevelManager.instance.tempPos = nextScenePlayerPos;
            LevelManager.instance.LoadNextScene(sceneToLoad);

            if (disablePlayer)
            {
                PlayerController.instance.gameObject.SetActive(false);
            }
            if (playSound)
            {
                AudioManager.instance.PlaySFX(soundToPlay);
            }
        }
    }
}
