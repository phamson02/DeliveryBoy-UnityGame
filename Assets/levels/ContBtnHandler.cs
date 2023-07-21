using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContBtnHandler : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseBtn;

    public GameObject joystick;
    // public GameObject taskList;
    public HealthBar healthBar;
    public GameObject itemDelivered;
    public CountdownTimer timer;
    public void Continue()
    {
        pauseMenu.SetActive(false);
        // taskList.SetActive(true);
        pauseBtn.SetActive(true);
        joystick.SetActive(true);
        healthBar.gameObject.SetActive(true);
        itemDelivered.SetActive(true);
        timer.counting = true;
    }
}
