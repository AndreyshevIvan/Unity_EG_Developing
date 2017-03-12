using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLevelController : MonoBehaviour
{

    public Button[] m_levelButtons;

    public InfoController m_info;

    int m_openLevelsCount;

    bool m_isLevelHackActive = false;

    void Awake()
    {
        m_openLevelsCount = m_info.GetOpenLevelsCount();

        UnlockLevelButtons(m_openLevelsCount);
    }
    void Reset()
    {
        foreach(Button button in m_levelButtons)
        {
            button.interactable = false;
        }
    }
    void UnlockLevelButtons(int levelsCount)
    {
        Reset();

        for (int i = 0; i < levelsCount; i++)
        {
            m_levelButtons[i].interactable = true;
        }
    }

    void FixedUpdate()
    {
        HandleCheats();
    }

    void HandleCheats()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (m_isLevelHackActive)
            {
                UnlockLevelButtons(m_openLevelsCount);
            }
            else
            {
                UnlockLevelButtons(m_info.GetMaxLevelsCount());
            }

            m_isLevelHackActive = !m_isLevelHackActive;
        }
    }

    public void SetSpawnLevel(int levelNumber)
    {
        m_info.SetSpawnLevel(levelNumber);
    }
}
