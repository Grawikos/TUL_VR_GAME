using UnityEngine;

public class GazeAndDelayNarration : MonoBehaviour
{
    public AudioSource narrationAudio;
    public float gazeDuration = 2f;
    public float delayInSeconds = 1f;
    public bool singleUse = true;
    public float doNotPlayBefore = 0f;

    private float gazeTimer = 0f;
    private bool isPlaying = false;
    private bool done = false;
    private float elapsedTime = 0f;

    void Update()
    {
        if (done){
            return;
        }
        elapsedTime += Time.deltaTime;
        if (elapsedTime < doNotPlayBefore)
        {
            gazeTimer = 0f; // Reset gaze timer if narration cannot start
            return;
        }
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                gazeTimer += Time.deltaTime;

                if (gazeTimer >= gazeDuration && !isPlaying)
                {
                    isPlaying = true;
                    Invoke("PlayNarration", delayInSeconds);
                    if (singleUse){
                        done = true;
                    }
                }
            }
            else
            {
                ResetGaze();
            }
        }
        else
        {
            ResetGaze();
        }
    }

    void PlayNarration()
    {
        if (narrationAudio != null)
        {
            narrationAudio.Play();
        }
    }

    void ResetGaze()
    {
        gazeTimer = 0f;
        isPlaying = false;
    }
}
