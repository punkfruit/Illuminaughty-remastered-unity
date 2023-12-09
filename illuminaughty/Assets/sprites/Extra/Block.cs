using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Transform playerTra;
    public float moveSpeed;
    public float radiusForBlock = 5;
    public float radiusToIgnore = 1;
    public Rigidbody2D theRB;

    public SpriteRenderer beamSR;
    public GameObject beamGO;

    //line
   


    private void Start()
    {
        playerTra = PlayerController.instance.transform;

        beamSR.drawMode = SpriteDrawMode.Sliced;
        //line
       
    }


    public void MoveTowardPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTra.position, moveSpeed * Time.deltaTime);
        theRB.velocity = Vector2.zero;

        //line
        //beamGO.transform.position = Vector3.Distance(transform.position, playerTra.position;
        

    }

    private void Update()
    {
      
        if (Input.GetButton("Fire2"))
        {
            if(Vector3.Distance(transform.position, playerTra.position) <= radiusForBlock && Vector3.Distance(transform.position, playerTra.position) >= radiusToIgnore)
            {
                MoveTowardPlayer();
            }

            if(Vector3.Distance(transform.position, playerTra.position) <= radiusForBlock)
            {
                beamSR.enabled = true;
                beamSR.size = new Vector2(Vector3.Distance(transform.position, playerTra.position) * 2, 1);
            }
            else
            {
                beamSR.enabled = false;
            }
        }
        else
        {
            beamSR.enabled = false;
        }

        Vector2 offset = new Vector2(PlayerController.instance.transform.position.x - beamGO.transform.position.x, PlayerController.instance.transform.position.y - beamGO.transform.position.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        beamGO.transform.rotation = Quaternion.Euler(0, 0, angle);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusForBlock);
    }
}
