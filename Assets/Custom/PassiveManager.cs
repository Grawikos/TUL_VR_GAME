using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMover : MonoBehaviour
{
    public Transform player;         // Reference to the player object (e.g., VR rig or camera)
    public Transform positionA;      // Position inside SphereA
    public Transform positionB;      // Position inside SphereB

    public Image fadeImage;          // Reference to the fade image
    public float fadeDuration = 1f;  // Duration of the fade effect

    public AudioSource introductionAudio; // The introduction narration
    public AudioSource[] hurryRecordings; // Array of hurry-up recordings
    public float delayBetweenHurryRecordings = 10f; // Delay between hurry recordings
    public float lingerTimeAfterRecordings = 10f; // Extra time before failure trigger
    public string failureSceneName = "FailureScene"; // Name of the failure scene
    public string nextSceneName = "NextScene"; // Name of the next scene if escaped successfully

    private bool introductionPlayed = false;
    private int currentRecordingIndex = 0;
    private float recordingTimer = 0f;
    private bool allRecordingsPlayed = false;
    private bool lingering = false;
    private float lingerTimer = 0f;

    // Method to move the player to Position A with fade

    private void Update()
    {
        // Handle introduction narration
        if (!introductionPlayed && !introductionAudio.isPlaying)
        {
            introductionAudio.Play();
            introductionPlayed = true;
        }

        // Play hurry recordings sequentially with delay
        if (introductionPlayed && !allRecordingsPlayed)
        {
            if (currentRecordingIndex < hurryRecordings.Length)
            {
                recordingTimer += Time.deltaTime;
                if (recordingTimer >= delayBetweenHurryRecordings)
                {
                    hurryRecordings[currentRecordingIndex].Play();
                    currentRecordingIndex++;
                    recordingTimer = 0f;
                }
            }
            else
            {
                allRecordingsPlayed = true; // Mark all recordings as played
            }
        }

        // Handle lingering logic after all recordings are played
        if (allRecordingsPlayed && !lingering)
        {
            lingerTimer += Time.deltaTime;
            if (lingerTimer >= lingerTimeAfterRecordings)
            {
                lingering = true; // Player has lingered too long
            }
        }
    }

    // Call this function when the player presses the escape button
    public void TryEscape()
    {
        if (lingering)
        {
            // Player lingered too long: teleport to failure scene
            StartCoroutine(FadeOutAndLoadScene(failureSceneName));

        }
        else
        {
            // Player escapes successfully: proceed to the next scene
            StartCoroutine(FadeOutAndLoadScene(nextSceneName));
        }
    }




    public void MoveToA()
    {
        StartCoroutine(FadeAndMove(positionA));
    }

    // Method to move the player to Position B with fade
    public void MoveToB()
    {
        StartCoroutine(FadeAndMove(positionB));
    }

    private IEnumerator FadeAndMove(Transform targetPosition)
    {
        // Fade to black
        yield return StartCoroutine(Fade(1f));

        // Move the player to the target position
        if (player != null && targetPosition != null)
        {
            Debug.Log($"Moving to {targetPosition.position}");
            player.position = targetPosition.position;
        }


        // Fade back to visible
        yield return StartCoroutine(Fade(0f));
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        // Ensure the fadeImage starts visible and fades out
        yield return StartCoroutine(Fade(1f));

        // Load the next scene
        SceneManager.LoadScene(sceneName);

        // Ensure the fadeImage starts fully opaque in the new scene and fades in
        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        if (fadeImage == null)
        {
            Debug.LogError("Fade Image is not assigned!");
            yield break;
        }

        float startAlpha = fadeImage.color.a;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }

        // Ensure the final alpha is set
        Color finalColor = fadeImage.color;
        finalColor.a = targetAlpha;
        fadeImage.color = finalColor;
    }
}
