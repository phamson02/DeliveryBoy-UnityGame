using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    public AudioMixer mixer;

    public GameObject musicOn;
    public GameObject musicOff;

    public GameObject slider;

    public GameData data;

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("musicvol", sliderValue);
        data.volume = sliderValue;
        SaveSystem.Save(data);
    }

    public void btnOn()
    {
        AudioListener.volume = 0;
        musicOn.SetActive(false);
        musicOff.SetActive(true);
        data.audios = 0;
        SaveSystem.Save(data);
    }

    public void btnOff()
    {
        AudioListener.volume = 1;
        musicOn.SetActive(true);
        musicOff.SetActive(false);
        data.audios = 1;
        SaveSystem.Save(data);
    }
}
