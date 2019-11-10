using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    public AudioClip[] audioClips;

    void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int Index)
    {
        if ((Index >= 0) && (Index < this.audioClips.Length))
        {
            this.audioSource.clip = this.audioClips[Index];
            this.audioSource.Play();
        }
    }

}
