using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIController : MonoBehaviour
{
    public InfoController m_info;
    public BonusesPlateController m_plateController;

    public Text m_pointsField;
    public Text m_wallField;
    public Text m_ballsField;
    public Text m_levelName;
    public Text m_timer;
    public Text m_multiplier;

    public GameObject m_heathBar;
    public GameObject m_hearth;

    List<PlayerHearth> m_playerHeaths;

    bool m_isGameStart = false;
    float m_addingTime = 0;
    int m_pointsPerAdd = 1;

    const float TIME_TO_ONE_ADD = 0.02f;
    const float POINTS_TO_ONE_ADD_IN_PERSENTS = 1;
    const float HEARTH_POSITION_FACTOR = 1.1f;
    const float MULTIBALL_PLATE_DURATION = 3;
    const float MULTIPLIER_PLATE_DURATION = 3;
    const float LIFE_PLATE_DUR = 3;

    private void Awake()
    {
        m_playerHeaths = new List<PlayerHearth>();
        m_pointsField.text = "0";
        m_wallField.text = "0";
        m_ballsField.text = "0";
        m_timer.text = "0 0 : 0 0";
        m_levelName.text = m_info.GetSpawnLevelName();
        m_isGameStart = false;
    }
    public void InitLifes(int lifesCount)
    {
        Vector2 barSize = m_heathBar.GetComponent<RectTransform>().rect.size;
        RectTransform hearthTransform = m_hearth.GetComponent<RectTransform>();
        float hearthWidth = barSize.x / (lifesCount + 1);
        hearthTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hearthWidth);
        float spawnPosX = -hearthWidth / 2;

        while (lifesCount > 0)
        {
            GameObject newHearth = Instantiate(m_hearth);
            newHearth.transform.SetParent(m_heathBar.transform, false);
            newHearth.transform.localPosition = new Vector3(spawnPosX, 0, 0);
            spawnPosX -= hearthWidth * HEARTH_POSITION_FACTOR;
            PlayerHearth hearth = newHearth.GetComponent<PlayerHearth>();
            hearth.Start();
            m_playerHeaths.Add(hearth);
            lifesCount--;
        }
    }
    public void StartPlaying(bool isGameStart)
    {
        m_isGameStart = isGameStart;
    }
    public void ResetToNextLife()
    {
        m_plateController.Reset();
    }

    public void UpdateTime(float timeInSeconds)
    {
        if (m_isGameStart)
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

            m_timer.text = SetSpacesBetweenCh(minutesStr + ":" + secondsStr);
        }
    }
    public void UpdateLife(int lifeCount)
    {
        int hearthsCount = 0;
        foreach(PlayerHearth hearth in m_playerHeaths)
        {
            if (!hearth.IsDead())
            {
                hearthsCount++;
            }
        }

        if (lifeCount < hearthsCount)
        {
            for (int i = lifeCount; i < hearthsCount; i++)
            {
                m_playerHeaths[i].Kill();
            }
        }
        else if (lifeCount > hearthsCount)
        {
            m_playerHeaths[hearthsCount].Rise();
            m_plateController.AddLife(LIFE_PLATE_DUR);
        }
    }
    public void UpdatePoints(int points)
    {
        SetPointsPerAddToValue(points);

        int currPoints = ConvertPointsToInt(m_pointsField.text);
        int pointsToAdd = points - currPoints;

        m_addingTime += Time.deltaTime;

        if (m_addingTime >= TIME_TO_ONE_ADD && pointsToAdd != 0)
        {
            if (pointsToAdd >= m_pointsPerAdd)
            {
                AddPointsToText(m_pointsPerAdd);
            }
            else
            {
                AddPointsToText(pointsToAdd);
            }

            m_addingTime = 0;
        }
    }
    public void UpdateMultiplier(int multiplitter)
    {
        m_multiplier.text = "x " + multiplitter.ToString();
    }

    public void CreateFireBallPlate(float duration)
    {
        m_plateController.AddFireBall(duration);
    }
    public void CreateWallPlate(float duration)
    {
        m_plateController.AddWall(duration);
    }
    public void CreateMultiplierPlate()
    {
        m_plateController.AddMultiplier(MULTIPLIER_PLATE_DURATION);
    }
    public void CreateMultiBallsPlate()
    {
        m_plateController.AddMultyball(MULTIBALL_PLATE_DURATION);
    }
    public void CreateAttackPlate(float duration)
    {
        m_plateController.AddAttack(duration);
    }

    void AddPointsToText(int points)
    {
        int currPoints = ConvertPointsToInt(m_pointsField.text);
        int newPoints = currPoints + points;

        m_pointsField.text = SetSpacesBetweenCh(newPoints.ToString());
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
}
