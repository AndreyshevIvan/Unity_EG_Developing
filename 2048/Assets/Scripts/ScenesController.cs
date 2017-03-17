using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{

    float m_coldown = 0.25f;

    public IEnumerator SetMenuScene()
    {
        yield return new WaitForSeconds(m_coldown);
        SceneManager.LoadScene("Scenes/Menu");
    }

    public IEnumerator SetGameplayScene()
    {
        yield return new WaitForSeconds(m_coldown);
        SceneManager.LoadScene("Scenes/Gameplay");
    }

    public IEnumerator SetScoresScene()
    {
        yield return new WaitForSeconds(m_coldown);
        SceneManager.LoadScene("Scenes/Scores");
    }

    public IEnumerator SetReadnameScene()
    {
        yield return new WaitForSeconds(m_coldown);
        SceneManager.LoadScene("Scenes/ReadName");
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}