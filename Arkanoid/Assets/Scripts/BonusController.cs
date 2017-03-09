using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{

    public PlayerController m_player;

    ArrayList m_bonuses;

    public BottomWall m_bottomWallBonus;
    public Life m_lifeBonus;
    public MultyBall m_multyBallBonus;

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

        if (random < 15)
        {
            CreateBottomWallBonus(position);
        }
        else if (random >= 15 && random < 30)
        {
            CreateLifeBonus(position);
        }
        else if (random >= 30 && random < 45)
        {
            CreateMultyBall(position);
        }
    }
    void CreateBottomWallBonus(Vector3 position)
    {
        Bonus newBonus = Instantiate(m_bottomWallBonus, position, Quaternion.identity);
        newBonus.Init(m_player);
        m_bonuses.Add(newBonus);
    }
    void CreateLifeBonus(Vector3 position)
    {
        Bonus newBonus = Instantiate(m_lifeBonus, position, Quaternion.identity);
        newBonus.Init(m_player);
        m_bonuses.Add(newBonus);
    }
    void CreateMultyBall(Vector3 position)
    {
        Bonus newBonus = Instantiate(m_multyBallBonus, position, Quaternion.identity);
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
