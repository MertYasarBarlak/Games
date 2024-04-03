using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IGMenuManager : MonoBehaviour
{
    public GameObject inGameMenu;
    public GameObject pauseMenu;
    public GameObject gameOver;
    public Text HighScoreText;
    public Text HighScoreText2;

    private void Start()
    {
        HighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
        HighScoreText2.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void PauseButton()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        inGameMenu.SetActive(false);
    }

    public void ResumeButton()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        inGameMenu.SetActive(true);
    }

    public void HomeButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void RestartButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOver.SetActive(true);
        inGameMenu.SetActive(false);
    }
}
