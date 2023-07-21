using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public GameObject levelButton;
    public int level;
    // void Start()
    // {
    //     levelText.text = level.ToString();
    // }
    public void SelectLevel ()
    {   
        level = int.Parse(levelButton.name);
        PlayerPrefs.SetInt("SelectedLevel", level);
        PlayerPrefs.Save();
        SceneManager.LoadScene((level-1)/4+1);
    }

    
}
