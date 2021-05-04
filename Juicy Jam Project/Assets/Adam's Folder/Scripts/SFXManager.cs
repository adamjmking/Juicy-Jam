using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    //Audiosource
    public AudioSource sfxSource;

    //List of audioclips
    [SerializeField] public AudioClip[] sfxList;

    public void PlaySound(int index)
    {
        sfxSource.PlayOneShot(sfxList[index]);
    }
}
