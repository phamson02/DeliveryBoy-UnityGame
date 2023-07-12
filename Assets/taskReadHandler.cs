using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class taskReadHandler : MonoBehaviour
{
    public GameObject taskCanvas;
    public GameObject pauseBtn;
    public GameObject joystick;
    // Start is called before the first frame update
    public void taskRead()
    {
        taskCanvas.SetActive(false);
        pauseBtn.SetActive(true);
        joystick.SetActive(true);
    }
}
