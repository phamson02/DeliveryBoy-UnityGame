using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class taskExit : MonoBehaviour
{
    public void exitTask()
    {
        SceneManager.LoadScene(0);
    }
}
