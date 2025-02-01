using UnityEngine;
using System.Collections;
using UnityEngine.Video;

public class FreezeVideoOnClick : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign your VideoPlayer (the sphere)
    
    public void OnButtonPress()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Pause(); // Freezes the video
        }
    }
}

