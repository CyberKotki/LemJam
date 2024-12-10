using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    [SerializeField] private AudioSource sfxObject;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void PlaySFXClip(AudioClip sfxClip, Transform spawnTransform, Boolean ignorePause = false, float volume = 1.0f)
    {
        AudioSource audioSource = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = sfxClip;
        audioSource.volume = volume;
        audioSource.ignoreListenerPause = ignorePause;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSFXClip(AudioClip[] sfxClip, Transform spawnTransform, Boolean ignorePause = false,
        float volume = 1.0f)
    {
        int randIdx = Random.Range(0, sfxClip.Length);
        AudioSource audioSource = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = sfxClip[randIdx];
        audioSource.volume = volume;
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.ignoreListenerPause = ignorePause;
        audioSource.Play();
        float clipLength = audioSource.clip.length / Mathf.Abs(audioSource.pitch);
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayUIClick(AudioClip sfxClip)
    {
        PlaySFXClip(sfxClip, transform, ignorePause: true, volume: 1.0f);
    }
}


// to play sfx use
// [SerializeField] private AudioClip <soundClip name>
// SoundFXManager.instance.PlaySFXClip(<soundClip name>, transform, <volume float - default 1.0f>)

// for random sfx use
// [SerializeField] private AudioClip[] <soundClip name>
// SoundFXManager.instance.PlayRandomSFXClip(<soundClip name>, transform, <volume float - default 1.0f>)