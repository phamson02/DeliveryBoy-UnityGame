using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backMenuBtn : MonoBehaviour
{
    public GameObject currentPanel;

    public GameData data;
    public void backToMenu()
    {
        data = SaveSystem.Load();
        currentPanel.SetActive(false);
    }

    public void exit()
    {
        Application.Quit();
    }
}
