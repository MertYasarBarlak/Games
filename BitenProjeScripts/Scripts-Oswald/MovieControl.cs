using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MovieControl : MonoBehaviour
{
    public int sceneNumber;
    float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (!GetComponent<VideoPlayer>().isPlaying && timer >= 3f) SceneManager.LoadScene(sceneNumber);
    }
}
