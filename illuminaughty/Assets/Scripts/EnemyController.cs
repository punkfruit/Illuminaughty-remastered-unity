using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
    [Header("technical stuff")]
    public Rigidbody2D theRB;
    public SpriteRenderer theBody;
    public float moveSpeed;
    public bool explodeOnContact, basicCan, attackContact, projectile;

    public float rangeToChasePlayer, rangeToIgnorePlayer;
    public float rangeToAttack;
    public float rangeToShoot;
    public string key;

    [Header("Animation")]
    private Vector3 moveDirection;
    public Transform jointsun;
    public Animator anim;

    [Header("Death & Health info")]
    public GameObject[] deaths;
    public int health = 50;

    [Header("Attacking Player")]
    public GameObject attack;
    public Transform attacker;
    public int damageToGive;
    public int scoreValue;
    private bool inRange = false;
    public float attackCoolDown;
    private float counter;
    



    // Start is called before the first frame update
    void Start()
    {
        LevelManager.instance.enemiesInLevel.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.gameObject.activeInHierarchy && theBody.isVisible)
        {
            if (basicCan)
            {
                BasicCan();
            }

            if (attackContact)
            {
                AttackContact();
            }

            if (projectile)
            {
                Shoot();
            }
        }
       
        

        if (moveDirection != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    public void BasicCan()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
        {
            moveDirection = PlayerController.instance.transform.position - transform.position;
            jointsunFlip();
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        moveDirection.Normalize();

        theRB.velocity = moveDirection * moveSpeed;
    }

    public void Shoot()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer && Vector3.Distance(transform.position, PlayerController.instance.transform.position) > rangeToIgnorePlayer)
        {
            moveDirection = PlayerController.instance.transform.position - transform.position;
            jointsunFlip();
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        moveDirection.Normalize();

        theRB.velocity = moveDirection * moveSpeed;

        if (counter > 0)
        {
            counter -= Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= rangeToShoot)
        {
            inRange = true;

        }
        else
        {
            inRange = false;
        }

        if (inRange && counter <= 0)
        {
            Instantiate(attack, attacker.transform.position, attacker.transform.rotation);
            counter = attackCoolDown;
        }
    }

    public void AttackContact()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer && Vector3.Distance(transform.position, PlayerController.instance.transform.position) > rangeToAttack)
        {
            moveDirection = PlayerController.instance.transform.position - transform.position;
            jointsunFlip();
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        moveDirection.Normalize();

        theRB.velocity = moveDirection * moveSpeed;

        //rotate attacker.. hopefully
        Vector2 offset = new Vector2(PlayerController.instance.transform.position.x - attacker.transform.position.x, PlayerController.instance.transform.position.y - attacker.transform.position.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        attacker.transform.rotation = Quaternion.Euler(0, 0, angle);

        // if (counter > 0 && Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= rangeToAttack)
        // {

        if (counter > 0)
        {
            counter -= Time.deltaTime;
        }


        //  }

        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= rangeToAttack)
        {
            inRange = true;

        }
        else
        {
            inRange = false;
        }

        if (inRange && counter <= 0)
        {
            GameObject attac = Instantiate(attack, attacker.transform.position, attacker.transform.rotation);
            attac.transform.parent = attacker;
            counter = attackCoolDown;
        }
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;
        rangeToChasePlayer += 10;//should change to variable later
        rangeToShoot += 5;

        if(health <= 0)
        {
            int selectedDeath = Random.Range(0, deaths.Length);

            Instantiate(deaths[selectedDeath], transform.position, transform.rotation);
            Destroy(gameObject);
            LevelManager.instance.enemiesInLevel.Remove(this);
            ScoreController.instance.AddToScore(scoreValue);

            if(PlayerPrefs.GetString(key) != "true")
            {
                PlayerPrefs.SetString(key, "true");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && explodeOnContact)
        {
            PlayerHealthController.instance.DamagePlayer(damageToGive);
            int selectedDeath = Random.Range(0, deaths.Length);
            Instantiate(deaths[selectedDeath], transform.position, transform.rotation);
            Destroy(gameObject);
            LevelManager.instance.enemiesInLevel.Remove(this);
        }
    }

    public void jointsunFlip()
    {
        //make the sunglasses and weed look at player, roughly
        Vector3 playerpos = PlayerController.instance.transform.position;
        if (playerpos.x > transform.position.x)
        {
            jointsun.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            jointsun.localScale = Vector3.one;
        }
    }
}
