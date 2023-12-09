using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public bool active = true;
    public CameraController cameraFollow;
    public Transform playerTransform;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        if (active)
        {
            playerTransform = PlayerController.instance.cameraTarget;
            cameraFollow = CameraController.instance;
            cameraFollow.Setup(() => playerTransform.position);
        }
    }



    private void Update()
    {
        if (active)
        {
            if (playerTransform == null)
            {
                playerTransform = PlayerController.instance.cameraTarget;
                cameraFollow.Setup(() => playerTransform.position);
            }
            if (cameraFollow == null)
            {
                cameraFollow = CameraController.instance;
                cameraFollow.Setup(() => playerTransform.position);
            }
        }
    }


}
