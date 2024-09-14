using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    public static void PlaySound(AudioClip clip)
    {
        GameObject soundGameObject = new GameObject("sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

        audioSource.PlayOneShot(clip);
    }

    public static void PlayMusic(AudioClip clip)
    {
        GameObject soundGameObject = new GameObject("sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

        audioSource.loop = true;
        audioSource.clip = clip;

        audioSource.Play();
    }
}
