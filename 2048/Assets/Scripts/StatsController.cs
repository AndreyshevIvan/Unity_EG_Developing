using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    public GameObject m_sceneCurtain;

    ScenesController m_sceneController;

    private void Awake()
    {
        m_sceneCurtain.SetActive(false);
        m_sceneController = new ScenesController();
    }

    public void SetMenuScene()
    {
        m_sceneCurtain.SetActive(true);
        m_sceneCurtain.GetComponent<Animation>().Play();
        StartCoroutine(m_sceneController.SetMenuScene());
    }
}
