using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene((int)SceneEnums.Game);
    }
    public void GoToConfig()
    {

    }
    public void QuitGame()
    {
        Debug.Log("game closed...");
        Application.Quit();
    }
}
