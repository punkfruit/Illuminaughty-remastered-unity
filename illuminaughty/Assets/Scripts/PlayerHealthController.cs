using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth;
    public int maxHealth;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
    {
        //currentHealth = maxHealth;

        UIcontroller.instance.healthSlider.maxValue = maxHealth;
        UIcontroller.instance.healthSlider.value = currentHealth;
        UIcontroller.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    public void Load()
    {

    }

    public void DamagePlayer(int damage)
    {
        //temp invincibility a possible fix??
        currentHealth -= damage;
        AudioManager.instance.PlaySFX(16);

        if(currentHealth <= 0)
        {
            PlayerController.instance.gameObject.SetActive(false);
            UIcontroller.instance.gameObject.SetActive(true);
            UIcontroller.instance.deathScreen.SetActive(true);
            UIcontroller.instance.outOf.gameObject.SetActive(false);
            MouseCursor.instance.on = false;
        }


        UIcontroller.instance.healthSlider.value = currentHealth;
        UIcontroller.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    public void HealPlayer(int amount)
    {
        currentHealth += amount;
        if(currentHealth > maxHealth) { currentHealth = maxHealth; }
        UIcontroller.instance.healthSlider.value = currentHealth;
        UIcontroller.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
