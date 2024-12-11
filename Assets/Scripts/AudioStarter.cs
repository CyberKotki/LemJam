using UnityEngine;

public class AudioStarter : MonoBehaviour
{
    [SerializeField] AudioClip music;
    [SerializeField] AudioClip ambient;
    void Start()
    {
        // MusicManager.instance.PlayBGMusic(music);
        // AmbientManager.instance.PlayAmbient(ambient);
    }

}
