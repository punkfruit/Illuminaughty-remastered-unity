using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;

    public int level = 1;
    public int score;
    public int displayScore, healthMultiplier;
    public float scoreToLevel;
    public float scoreMultiplier, incrementSpeed;
    public GameObject levelGraphic;


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
        
        displayScore = score;

        UIcontroller.instance.levelText.text = "Level: " + level;
        UIcontroller.instance.scoreSlider.maxValue = scoreToLevel;
        UIcontroller.instance.scoreSlider.value = score;
        UIcontroller.instance.scoreText.text = score.ToString() + " / " + scoreToLevel.ToString("F0");

        StartCoroutine(ScoreUpdater());
    }

    private IEnumerator ScoreUpdater()
    {
        while (true)
        {
            if (displayScore < score)
            {
                displayScore++; //Increment the display score by 1
                UIcontroller.instance.scoreText.text = displayScore.ToString() + " / " + scoreToLevel.ToString("F0"); //Write it to the UI
                UIcontroller.instance.scoreSlider.value = displayScore;
            }
            yield return new WaitForSeconds(incrementSpeed); // I used .2 secs but you can update it as fast as you want
        }
    }

    public void AddToScore(int scoreToAdd)
    {
        score = score + scoreToAdd;
        if(score >= scoreToLevel)
        {
            score -= score;
            displayScore = 0;
            level++;
            PlayerHealthController.instance.maxHealth = PlayerHealthController.instance.maxHealth + healthMultiplier;
            PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;

            scoreToLevel = scoreToLevel * scoreMultiplier;

            UIcontroller.instance.levelText.text = "Level: " + level;
            UIcontroller.instance.scoreText.text = displayScore.ToString() + " / " + scoreToLevel.ToString("F0"); //Write it to the UI
            UIcontroller.instance.scoreSlider.value = displayScore;
            UIcontroller.instance.scoreSlider.maxValue = scoreToLevel;
            UIcontroller.instance.healthSlider.maxValue = PlayerHealthController.instance.maxHealth;
            UIcontroller.instance.healthSlider.value = PlayerHealthController.instance.currentHealth;
            UIcontroller.instance.healthText.text = PlayerHealthController.instance.currentHealth.ToString() + " / " + PlayerHealthController.instance.maxHealth.ToString();

            GameObject graphic = Instantiate(levelGraphic);
        }

       
        //UIcontroller.instance.scoreSlider.value = score;
       // UIcontroller.instance.scoreText.text = score.ToString() + " / " + scoreToLevel.ToString();
    }

   
}
