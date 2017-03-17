using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadNameController : MonoBehaviour
{
    public DataController m_data;

    public InputField m_nameField;

    void SetInfo(byte mapIndex)
    {
        m_data.SetMapIndex(mapIndex);

        SetPlayerName();
    }
    void SetPlayerName()
    {
        string playerName = m_nameField.text;
        m_data.SetUsername(playerName);
    }
}
