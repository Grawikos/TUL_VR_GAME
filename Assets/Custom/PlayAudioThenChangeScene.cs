using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayAudioThenChangeScene : MonoBehaviour
{
    public AudioSource audioSource; // Assign in Inspector
    public string sceneToLoad; // Set the target scene name in Inspector
    public float delayAfterAudio = 0.5f; // Extra delay after audio finishes

    public void PlayAndChangeScene()
    {
        if (audioSource != null)
        {
            audioSource.Play(); // Play the audio
            StartCoroutine(WaitAndLoadScene()); // Start coroutine
        }
        else
        {
            Debug.LogError("AudioSource not assigned!");
            LoadScene(); // Fallback: Load scene immediately if no audio
        }
    }

    private IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(audioSource.clip.length + delayAfterAudio);
        LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
