using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBehavior : MonoBehaviour, GameBehavior
{

    private GameController m_controller;
    public GameItems m_items;

    private void Awake()
    {

    }

    public void Init(GameController controller)
    {
        m_controller = controller;
    }

    public void UpdateBehavior()
    {

    }
}
