using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameoverUIController : MonoBehaviour
{
    public InfoController m_info;
    public Text m_levelName;
    public ButtonEffects m_restartButton;

    void Start()
    {
        m_levelName.text = m_info.GetSpawnLevelName();
        m_restartButton.SetArtificialActive(true);
    }

}
