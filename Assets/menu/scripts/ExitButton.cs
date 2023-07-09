using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{   
    public GameObject exitPanel;
    // Start is called before the first frame update
    public void QuitGameBtn()
    {
        exitPanel.SetActive(true);
    }
    // Update is called once per frame
}
