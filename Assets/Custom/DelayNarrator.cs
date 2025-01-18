using UnityEngine;

public class DelayNarrator : MonoBehaviour
{
    public AudioSource narrationAudio;
    public float delayInSeconds = 3f; // Delay before narration starts

    void Start()
    {
        if (narrationAudio != null)
        {
            Invoke("PlayNarration", delayInSeconds);
        }
        else
        {
            Debug.LogWarning("AudioSource not assigned!");
        }
    }

    void PlayNarration()
    {
        narrationAudio.Play();
    }
}