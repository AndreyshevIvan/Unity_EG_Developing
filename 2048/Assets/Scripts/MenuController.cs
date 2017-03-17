using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    public GameObject m_menuButtons;

    ScenesController m_sceneController;

    private void Awake()
    {
        m_sceneController = new ScenesController();
    }

    public void SetReadNameScene()
    {
        m_menuButtons.GetComponent<Animation>().Play();
        StartCoroutine(m_sceneController.SetReadnameScene());
    }

}
