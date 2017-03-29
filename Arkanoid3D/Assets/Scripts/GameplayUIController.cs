using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIController : MonoBehaviour
{
    public InfoController m_info;

    public Text m_pointsField;
    public Text m_wallField;
    public Text m_ballsField;
    public Text m_multiplitterField;
    public Text m_levelName;

    public GameObject m_heathBar;
    public GameObject m_hearth;

    List<GameObject> m_playerHeaths;

    float m_addingTime = 0;

    const float ADDING_POINTS_SPEED = 0.01f;
    const int POINTS_PER_ONE_ADD = 10;
    const float HEARTH_POSITION_FACTOR = 1.1f;

    private void Awake()
    {
        m_playerHeaths = new List<GameObject>();
        m_pointsField.text = "0";
        m_wallField.text = "0";
        m_ballsField.text = "0";
        m_multiplitterField.text = "0";
        m_levelName.text = m_info.GetSpawnLevelName();
    }
    public void Init(int lifesCount)
    {
        InitLifes(lifesCount);
    } 
    void InitLifes(int lifesCount)
    {
        RectTransform barTransform = m_heathBar.GetComponent<RectTransform>();
        RectTransform hearthTransform = m_hearth.GetComponent<RectTransform>();
        float hearthWidth = barTransform.rect.size.x / (lifesCount + 1);
        hearthTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hearthWidth);
        float spawnPosX = -hearthWidth / 2;

        while (lifesCount > 0)
        {
            GameObject health = Instantiate(m_hearth);
            health.transform.SetParent(m_heathBar.transform, false);
            health.transform.localPosition = new Vector3(spawnPosX, 0, 0);
            spawnPosX -= hearthWidth * HEARTH_POSITION_FACTOR;
            lifesCount--;
            m_playerHeaths.Add(health);
        }
    }

    public void UpdateBalls(int ballsCount)
    {
        m_ballsField.text = "Balls: " + ballsCount.ToString();
    }
    public void UpdateWall(float duration, float maxDuration)
    {
        int seconds = (int)maxDuration - (int)duration;

        if (duration != 0)
        {
            m_wallField.text = "Wall time: " + seconds.ToString();
        }
        else
        {
            m_wallField.text = "Wall is off";
        }
    }
    public void UpdateLife(int lifeCount)
    {
        int hearthsCount = m_playerHeaths.Count;

        if (lifeCount < hearthsCount)
        {
            for (int i = lifeCount; i < hearthsCount; i++)
            {
                m_playerHeaths[i].GetComponent<PlayerHearth>().Kill();
                m_playerHeaths.Remove(m_playerHeaths[i]);
            }
        }
    }
    public void UpdatePoints(int points)
    {
        int currPoints = int.Parse(m_pointsField.text);
        int pointsToAdd = points - currPoints;

        m_addingTime += Time.deltaTime;

        if (m_addingTime >= ADDING_POINTS_SPEED && pointsToAdd != 0)
        {
            if (pointsToAdd >= POINTS_PER_ONE_ADD)
            {
                AddPointsToText(POINTS_PER_ONE_ADD);
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
        m_multiplitterField.text = "Multiplier: x" + multiplitter.ToString();
    }
    void AddPointsToText(int points)
    {
        int currPoints = int.Parse(m_pointsField.text);
        int newPoints = currPoints + points;

        m_pointsField.text = newPoints.ToString();
    }
}
