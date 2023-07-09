using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    // Start is called before the first frame update
    //public int gameStartScene;
    public GameObject levelMenu;
    
    public void StartGame()
    {
        //SceneManager.LoadScene(gameStartScene);
        levelMenu.SetActive(true);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
