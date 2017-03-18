using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadNameController : MonoBehaviour
{
    public GameObject m_sceneCurtain;
    public InputField m_nameField;
    DataController m_data;

    ScenesController m_sceneController;

    private void Awake()
    {
        m_data = new DataController();
        m_sceneController = new ScenesController();
        m_sceneCurtain.SetActive(false);
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
        m_sceneCurtain.SetActive(true);
        m_sceneCurtain.GetComponent<Animation>().Play();
        StartCoroutine(m_sceneController.SetGameplayScene());
    }
    public void SetMenuScene()
    {
        m_sceneCurtain.SetActive(true);
        m_sceneCurtain.GetComponent<Animation>().Play();
        StartCoroutine(m_sceneController.SetMenuScene());
    }
}
