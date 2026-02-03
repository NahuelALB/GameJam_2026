using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ConfigIntroVideo : MonoBehaviour
{
    public VideoPlayer intro;

    void Start()
    {
        intro.loopPointReached += OnVideoFinished;
    }
    
    void OnVideoFinished(VideoPlayer intro)
    {
        SceneManager.LoadScene(1);
    }
}
