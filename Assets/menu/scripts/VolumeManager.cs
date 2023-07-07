using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    public AudioMixer mixer;

    public GameObject musicOn;
    public GameObject musicOff;

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("musicvol", sliderValue);
    }

    public void btnOn()
    {
        AudioListener.volume = 0;
        musicOn.SetActive(false);
        musicOff.SetActive(true);
    }

    public void btnOff()
    {
        AudioListener.volume = 1;
        musicOn.SetActive(true);
        musicOff.SetActive(false);
    }
}
