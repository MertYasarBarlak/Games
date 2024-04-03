using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider healthBar;
    public Text scoreText;
    public int health;
    public int maxHealth;
    public int score;

    void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score.ToString();

        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.minValue = 0;
        healthBar.value = health;
    }

    public void UpdateScore(int updateScore)
    {
        score += updateScore;
        scoreText.text = "Score: " + score.ToString();

        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void UpdateHealth(int updateHealt)
    {
        health += updateHealt;
        healthBar.value = health;
    }
}