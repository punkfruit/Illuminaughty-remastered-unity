using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScienceDoor : MonoBehaviour
{
    [Header("tech stuff")]
    public GameObject doorLeft;
    public GameObject doorRight;
    public Transform leftOpen, rightOpen;
    public Vector3 ogLeft, ogRight;
    public float speed, close;
    public bool open;

    private void Start()
    {
        ogLeft = doorLeft.transform.position;
        ogRight = doorRight.transform.position;
    }
    private void Update()
    {
        if (open)
        {
            doorLeft.transform.position = Vector3.MoveTowards(doorLeft.transform.position, leftOpen.position, speed * Time.deltaTime);
            doorRight.transform.position = Vector3.MoveTowards(doorRight.transform.position, rightOpen.position, speed * Time.deltaTime);
        }
        else
        {
            doorLeft.transform.position = Vector3.MoveTowards(doorLeft.transform.position, ogLeft, close * Time.deltaTime);
            doorRight.transform.position = Vector3.MoveTowards(doorRight.transform.position, ogRight, close * Time.deltaTime);
        }
    }
    public void OpenThis()
    {
        Vector3.MoveTowards(doorLeft.transform.position, leftOpen.position, 10);
        Debug.Log("opened");
    }
}
