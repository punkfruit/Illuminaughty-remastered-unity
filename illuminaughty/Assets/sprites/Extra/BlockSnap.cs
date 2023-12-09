using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSnap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Block")
        {
            other.transform.position = transform.position;
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
