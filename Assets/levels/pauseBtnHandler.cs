using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseBtnHandler : MonoBehaviour
{
    public GameObject pauseMenu;

    public GameObject pauseBtn;

    public GameObject joystick;

    public GameObject taskList;
    public void pauseGame()
    {
        taskList.SetActive(false);
        pauseMenu.SetActive(true);
        pauseBtn.SetActive(false);
        joystick.SetActive(false);
    }
}
