using UnityEngine;

public class GazeNarrationController : MonoBehaviour
{
    public AudioSource narrationAudio;
    public float gazeDuration = 2f; // Time to trigger narration
    public bool singleUse = true;
    private float gazeTimer = 0f;
    private bool done = false;

    void Update()
    {
        if (done){
            return;
        }
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                gazeTimer += Time.deltaTime;

                if (gazeTimer >= gazeDuration && !narrationAudio.isPlaying)
                {
                    narrationAudio.Play();
                    done = true;
                }
            }
            else
            {
                gazeTimer = 0f; // Reset timer if not gazing
            }
        }
        else
        {
            gazeTimer = 0f; // Reset timer if no object is hit
        }
    }
}
