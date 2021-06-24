using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class buttonSFX : MonoBehaviour
{
    public AudioSource mySFX;
    public AudioClip hoverSFX;
    public AudioClip clickSFX;

    public void HoverSound()
    {
        mySFX.PlayOneShot(hoverSFX);
    }

    public void ClickSound()
    {
        mySFX.PlayOneShot(clickSFX);
    }
}
