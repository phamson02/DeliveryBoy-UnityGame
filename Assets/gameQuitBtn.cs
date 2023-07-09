using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class gameQuitBtn : MonoBehaviour
{
    public void saveAndQuit()
    {
        SceneManager.LoadScene(0);
    }
}
