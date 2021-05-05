using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    //Audiosource
    public AudioSource musicSource;

    //List of audioclips
    [SerializeField] public AudioClip music;

    private void Start()
    {
        musicSource.PlayOneShot(music);
    }
}
