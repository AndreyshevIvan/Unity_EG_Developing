using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameBehaviour
{
    void Init(GameController controller);
    void UpdateBehavior();
}

public class GameBehaviour : MonoBehaviour, IGameBehaviour
{
    protected GameController m_controller;
    protected GameItems m_items;

    public void Init(GameController controller)
    {
        m_controller = controller;
        m_items = controller.GetItems();
    }

    public virtual void UpdateBehavior() { }
}
