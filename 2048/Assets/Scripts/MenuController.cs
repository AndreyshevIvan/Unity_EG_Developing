using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    public GameObject m_sceneCurtain;

    ScenesController m_sceneController;

    private void Awake()
    {
        m_sceneController = new ScenesController();
        m_sceneCurtain.SetActive(false);
    }

    public void SetReadNameScene()
    {
        m_sceneCurtain.SetActive(true);
        m_sceneCurtain.GetComponent<Animation>().Play();
        StartCoroutine(m_sceneController.SetReadnameScene());
    }

    public void SetStatsScene()
    {
        m_sceneCurtain.SetActive(true);
        m_sceneCurtain.GetComponent<Animation>().Play();
        StartCoroutine(m_sceneController.SetScoresScene());
    }

}
