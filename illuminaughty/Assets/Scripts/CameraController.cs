using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public Transform target;
    public static CameraController instance;
    private Func<Vector3> GetCameraFollowPositionFunc;
    public float cameraMoveSpeed;


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
        //target = PlayerController.instance.cameraTarget;
        DontDestroyOnLoad(this);
    }

    public void Setup(Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }

    public void SetGetCameraFollowPositionFunc(Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }
    void Update()
    {
        // transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;

        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);

        if(distance > 0)
        {
            Vector3 newCameraPosition = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;

            float distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);

            if(distanceAfterMoving > distance)
            {
                //overshot target
                newCameraPosition = cameraFollowPosition;
            }

            transform.position = newCameraPosition;
        }

    }

    public void SelfDes()
    {
        Destroy(gameObject);
    }
}
