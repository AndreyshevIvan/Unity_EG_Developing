using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchScenesCommands : MonoBehaviour
{

    public void SetGameplayScene()
    {
        SceneManager.LoadScene("Scenes/Gameplay");
    }
    public void SetMenuScene()
    {
        SceneManager.LoadScene("Scenes/Menu");
    }
    public void SetGameoverScene()
    {
        SceneManager.LoadScene("Scenes/Gameover");
    }
    public void SetChangeLevelScene()
    {
        SceneManager.LoadScene("Scenes/ChangeLevel");
    }
    public void SetWinScene()
    {
        SceneManager.LoadScene("Scenes/Win");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
