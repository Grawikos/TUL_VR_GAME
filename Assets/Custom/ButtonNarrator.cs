using UnityEngine;

public class ButtonNarrator : MonoBehaviour
{
    public AudioSource narrationAudio;

    public void PlayNarration()
    {
        if (narrationAudio != null)
        {
            narrationAudio.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource not assigned!");
        }
    }
}