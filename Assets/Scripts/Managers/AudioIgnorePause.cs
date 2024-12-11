using UnityEngine;

public class AudioIgnorePause : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null) audio.ignoreListenerPause = true;
    }
}