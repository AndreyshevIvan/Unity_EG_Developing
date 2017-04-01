using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InfoController : MonoBehaviour
{
    public string[] m_levels;
    int m_levelsCount;

    const string OPEN_LEVELS_COUNT_KEY = "OpenLevelsCount";
    const string SPAWN_LEVEL_KEY = "SpawnLevel";
    const string MAX_POINTS_KEY = "MaxPointsPer";
    const string TOTAL_POINTS_KEY = "TotalPoints";
    const string LEVEL_POINTS_KEY = "LevelPoints";
    const string LEVEL_TIME_KEY = "LevelTime";

    const string LEVELS_PATH = "Assets/Info/";
    const string STOP_READ_KEY = "END.";

    public void ResetSaves()
    {
        PlayerPrefs.DeleteAll();
    }

    public void TryOpenNewLevel()
    {
        int currLevel = GetSpawnLevelNumber();

        if (currLevel < m_levelsCount)
        {
            int newLevel = currLevel + 1;

            PlayerPrefs.SetInt(OPEN_LEVELS_COUNT_KEY, newLevel);
            SaveInfo();
        }
    }
    public void SetSpawnLevel(int levelNumber)
    {
        PlayerPrefs.SetInt(SPAWN_LEVEL_KEY, levelNumber);
        SaveInfo();
    }
    public void TrySetMaxPoints(int newMaxPoints)
    {
        if (GetMaxPoints() < newMaxPoints)
        {
            PlayerPrefs.SetInt(MAX_POINTS_KEY, newMaxPoints);
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

        PlayerPrefs.SetInt(TOTAL_POINTS_KEY, total);
        SaveInfo();
    }
    public void SetLevelPoints(int points)
    {
        PlayerPrefs.SetInt(LEVEL_POINTS_KEY, points);
    }
    public void SetLevelTime(int seconds)
    {
        PlayerPrefs.SetInt(LEVEL_TIME_KEY, seconds);
    }

    public int GetOpenLevelsCount()
    {
        return PlayerPrefs.GetInt(OPEN_LEVELS_COUNT_KEY, 1);
    }
    public int GetMaxLevelsCount()
    {
        m_levelsCount = m_levels.Length;
        return m_levelsCount;
    }
    public int GetSpawnLevelNumber()
    {
        return PlayerPrefs.GetInt(SPAWN_LEVEL_KEY, 1);
    }
    public List<string> GetSpawnLevel()
    {
        int levelNumber = GetSpawnLevelNumber();
        string levelName = m_levels[levelNumber - 1];
        StreamReader reader = new StreamReader(LEVELS_PATH + levelName);
        List<string>level = new List<string>();

        reader.ReadLine(); // skip name line

        string line = reader.ReadLine();
        while (line != null && line != STOP_READ_KEY)
        {
            level.Add(line);
            line = reader.ReadLine();
        }
        reader.Close();

        return level;
    }
    public int GetMaxPoints()
    {
        return PlayerPrefs.GetInt(MAX_POINTS_KEY, 0);
    }
    public int GetTotal()
    {
        return PlayerPrefs.GetInt(TOTAL_POINTS_KEY, 0);
    }
    public int GetLastLevelPoints()
    {
        return PlayerPrefs.GetInt(LEVEL_POINTS_KEY, 0);
    }
    public string GetSpawnLevelName()
    {
        int levelNumber = GetSpawnLevelNumber();
        string levelName = m_levels[levelNumber - 1];
        StreamReader reader = new StreamReader(LEVELS_PATH + levelName);
        string name = reader.ReadLine();
        reader.Close();

        return name;
    }
    public List<string> GetAllLevelsNames()
    {
        List<string> names = new List<string>();

        foreach(string level in m_levels)
        {
            StreamReader reader = new StreamReader(LEVELS_PATH + level);
            string levelName = reader.ReadLine();
            names.Add(levelName);
            reader.Close();
        }

        return names;
    }
    public int GetLastLevelTime()
    {
        return PlayerPrefs.GetInt(LEVEL_TIME_KEY, 0);
    }

    void SaveInfo()
    {
        PlayerPrefs.Save();
    }
}
