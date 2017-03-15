using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{

    public void SetMenuScene()
    {
        SceneManager.LoadScene("Scenes/Menu");
    }

    public void SetGameplayScene()
    {
        SceneManager.LoadScene("Scenes/Gameplay");
    }

    public void SetScoresScene()
    {
        SceneManager.LoadScene("Scenes/Scores");
    }

    public void SetReadnameScene()
    {
        SceneManager.LoadScene("Scenes/ReadName");
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}