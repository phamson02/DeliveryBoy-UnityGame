using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class gameQuitBtn : MonoBehaviour
{
    public GameData data;
    public void saveAndQuit()
    {
        //SaveSystem.Save(data);
        SceneManager.LoadScene(0);
    }
}
