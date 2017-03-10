using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{

    public AbstractUser m_player;

    ArrayList m_bonuses;

    public BottomWall m_bottomWallBonus;
    public Life m_lifeBonus;
    public MultyBall m_multyBallBonus;
    public TimeScale m_timeScaleBonus;
    public PointsMultiplitter m_multiplitter;

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

    public void DropBonus(Vector3 position)
    {
        int random = Random.Range(0, 100);
        Bonus newBonus = null;

        if (random < 15)
        {
            newBonus = m_bottomWallBonus;
        }
        else if (random >= 15 && random < 30)
        {
            newBonus = m_lifeBonus;
        }
        else if (random >= 30 && random < 45)
        {
            newBonus = m_multyBallBonus;
        }
        else if (random >= 45 && random < 60)
        {
            newBonus = m_timeScaleBonus;
        }
        else if (random >= 60 && random < 75)
        {
            newBonus = m_multiplitter;
        }

        if (newBonus != null)
        {
            CreateBonus(newBonus, position);
        }
    }
    void CreateBonus(Bonus m_newBonus, Vector3 position)
    {
        Bonus newBonus = Instantiate(m_newBonus, position, Quaternion.identity);
        newBonus.Init(m_player);
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
