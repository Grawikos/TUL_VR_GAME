using UnityEngine;
using System.Collections;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEndSceneChanger : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Assign the VideoPlayer component
    public string sceneToLoad;       // Set the next scene name in the Inspector

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnMovieFinished; // Detect when video ends
        }
    }

    void OnMovieFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(sceneToLoad); // Change scene when video ends
    }
}
