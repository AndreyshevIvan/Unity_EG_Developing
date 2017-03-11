using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InfoController : MonoBehaviour
{

    public string[] m_levels;

    int m_levelsCount;

    string m_openLevelsCountKey = "OpenLevelsCount";
    string m_spawnLevelKey = "SpawnLevel";

    const string m_levelsRoot = "Assets/Maps/";

    private void Awake()
    {
        m_levelsCount = m_levels.Length;
        Debug.Log(m_levelsCount);
    }

    public void ResetSaves()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SetOpenLevelsCount(int newCount)
    {
        PlayerPrefs.SetInt(m_openLevelsCountKey, newCount);
        SaveInfo();
    }
    public void SetSpawnLevel(int levelNumber)
    {
        PlayerPrefs.SetInt(m_spawnLevelKey, levelNumber);
        SaveInfo();
    }

    public int GetOpenLevelsCount()
    {
        return PlayerPrefs.GetInt(m_openLevelsCountKey, 1);
    }
    public int GetMaxLevelsCount()
    {
        return m_levelsCount;
    }
    public int GetSpawnLevel()
    {
        return PlayerPrefs.GetInt(m_spawnLevelKey, 1);
    }
    public StreamReader GetSpawnLevelReader()
    {
        int level = GetSpawnLevel();
        string levelName = m_levels[level - 1];

        return (new StreamReader(m_levelsRoot + levelName));

    }

    void SaveInfo()
    {
        PlayerPrefs.Save();
    }
}
