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
    string m_maxPointsKey = "MaxPointsPer";
    string m_totalPointsKey = "TotalPoints";
    string m_levelPointsKey = "LevelPoints";

    const string m_levelsRoot = "Assets/Maps/";

    public void ResetSaves()
    {
        PlayerPrefs.DeleteAll();
    }

    public void TryOpenNewLevel()
    {
        int currLevel = GetSpawnLevel();

        if (currLevel < m_levelsCount)
        {
            int newLevel = currLevel + 1;

            PlayerPrefs.SetInt(m_openLevelsCountKey, newLevel);
            SaveInfo();
        }
    }
    public void SetSpawnLevel(int levelNumber)
    {
        PlayerPrefs.SetInt(m_spawnLevelKey, levelNumber);
        SaveInfo();
    }
    public void TrySetMaxPoints(int newMaxPoints)
    {
        if (GetMaxPoints() < newMaxPoints)
        {
            PlayerPrefs.SetInt(m_maxPointsKey, newMaxPoints);
            SaveInfo();
        }
    }
    public void AddToTotal(int addPoints)
    {
        int total = GetTotal();
        if (int.MaxValue - addPoints >= total)
        {
            total += addPoints;
        }
        else
        {
            total = int.MaxValue;
        }

        PlayerPrefs.SetInt(m_totalPointsKey, total);
        SaveInfo();
    }
    public void SetLevelPoints(int points)
    {
        PlayerPrefs.SetInt(m_levelPointsKey, points);
    }

    public int GetOpenLevelsCount()
    {
        return PlayerPrefs.GetInt(m_openLevelsCountKey, 1);
    }
    public int GetMaxLevelsCount()
    {
        m_levelsCount = m_levels.Length;
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
    public int GetMaxPoints()
    {
        return PlayerPrefs.GetInt(m_maxPointsKey, 0);
    }
    public int GetTotal()
    {
        return PlayerPrefs.GetInt(m_totalPointsKey, 0);
    }
    public int GetLastLevelPoints()
    {
        return PlayerPrefs.GetInt(m_levelPointsKey, 0);
    }

    void SaveInfo()
    {
        PlayerPrefs.Save();
    }
}
