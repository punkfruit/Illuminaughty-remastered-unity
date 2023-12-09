using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed, delayAmount;
    private float counter;
    public int damageToDeal;
    public Vector3 direction;
    public GameObject impactEffect;
    public Transform center;
    public bool playSound, destroySound, rotateToPlayer, aimTowardPlayer = true, delay;
    public int soundToPlay, destroySoundToPlay;

    private SpriteRenderer theSR;



    // Start is called before the first frame update
    private void Awake()
    {
        theSR = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        if (aimTowardPlayer)
        {
            direction = PlayerController.instance.transform.position - transform.position;
            direction.Normalize();
            if (playSound)
            {
                AudioManager.instance.PlaySFX(soundToPlay);
            }
        }

        if (delay)
        {
            counter = delayAmount;
            theSR.enabled = false;
        }

        if (rotateToPlayer)
        {
            Vector2 offset = new Vector2(PlayerController.instance.transform.position.x - transform.position.x, PlayerController.instance.transform.position.y - transform.position.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (delay)
        {
            counter -= Time.deltaTime;

            if(counter <= 0)
            {
                delay = false;
                theSR.enabled = true;
            }
        }


        if (!delay)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer(damageToDeal);
        }
        if (destroySound)
        {
            AudioManager.instance.PlaySFX(destroySoundToPlay);
        }

        Destroy(gameObject);
        Instantiate(impactEffect, center.position, transform.rotation);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (transform.parent != null) // if object has a parent
        {
            if (transform.parent.childCount <= 1) // if this object is the last child
            {
                Destroy(transform.parent.gameObject, 0.1f); // destroy parent a few frames later
            }
        }
    }
}
