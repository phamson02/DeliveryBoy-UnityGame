using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseBtnHandler : MonoBehaviour
{
    public GameObject pauseMenu;

    public GameObject pauseBtn;

    public GameObject joystick;

    public GameObject taskList;

    public HealthBar healthBar;
    public GameObject itemsDelivered;
    public Button deliverButton, receiveButton;
    public CountdownTimer timer;
    public void pauseGame()
    {
        taskList.SetActive(false);
        pauseMenu.SetActive(true);
        pauseBtn.SetActive(false);
        joystick.SetActive(false);
        healthBar.gameObject.SetActive(false);
        itemsDelivered.SetActive(false);
        deliverButton.gameObject.SetActive(false);
        receiveButton.gameObject.SetActive(false);
        timer.counting = false;
    }
}
