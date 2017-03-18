using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsNode : MonoBehaviour
{
    public Text[] m_players;
    public Text[] m_scores;

    protected DataController m_data;
    protected int m_nodesCount = 0;

    private void Awake()
    {
        m_data = new DataController();
        m_nodesCount = m_players.Length;

        for (int i = 0; i < m_nodesCount; i++)
        {
            m_players[i].text = "";
            m_scores[i].text = "";
        }

        SetScores();
    }

    public virtual void SetScores() { }

    protected void PasteValues(string[] players, int[] scores)
    {
        for (int i = 0; i < m_nodesCount; i++)
        {
            string player = players[i];
            string score = scores[i].ToString();

            if (player != "" && score != "")
            {
                m_players[i].text = player;
                m_scores[i].text = score;
            }
        }
    }
}
