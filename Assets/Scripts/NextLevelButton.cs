using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    public void nextLevelButtonClick(){
        int currentLevel = PlayerPrefs.GetInt("SelectedLevel");
        PlayerPrefs.SetInt("SelectedLevel", currentLevel+1);
        SceneManager.LoadScene((currentLevel+1-1)/4+1);
    }
    
}
