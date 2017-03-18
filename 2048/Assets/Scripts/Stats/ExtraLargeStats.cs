using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLargeStats : StatsNode
{
    public override void SetScores()
    {
        string[] bestPlayers = m_data.GetBestPlayers(2);
        int[] bestScores = m_data.GetBestScores(2);

        PasteValues(bestPlayers, bestScores);
    }
}