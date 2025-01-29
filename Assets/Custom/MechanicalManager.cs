using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayLoad : MonoBehaviour
{
    public string nextScene = "4Mechanical_end";
    public float delayInSeconds = 23f;

    void Start()
    {
        Invoke("Load", delayInSeconds);
    }

    void Load(){
        SceneManager.LoadScene(nextScene);
    }
}