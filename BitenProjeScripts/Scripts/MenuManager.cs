using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void RestartButton()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentSceneName);
    }
}
