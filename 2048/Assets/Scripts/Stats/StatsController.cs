using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    public GameObject m_sceneCurtain;

    ScenesController m_sceneController;
    DataController m_data;

    private void Awake()
    {
        m_sceneCurtain.SetActive(false);
        m_sceneController = new ScenesController();
        m_data = new DataController();
    }

    public void SetMenuScene()
    {
        m_sceneCurtain.SetActive(true);
        m_sceneCurtain.GetComponent<Animation>().Play();
        StartCoroutine(m_sceneController.SetMenuScene());
    }

    public void ResetAll()
    {
        m_sceneCurtain.SetActive(true);
        m_sceneCurtain.GetComponent<Animation>().Play();
        StartCoroutine(m_sceneController.SetMenuScene());

        m_data.ResetData();
    }
}
