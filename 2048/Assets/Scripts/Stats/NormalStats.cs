using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalStats : StatsNode
{
    public override void SetScores()
    {
        string[] bestPlayers = m_data.GetBestPlayers(0);
        int[] bestScores = m_data.GetBestScores(0);

        PasteValues(bestPlayers, bestScores);
    }
}
