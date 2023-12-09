using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStarter : MonoBehaviour
{
    public bool ChapterStart;
    void Start()
    {
        if (!PlayerController.instance.gameObject.activeInHierarchy)
        {
            PlayerController.instance.gameObject.SetActive(true);
        }
        if (!UIcontroller.instance.gameObject.activeInHierarchy)
        {
            UIcontroller.instance.gameObject.SetActive(true);
        }
        UIcontroller.instance.HideLoadingScreen();
        PlayerController.instance.canMove = true;
        LevelManager.instance.isPaused = false;

        //if (!ChapterStart || GeneralManager.instance.checkpointReached == false)
        //{
        //    PlayerController.instance.gameObject.transform.position = LevelManager.instance.tempPos;
        //}

        CameraManager.instance.active = true;
        CameraController.instance.SetGetCameraFollowPositionFunc(() => PlayerController.instance.cameraTarget.position);

        if (ChapterStart)
        {
            return;
        }

        if (GeneralManager.instance.checkpointReached())
        {
            return;
        }

        PlayerController.instance.gameObject.transform.position = LevelManager.instance.tempPos;

    }
}
