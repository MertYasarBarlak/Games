using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_MenuManager : MonoBehaviour
{
    public void PlayButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}