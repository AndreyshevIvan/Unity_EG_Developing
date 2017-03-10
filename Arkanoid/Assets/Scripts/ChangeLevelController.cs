using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLevelController : MonoBehaviour
{

    public Button[] m_levels;

    int m_maxLevels;
    int m_openLevelsCount;

    bool m_isLevelHackActive = false;

    void Awake()
    {
        m_maxLevels = m_levels.Length;
        
        PlayerPrefs.SetInt("LevelsCount", m_maxLevels);
        m_openLevelsCount = PlayerPrefs.GetInt("OpenLevelsCount", 1);
        PlayerPrefs.Save();

        UnlockLevels(m_openLevelsCount);
    }
    void Reset()
    {
        foreach(Button button in m_levels)
        {
            button.interactable = false;
        }
    }
    void UnlockLevels(int levelsCount)
    {
        Reset();

        for (int i = 0; i < levelsCount; i++)
        {
            m_levels[i].interactable = true;
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
                UnlockLevels(m_openLevelsCount);
            }
            else
            {
                UnlockLevels(m_maxLevels);
            }

            m_isLevelHackActive = !m_isLevelHackActive;
        }
    }

    public void SetSpawnLevel(int level)
    {
        PlayerPrefs.SetInt("SpawnLevel", level);
        PlayerPrefs.Save();
    }
}
