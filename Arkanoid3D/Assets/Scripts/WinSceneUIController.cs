using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIBehavior
{
    NOT_STARTED,
    LEVEL_POINTS,
    STARS,
    TOTAL,
    COMPLETE,
}

public class WinSceneUIController : MonoBehaviour
{
    public InfoController m_info;

    public Text m_scoreField;
    public Text m_totalField;

    public Color m_starLocked;
    public Color m_starUnclocked;

    public Star[] m_stars;

    int m_points = 0;
    int m_total = 0;
    int m_starsToUnlockCount = 6;
    float m_addingTime = 0;
    int m_pointsPerAdd = 1;
    UIBehavior m_behavior = UIBehavior.NOT_STARTED;

    const float TIME_TO_ONE_ADD = 0.02f;
    const float POINTS_TO_ONE_ADD_IN_PERSENTS = 1;
    const float TIME_TO_UNLOCK_STAR = 0.3f;
    const int POINTS_TO_ONE_STAR = 5000;

    private void Awake()
    {
        m_scoreField.text = "0";
        m_totalField.text = "0";
        m_points = m_info.GetLastLevelPoints();
        m_total = m_info.GetTotal();

        m_starsToUnlockCount = m_points / POINTS_TO_ONE_STAR;
        if (m_starsToUnlockCount > m_stars.Length)
        {
            m_starsToUnlockCount = m_stars.Length;
        }
    }
    private void Start()
    {
        foreach (Star star in m_stars)
        {
            star.Lock(m_starLocked);
        }
    }

    private void FixedUpdate()
    {
        CheckBehavior();

        switch (m_behavior)
        {
            case UIBehavior.LEVEL_POINTS:
                AddPoints(ref m_scoreField, m_points);
                break;
            case UIBehavior.STARS:
                AddStars(m_starsToUnlockCount);
                break;
            case UIBehavior.TOTAL:
                AddPoints(ref m_totalField, m_total);
                break;
        }

        if (m_behavior != UIBehavior.COMPLETE)
        {
            m_addingTime += Time.deltaTime;
        }
    }
    void CheckBehavior()
    {
        UIBehavior oldBehavior = m_behavior;

        switch (m_behavior)
        {
            case UIBehavior.NOT_STARTED:
                {
                    SetPointsPerAddToValue(m_points);
                    m_behavior = UIBehavior.LEVEL_POINTS;
                }
                break;

            case UIBehavior.LEVEL_POINTS:
                if (IsAddPointsComplete(ref m_scoreField, m_points))
                {
                    m_behavior = UIBehavior.STARS;
                }
                break;

            case UIBehavior.STARS:
                if (IsStarsUnlocked())
                {
                    m_behavior = UIBehavior.TOTAL;
                    SetPointsPerAddToValue(m_total);
                }
                break;

            case UIBehavior.TOTAL:
                if (IsAddPointsComplete(ref m_totalField, m_total))
                {
                    m_behavior = UIBehavior.COMPLETE;
                }
                break;
        }

        if (oldBehavior != m_behavior)
        {
            m_addingTime = 0;
        }
    }

    public void AddPoints(ref Text field, int points)
    {
        int currPoints = int.Parse(field.text);
        int pointsToAdd = points - currPoints;

        if (m_addingTime >= TIME_TO_ONE_ADD && pointsToAdd != 0)
        {
            if (pointsToAdd >= m_pointsPerAdd)
            {
                AddPointsToText(ref field, m_pointsPerAdd);
            }
            else
            {
                AddPointsToText(ref field, pointsToAdd);
            }

            m_addingTime = 0;
        }
    }
    void AddPointsToText(ref Text field, int points)
    {
        int currValue = int.Parse(field.text);
        int newValue = currValue + points;

        string valueStr = newValue.ToString();
        field.text = valueStr;
    }
    void AddStars(int starsCount)
    {
        int unlockedStars = GetUnlockedStarsCount();

        if (unlockedStars < m_starsToUnlockCount &&
            m_addingTime > TIME_TO_UNLOCK_STAR)
        {
            m_stars[unlockedStars].Unlock(m_starUnclocked);
            m_addingTime = 0;
        }
    }

    void SetPointsPerAddToValue(int points)
    {
        m_pointsPerAdd = (int)(points * (POINTS_TO_ONE_ADD_IN_PERSENTS / 100));

        if (m_pointsPerAdd < 1)
        {
            m_pointsPerAdd = 1;
        }
    }

    int GetUnlockedStarsCount(bool isCheckAnim = false)
    {
        int unlockedCount = 0;

        foreach (Star star in m_stars)
        {
            if (star.IsUnlock(isCheckAnim))
            {
                unlockedCount++;
            }
        }

        return unlockedCount;
    }

    bool IsAddPointsComplete(ref Text field, int value)
    {
        if (int.Parse(field.text) == value)
        {
            m_addingTime = 0;
            return true;
        }

        return false;
    }
    bool IsStarsUnlocked()
    {
        return (GetUnlockedStarsCount(true) == m_starsToUnlockCount);
    }
}
