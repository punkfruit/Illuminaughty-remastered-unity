using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chap5Boss : MonoBehaviour
{
    public int id;
    public string namee;
    public bool isPlayer;
    public int health, maxHealth, defense, accuracy;
    public BattleCanvas battleCan;
    public Text nameSpace, healthSpace;

    public SpriteRenderer sprit;
    public bool isDead, myTurn, myTurnTrigger;
    public GameObject deathOBJ, button, healthSpaceOBJ;
    public Collider2D colid;
    public int scoreValue, healam;

    public string[] moves;
    public int[] moveID;
    public int selectedMove;

    public float mainTimer;
    private float mainCounter;
    public string turnPhrase, deathPhrase;
    public float timer;
    private float counter;

    public GameObject[] attacks;
    public GameObject gardenGateKey;
    public bool dropKey;
    // Start is called before the first frame update
    void Start()
    {
        if (isPlayer)
        {
            health = PlayerHealthController.instance.currentHealth;
            maxHealth = PlayerHealthController.instance.maxHealth;
        }
        else
        {
            health = maxHealth;
            nameSpace.text = namee;

            healthSpace.text = "<Health: " + health + "/" + maxHealth;
        }

        
    }

    private void Update()
    {
        if (myTurnTrigger)
        {

            if (isDead)
            {
                battleCan.ChangeTurn();
                myTurnTrigger = false;
            }
            else
            {
                myTurn = true;
                if (!isPlayer)
                {
                    counter = timer;
                    mainCounter = mainTimer;
                }
                else
                {
                    battleCan.raycastBlock.SetActive(false);
                }

                TextWritter.instance.AddWriter(battleCan.announceText, turnPhrase, .06f);
                myTurnTrigger = false;
            }
            
        }



        if (myTurn)
        {
            if(mainCounter > 0)
            {
                mainCounter -= Time.deltaTime;
                if(mainCounter <= 0)
                {
                    battleCan.ChangeTurn();
                }
            }

            if (counter > 0)
            {
                counter -= Time.deltaTime;
                if (counter <= 0)
                {
                    selectedMove = Random.Range(0, moveID.Length);
                    Turn();
                    //TextWritter.instance.AddWriter(battleCan.announceText, turnPhrase, .1f);
                }
            }
        }
    }

    public void Turn()
    {
        if (PlayerController.instance.gameObject.activeInHierarchy)
        {
            //if (isDead) { battleCan.ChangeTurn(); return; }
            Debug.Log("its " + namee + "turn");

            switch (moves[moveID[selectedMove]])
            {
                case "Tackle":
                    PlayerHealthController.instance.DamagePlayer(9);
                    //do stuff
                    break;
                case "Bash":
                    PlayerHealthController.instance.DamagePlayer(8);
                    //do stuff
                    break;
                case "Slice":
                    Instantiate(attacks[3], battleCan.bosses[0].transform.position, transform.rotation);
                    PlayerHealthController.instance.DamagePlayer(10);
                    //do stuff
                    break;
                case "Ember":
                    Instantiate(attacks[1], transform.position, transform.rotation);
                    //do stuff
                    break;
                case "String Shot":
                    Instantiate(attacks[6], transform.position, transform.rotation);
                    //do stuff
                    break;
                case "Water Sport":
                    Instantiate(attacks[8], transform.position, transform.rotation);
                    //do stuff
                    break;
                case "Heal Self":
                    Instantiate(attacks[5], transform.position, transform.rotation);
                    healam = Random.Range(10, 20);
                    Heal(healam);
                    //do stuff
                    break;
                case "Heal Other":
                    int toheal = Random.Range(1, battleCan.bosses.Length);
                    if(battleCan.bosses[toheal].isDead || toheal == id)
                    {
                        //toheal = Random.Range(1, battleCan.bosses.Length);
                        selectedMove = Random.Range(0, moveID.Length);
                        Turn();
                        return;
                    }
                    healam = Random.Range(10, 20);
                    battleCan.bosses[toheal].Heal(healam);
                    Instantiate(attacks[5], battleCan.bosses[toheal].transform.position, transform.rotation);
                    //do stuff
                    break;
                case "Dirt Throw":
                    Instantiate(attacks[4], battleCan.bosses[0].transform.position, transform.rotation);
                    PlayerHealthController.instance.DamagePlayer(6);
                    //do stuff
                    break;
                case "Bullet Seed":
                    Instantiate(attacks[0], transform.position, transform.rotation);
                    //do stuff
                    break;
                case "Leaf Blade":
                    Instantiate(attacks[2], transform.position, transform.rotation);
                    //do stuff
                    break;
                case "Needle Arm":
                    Instantiate(attacks[7], transform.position, transform.rotation);
                    //do stuff
                    break;
                default:
                    Debug.LogError(moves[moveID[selectedMove]] + " isnt a move");
                    break;
            }

            TextWritter.instance.AddWriter(battleCan.announceText, namee + " used " + moves[moveID[selectedMove]] + "!", .04f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bullet" || other.tag == "Laser")
        {
            DealDamage(other.GetComponent<PlayerBullet>().damageToGive);
        }
    }

    public void DealDamage(int dam)
    {
        health -= dam;
        if(isPlayer) { PlayerHealthController.instance.DamagePlayer(dam); }

        if (!isPlayer)
        {
            healthSpace.text = "<Health: " + health + "/" + maxHealth;
            if (health <= 0)
            {
                //Destroy(gameObject);
                sprit.enabled = false;
                isDead = true;
                Instantiate(deathOBJ, transform.position, transform.rotation);
                button.SetActive(false);
                healthSpaceOBJ.SetActive(false);
                colid.enabled = false;
                ScoreController.instance.AddToScore(scoreValue);

                if (dropKey)
                {
                    gardenGateKey.SetActive(true);
                }

                battleCan.BossKilled();
            }
        }
    }

    public void Heal(int healamount)
    {
        health += healamount;

        if(health > maxHealth)
        {
            health = maxHealth;
        }

        healthSpace.text = "<Health: " + health + "/" + maxHealth;
    }
}
