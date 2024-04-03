using UnityEngine;
using UnityEngine.SceneManagement;

public class Video_MenuManager : MonoBehaviour
{
    public GameObject videoControlMenu;
    public GameObject videoPlayer;

    public void MenuOpener()
    {
        if (videoControlMenu.activeSelf == true) videoControlMenu.SetActive(false);
        else videoControlMenu.SetActive(true);
    }

    public void SkipButton()
    {
        SceneManager.LoadScene(videoPlayer.GetComponent<MovieControl>().sceneNumber);
    }
}
