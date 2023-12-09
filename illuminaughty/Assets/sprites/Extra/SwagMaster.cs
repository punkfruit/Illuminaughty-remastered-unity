using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwagMaster : MonoBehaviour
{
    public Transform swag;
    public Camera theCam;
    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
        UIcontroller.instance.gameObject.SetActive(false);
        CameraController.instance.SetGetCameraFollowPositionFunc(() => transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);

        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        swag.rotation = Quaternion.Euler(0, 0, angle);
    }
}
