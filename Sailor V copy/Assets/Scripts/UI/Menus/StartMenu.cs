using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] GameObject startSelected;

    void OnEnable()
    {
        StartCoroutine(HandleLayoutInitialization());
    }
    void ExecuteOnEnable()
    {
        EventSystem.current.SetSelectedGameObject(startSelected);
    }

    // Menu options
    public void MenuSelect_StartGame()
    {
        SceneManager.LoadScene((int)SceneEnums.Game);
    }
    public void MenuSelect_Settings()
    {

    }
    public void MenuSelect_QuitGame()
    {
        Debug.Log("game closed...");
        Application.Quit();
    }
    IEnumerator HandleLayoutInitialization()
    {
        yield return new WaitForEndOfFrame();
        ExecuteOnEnable();
    }
}



