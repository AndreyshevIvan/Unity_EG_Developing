using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{

    string m_mapIndexKey = "MapIndex";
    string m_usernameKey = "Username";
    string m_bestScore = "BestScore";

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }

    public int GetMapIndex()
    {
        int index = PlayerPrefs.GetInt(m_mapIndexKey, 0);

        return index;
    }
    public string GetUsername()
    {
        string username = PlayerPrefs.GetString(m_usernameKey, "@username");

        return username;
    }
    public uint GetBestScore(int mapIndex)
    {
        string mapKey = IndexToKey(mapIndex);
        int score = PlayerPrefs.GetInt(m_bestScore + mapKey, 0);

        return (uint)score;
    }

    public void SetMapIndex(int mapIndex)
    {
        PlayerPrefs.SetInt(m_mapIndexKey, mapIndex);
    }
    public void SetUsername(string username)
    {
        PlayerPrefs.SetString(m_usernameKey, username);
    }
    public void SetBestScore(int mapIndex, uint score, string playerName)
    {
        uint currBest = GetBestScore(mapIndex);

        if (currBest < score)
        {
            string mapKey = IndexToKey(mapIndex);
            PlayerPrefs.SetInt(m_bestScore + mapKey, (int)score);
        }
    }

    string IndexToKey(int mapIndex)
    {
        string mapKey = "_Small";

        switch(mapIndex)
        {
            case 0:
                mapKey = "_Small";
                break;
            case 1:
                mapKey = "_Normal";
                break;
            case 2:
                mapKey = "_Big";
                break;
        }

        return mapKey;
    }

    private void OnDestroy()
    {
        PlayerPrefs.Save();
    }
}
