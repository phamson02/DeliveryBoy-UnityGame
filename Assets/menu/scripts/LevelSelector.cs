using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public GameObject levelButton;
    int level;
    // void Start()
    // {
    //     levelText.text = level.ToString();
    // }
    public void SelectLevel ()
    {
        level = int.Parse(levelButton.name);
        SceneManager.LoadScene((level-1)/4+1);
    }
}
