using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    GameController m_controller;

    protected ItemsController m_items;
    protected CameraController m_cameraController;
    protected PlatformController m_platformController;
    protected BallsController m_ballsController;
    protected Spawner m_spawner;
    protected BlocksController m_blocksController;

    public void Init(GameController controller)
    {
        m_controller = controller;
        m_items = controller.GetItems();
        m_cameraController = controller.GetCameraController();
        m_platformController = controller.GetPlatformController();
        m_ballsController = controller.GetBallsController();
        m_spawner = controller.GetSpawner();
        m_blocksController = controller.GetBlocksController();

        PersonalInit();
    }

    public virtual void StartOptions() { }
    protected virtual void PersonalInit() { }
    public virtual void UpdateBehavior() { }

    protected void SetGameplayBehaviour()
    {
        m_controller.SetGameplayBehaviour();
    }
    protected void SetMenuBehaviour()
    {
        m_controller.SetMenuBehaviour();
    }
    protected void SetGameoverBehaviour()
    {
        m_controller.SetGameoverBehaviour();
    }
    protected void SetChangeLevelBehaviour()
    {
        m_controller.SetChangeLevelBehaviour();
    }
    protected void SetExitBehaviour()
    {
        m_controller.SetExitBehaviour();
    }
}
