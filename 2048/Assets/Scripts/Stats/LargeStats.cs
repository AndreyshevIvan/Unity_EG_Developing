using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeStats : StatsNode
{
    public override void SetScores()
    {
        string[] bestPlayers = m_data.GetBestPlayers(1);
        int[] bestScores = m_data.GetBestScores(1);

        PasteValues(bestPlayers, bestScores);
    }
}
