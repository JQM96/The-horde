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

        GameObject.Destroy(soundGameObject, clip.length);
    }
    public static void PlaySound(AudioClip clip, bool variance)
    {
        GameObject soundGameObject = new GameObject("sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

        if (variance)
        {
            float amount = Random.Range(1f, 1.5f);

            audioSource.pitch = amount;
            audioSource.PlayOneShot(clip);
        }
        else
        {
            audioSource.PlayOneShot(clip);
        }
        

        GameObject.Destroy(soundGameObject, clip.length);
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
