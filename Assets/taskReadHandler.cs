using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class taskReadHandler : MonoBehaviour
{
    public GameObject taskCanvas;
    public GameObject pauseBtn;
    public GameObject joystick;
    public HealthBar healthBar;
    // Start is called before the first frame update
    public void taskRead()
    {
        taskCanvas.SetActive(false);
        pauseBtn.SetActive(true);
        joystick.SetActive(true);

        healthBar.gameObject.SetActive(true);
    }
}
