using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{

    public BottomWall m_bottomWall;

    ArrayList m_bonuses;

    private void Awake()
    {
        m_bonuses = new ArrayList();
    }

    private void FixedUpdate()
    {
        CheckBonusesExist();
    }
    void CheckBonusesExist()
    {
        RemoveNulls();

        ArrayList toDelete = new ArrayList();

        foreach (Bonus ball in m_bonuses)
        {
            if (ball != null && !ball.IsLive())
            {
                toDelete.Add(ball);
            }
        }

        foreach (Bonus ball in toDelete)
        {
            ball.DestroyBonus();
            m_bonuses.Remove(ball);
        }

        toDelete.Clear();
    }
    void RemoveNulls()
    {
        ArrayList toDelete = new ArrayList();

        foreach (Bonus ball in m_bonuses)
        {
            if (ball == null)
            {
                toDelete.Add(ball);
            }
        }

        foreach (Bonus ball in toDelete)
        {
            if (ball == null)
            {
                m_bonuses.Remove(ball);
            }
        }

        toDelete.Clear();
    }

    public void CreateBottomWallBonus(Vector3 position)
    {
        Bonus newBonus = Instantiate(m_bottomWall, position, Quaternion.identity);
        m_bonuses.Add(newBonus);
    }

    public void SetFreeze(bool isFreeze)
    {
        foreach(Bonus bonus in m_bonuses)
        {
            bonus.SetFreeze(isFreeze);
        }
    }

    public void ClearBonuses()
    {
        foreach(Bonus bonus in m_bonuses)
        {
            bonus.DestroyBonus();
        }

        m_bonuses.Clear();
    }
}
