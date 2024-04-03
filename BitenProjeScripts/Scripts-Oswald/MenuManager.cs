using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Slider healthBar;
    public GeneralHealth playerHealth;

    public Text ammoText;
    public PlayerController player;

    public Text proofText;
    public PlayerProofManager proof;

    public GameObject inGameMenu;
    public GameObject pauseMenu;

    void Start()
    {
        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.minValue = 0;
        healthBar.value = playerHealth.health;
    }

    void FixedUpdate()
    {
        healthBar.value = playerHealth.health;
        ammoText.text = " x " + player.ammo.ToString();
        proofText.text = " x " + proof.proofCount.ToString();
    }

    public void PauseButton()
    {
        inGameMenu.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeButton()
    {
        pauseMenu.SetActive(false);
        inGameMenu.SetActive(true);
        Time.timeScale = 1f;
    }
}