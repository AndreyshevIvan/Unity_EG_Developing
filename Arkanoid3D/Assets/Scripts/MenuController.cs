using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    public InfoController m_info;

    private void Awake()
    {

    }

    public void ResetSaves()
    {
        m_info.ResetSaves();
    }
}