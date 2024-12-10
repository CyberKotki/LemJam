using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float volumeLevel)
    {
        audioMixer.SetFloat("MasterVol", Mathf.Log10(volumeLevel) * 20f);
    }
    
    public void SetSFXVolume(float volumeLevel)
    {
        audioMixer.SetFloat("SFXVol", Mathf.Log10(volumeLevel) * 20f);
    }
    
    public void SetMusicVolume(float volumeLevel)
    {
        audioMixer.SetFloat("MusicVol", Mathf.Log10(volumeLevel) * 20f);
    }
}