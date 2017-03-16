using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{

    float m_coldown = 0.25f;

    public void SetMenuScene()
    {
        SceneManager.LoadScene("Scenes/Menu");
    }

    public IEnumerator SetGameplayScene()
    {
        yield return new WaitForSeconds(m_coldown);
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