using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public Text m_login;

    void Awake()
    {
        if (DataManager.IsGameBeenStarted())
        {
            SceneManager.LoadScene("Scenes/MainScene");
        }
    }

    public void StartGame()
    {
        string playerName = m_login.text;

        if (playerName != "")
        {
            DataManager.StartGame(playerName);
            SceneManager.LoadScene("Scenes/MainScene");
        }
    }
}
