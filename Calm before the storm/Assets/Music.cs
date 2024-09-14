using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioClip music;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.PlayMusic(music);
    }
}
