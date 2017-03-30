using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIBehavior
{
    NOT_STARTED,
    LEVEL_POINTS,
    TIME,
    STARS,
    TOTAL,
    COMPLETE,
}

public class WinSceneUIController : MonoBehaviour
{
    public InfoController m_info;
    public Animator m_animator;

    public Text m_scoreField;
    public Text m_totalField;
    public Text m_levelnameField;
    public Text m_timeField;

    public ButtonEffects m_continueButton;

    public Color m_starLocked;
    public Color m_starUnclocked;

    public Star[] m_stars;

    int m_points = 0;
    int m_total = 0;
    int m_starsToUnlockCount = 6;
    int m_levelTime = 0;
    float m_addingTime = 0;
    float m_colodown = 0;
    int m_pointsPerAdd = 1;
    UIBehavior m_behavior = UIBehavior.NOT_STARTED;

    const float TIME_TO_ONE_ADD = 0.025f;
    const float POINTS_TO_ONE_ADD_IN_PERSENTS = 1;
    const float TIME_TO_UNLOCK_STAR = 0.3f;
    const int POINTS_TO_ONE_STAR = 5000;
    const float START_COLDOWN = 1;

    void Awake()
    {
        m_scoreField.text = "0";
        m_totalField.text = "0";
        m_timeField.text = "0 0 : 0 0";
        m_points = m_info.GetLastLevelPoints();
        m_total = m_info.GetTotal();
        m_levelnameField.text = m_info.GetSpawnLevelName();
        m_levelTime = m_info.GetLastLevelTime();

        m_starsToUnlockCount = m_points / POINTS_TO_ONE_STAR;
        if (m_starsToUnlockCount > m_stars.Length)
        {
            m_starsToUnlockCount = m_stars.Length;
        }
    }
    void Start()
    {
        m_continueButton.SetInteractable(false);
        m_continueButton.SetArtificialActive(false);

        foreach (Star star in m_stars)
        {
            star.Lock(m_starLocked);
        }
    }

    void FixedUpdate()
    {
        if (m_animator.GetTime() > START_COLDOWN)
        {
            CheckBehavior();

            switch (m_behavior)
            {
                case UIBehavior.LEVEL_POINTS:
                    AddPoints(ref m_scoreField, m_points);
                    break;
                case UIBehavior.TIME:
                    AddTime();
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
        else
        {
            m_colodown += Time.deltaTime;
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
                    SetPointsPerAddToValue(m_levelTime);
                    m_behavior = UIBehavior.TIME;
                }
                break;

            case UIBehavior.TIME:
                if (IsAddTimeComplete(ref m_timeField, m_levelTime))
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
                    m_continueButton.SetInteractable(true);
                    m_continueButton.SetArtificialActive(true);
                }
                break;
        }

        if (oldBehavior != m_behavior)
        {
            m_animator.SetInteger("CurrentAnimation", (int)m_behavior);
            m_addingTime = 0;
        }
    }

    void AddPoints(ref Text field, int points)
    {
        int currPoints = ConvertPointsToInt(field.text);
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
        int currValue = ConvertPointsToInt(field.text);
        int newValue = currValue + points;

        string valueStr = newValue.ToString();
        field.text = SetSpacesBetweenCh(valueStr);
    }
    void AddTime()
    {
        int currTime = ConvertTimeToInt(m_timeField.text);
        int timeToCurrAdd = m_levelTime - currTime;

        if (m_addingTime >= TIME_TO_ONE_ADD && timeToCurrAdd != 0)
        {
            string newTime = "";

            if (timeToCurrAdd >= m_pointsPerAdd)
            {
                newTime = ConvertTimeToStr(currTime + m_pointsPerAdd);
            }
            else
            {
                newTime = ConvertTimeToStr(currTime + timeToCurrAdd);
            }

            m_timeField.text = SetSpacesBetweenCh(newTime);
            m_addingTime = 0;
        }
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
    string SetSpacesBetweenCh(string str, int spacesCount = 1)
    {
        string spaces = "";

        while (spacesCount > 0)
        {
            spaces += " ";
            spacesCount--;
        }

        string result = "";

        for (int i = 0; i < str.Length; i++)
        {
            result += str[i];

            if (i != str.Length - 1)
            {
                result += spaces;
            }
        }

        return result;
    }

    string ConvertTimeToStr(float timeInSeconds)
    {
        int seconds = (int)timeInSeconds;
        int minutes = seconds / 60;
        seconds = seconds - (minutes * 60);

        string minutesStr = minutes.ToString();
        string secondsStr = seconds.ToString();

        if (minutes < 10)
        {
            minutesStr = "0" + minutesStr;
        }
        if (seconds < 10)
        {
            secondsStr = "0" + secondsStr;
        }

        return minutesStr + ":" + secondsStr;
    }
    int ConvertTimeToInt(string timeStr)
    {
        string minutes = "";
        string seconds = "";
        bool isSeconds = false;

        for (int i = 0; i < timeStr.Length; i++)
        {
            char ch = timeStr[i];

            if (ch == ':')
            {
                isSeconds = true;
            }

            if (ch >= '0' && ch <= '9')
            {
                if (isSeconds)
                {
                    seconds += ch;
                }
                else
                {
                    minutes += ch;
                }
            }
        }

        int intSec = int.Parse(seconds);
        int intMinutes = int.Parse(minutes);

        return (intSec + intMinutes * 60);
    }
    int ConvertPointsToInt(string str)
    {
        string result = "";
        char ch;

        for (int i = 0; i < str.Length; i++)
        {
            ch = str[i];
            if (ch >= '0' && ch <= '9')
            {
                result += ch;
            }
        }

        return int.Parse(result);
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
        if (ConvertPointsToInt(field.text) == value)
        {
            m_addingTime = 0;
            return true;
        }

        return false;
    }
    bool IsAddTimeComplete(ref Text field, int value)
    {
        string time = field.text;

        int intTime = ConvertTimeToInt(time);

        return intTime >= value;
    }
    bool IsStarsUnlocked()
    {
        return (GetUnlockedStarsCount(true) == m_starsToUnlockCount);
    }
}
