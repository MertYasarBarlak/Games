using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject contributorsMenu;
    public Text HighScoreText;

    private void Start()
    {
        Time.timeScale = 1.0f;
        HighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void PlayButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    public void ContributorsButton()
    {
        contributorsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
