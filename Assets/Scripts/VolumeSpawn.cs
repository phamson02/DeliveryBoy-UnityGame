using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class VolumeSpawn : MonoBehaviour
{
    public AudioMixer mixer;
    public GameObject musicOn;
    public GameObject musicOff;
    public GameObject slider;

    public GameData data;
    void Awake(){
        data = SaveSystem.Load();
        slider.GetComponent<UnityEngine.UI.Slider>().value = data.volume;
        mixer.SetFloat("musicvol", data.volume);
        AudioListener.volume = data.audios;
        if (data.audios == 0)
        {
            musicOn.SetActive(false);
            musicOff.SetActive(true);
        }
        else
        {
            musicOn.SetActive(true);
            musicOff.SetActive(false);
        }
    }
}
