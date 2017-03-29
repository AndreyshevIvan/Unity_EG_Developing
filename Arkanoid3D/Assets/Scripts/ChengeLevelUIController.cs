using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChengeLevelUIController : MonoBehaviour
{
    public InfoController m_info;
    public GameObject[] m_levelButtons;

    List<Text> m_levelNames;

    private void Awake()
    {
        m_levelNames = new List<Text>();

        for (int i = 0; i < m_levelButtons.Length; i++)
        {
            Text textField = m_levelButtons[i].GetComponentInChildren<Text>();
            m_levelNames.Add(textField);
        }
    }

    void Start()
    {
        SetLevelsNamesToButtons();
    }

    void SetLevelsNamesToButtons()
    {
        List<string> names = m_info.GetAllLevelsNames();

        for (int i = 0; i < m_levelNames.Count; i++)
        {
            if (names.Count > i)
            {
                m_levelNames[i].text = names[i];
            }
            else
            {
                m_levelNames[i].text = "Level " + i.ToString();
            }
        }
    }
}
