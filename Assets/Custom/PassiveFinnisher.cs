using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PassiveResetter : MonoBehaviour
{
    public Image fadeImage;          // Reference to the fade image
    public float fadeDuration = 1f;  // Duration of the fade effect
    public float delayBeforeReset;
    public AudioSource narrationAudio; 
    private bool done = false;

    public void Reset(string sceneName){
        if (done){
            return;
        }
        done = true;
        StartCoroutine(ResetToScene(sceneName));
    }

    public IEnumerator ResetToScene(string sceneName)
    {
        narrationAudio.Play();
        yield return new WaitForSeconds(delayBeforeReset);
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
