using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadNameController : MonoBehaviour
{
    public DataController m_data;

    public InputField m_nameField;

    ScenesController m_sceneController;

    private void Awake()
    {
        m_sceneController = new ScenesController();
    }

    public void SetInfo(int mapIndex)
    {
        m_data.SetMapIndex(mapIndex);

        SetPlayerName();
    }
    void SetPlayerName()
    {
        string playerName = m_nameField.text;
        m_data.SetUsername(playerName);
    }
    public void SetGameplayScene()
    {
        StartCoroutine(m_sceneController.SetGameplayScene());
    }
}
