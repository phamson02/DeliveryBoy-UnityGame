using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backMenuBtn : MonoBehaviour
{
    public GameObject currentPanel;
    public void backToMenu()
    {
        currentPanel.SetActive(false);
    }
}
