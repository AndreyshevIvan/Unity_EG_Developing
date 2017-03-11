using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{

    public AbstractUser m_player;

    ArrayList m_bonuses;
    List<Bonus> m_newBonuses;

    public BottomWall m_bottomWallBonus;
    public Life m_lifeBonus;
    public MultyBall m_multyBallBonus;
    public TimeScale m_timeScaleBonus;
    public PointsMultiplitter m_multiplitter;
    public Fireball m_fireball;
    public AttackMode m_attackMode;

    int m_blocksPerDrop = 5;
    int m_currDropNumber = 0;
    float m_dropTimer = 0;
    float m_oneDropTime = 4;

    private void Awake()
    {
        m_newBonuses = new List<Bonus>();
        m_bonuses = new ArrayList();

        m_newBonuses.Add(m_bottomWallBonus);
        m_newBonuses.Add(m_lifeBonus);
        m_newBonuses.Add(m_multyBallBonus);
        m_newBonuses.Add(m_timeScaleBonus);
        m_newBonuses.Add(m_multiplitter);
        m_newBonuses.Add(m_fireball);
        m_newBonuses.Add(m_attackMode);
    }

    private void FixedUpdate()
    {
        CheckBonusesExist();
        UpdateDropTimer();
    }
    void UpdateDropTimer()
    {
        if (m_dropTimer <= m_oneDropTime)
        {
            m_dropTimer += Time.deltaTime;
        }
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
            m_bonuses.Remove(ball);
        }

        toDelete.Clear();
    }

    public void DropBonus(Vector3 position)
    {
        m_currDropNumber++;

        if (IsDropAllowed())
        {
            int random = Random.Range(0, m_newBonuses.Count);
            CreateBonus(m_newBonuses[random], position);

            m_dropTimer = 0;
            m_currDropNumber = 0;
        }
    }
    void CreateBonus(Bonus m_newBonus, Vector3 position)
    {
        Bonus newBonus = Instantiate(m_newBonus, position, Quaternion.identity);
        newBonus.Init(m_player);
        m_bonuses.Add(newBonus);
    }
    bool IsDropAllowed()
    {
        bool isTimeValid = (m_dropTimer > m_oneDropTime);
        bool isDropNumberValid = (m_currDropNumber > m_blocksPerDrop);

        return isTimeValid && isDropNumberValid;
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
