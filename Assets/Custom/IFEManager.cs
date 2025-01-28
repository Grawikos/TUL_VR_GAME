using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class IFEPlayerMover : MonoBehaviour
{
    public Transform player;         // Reference to the player object (e.g., VR rig or camera)
    public Transform positionOff;      // Position inside SphereA
    public Transform positionB;      // Position inside SphereB

    public Image fadeImage;          // Reference to the fade image
    public float fadeDuration = 1f;  // Duration of the fade effect

    public AudioSource[] introductionAudios; // The introduction narration
    public AudioSource officeAudio; // Array of hurry-up recordings
    public float introDelay = 2;

    private int introductionPlayed = 0;
    private bool wentToOfiices = false;

    // Method to move the player to Position A with fade

    private void Update()
    {
        StartCoroutine(realUpdate());
    }

    private IEnumerator realUpdate(){
        if (introductionPlayed == 0)
        {
            introductionPlayed = 1;
            yield return new WaitForSeconds(introDelay);
            if (!wentToOfiices)
            {
                introductionAudios[0].Play();
                yield return new WaitForSeconds(9);
            }
            introductionPlayed = 2;
            if (!wentToOfiices)
            {
                introductionAudios[1].Play();
                yield return new WaitForSeconds(13);
            }
            introductionPlayed = 3;
            if (!wentToOfiices)
            {
                introductionAudios[2].Play();
            }
            introductionPlayed = 4;

        }
    }

    public void MoveToOffice()
    {
        StartCoroutine(FadeAndMove(positionOff));
    }

    // Method to move the player to Position B with fade
    public void MoveBack()
    {
        StartCoroutine(FadeAndMove(positionB));
    }

    private IEnumerator FadeAndMove(Transform targetPosition)
    {
        if (!wentToOfiices){
            wentToOfiices = true;
            switch (introductionPlayed){
                case 1:
                    yield return new WaitForSeconds(8);
                    break;
                case 2:
                    yield return new WaitForSeconds(13);
                    break;
                case 3:
                    yield return new WaitForSeconds(4);
                    break;
            }
            officeAudio.Play();
        }
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
