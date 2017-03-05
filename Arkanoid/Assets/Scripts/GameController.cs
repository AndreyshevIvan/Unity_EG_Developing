using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    GameBehaviour m_currentBehaviour;

    GameBehaviour m_gameplay;
    GameBehaviour m_menu;
    GameBehaviour m_changeLevel;
    GameBehaviour m_gameover;
    GameBehaviour m_exit;

    public GameplayBehaviour m_gameplayBehaviour;
    public MenuBehaviour m_menuBehaviour;
    public ChangeLevelBehaviour m_changeLevelBehavior;
    public GameoverBehaviour m_gameoverBehavior;
    public ExitBehaviour m_exitBehavior;

    public ItemsController m_items;
    public CameraController m_cameraController;
    public PlatformController m_platformController;
    public BallController m_ballController;
    public Spawner m_spawner;

    void Awake()
    {
        InitBehaviours();
        InitCameraController();
        InitPlatformController();
        InitBallController();
        InitSpawner();

        SetMenuBehaviour();
    }

    void InitBehaviours()
    {
        m_gameplay = m_gameplayBehaviour;
        m_menu = m_menuBehaviour;
        m_changeLevel = m_changeLevelBehavior;
        m_gameover = m_gameoverBehavior;
        m_exit = m_exitBehavior;

        m_gameplayBehaviour.Init(this);
        m_menuBehaviour.Init(this);
        m_changeLevelBehavior.Init(this);
        m_gameoverBehavior.Init(this);
        m_exitBehavior.Init(this);
    }
    void InitCameraController()
    {
        GameObject floorBlock = m_items.m_gameplayItems.m_floorBlock;
        float rotationRadius = floorBlock.transform.position.z;

        m_cameraController.Init(rotationRadius);
    }
    void InitPlatformController()
    {
        GameObject platform = m_items.m_mapItems.m_platform;
        GameObject floor = m_items.m_gameplayItems.m_floorBlock;

        float floorWidth = floor.transform.localScale.x;

        m_platformController.Init(platform, floorWidth);
    }
    void InitBallController()
    {
        GameObject ball = m_items.m_mapItems.m_ball;
        GameObject platform = m_items.m_mapItems.m_platform;

        m_ballController.Init(ball, platform);
    }
    void InitSpawner()
    {
        GameObject floor = m_items.m_gameplayItems.m_floorBlock;
        GameObject platform = m_items.m_mapItems.m_platform;

        Vector3 floorScale = floor.transform.localScale;
        Vector3 floorPosition = floor.transform.position;
        float height = platform.transform.position.y;

        m_spawner.Init(floorScale, floorPosition, height);
    }

    void FixedUpdate()
    {
        m_currentBehaviour.UpdateBehavior();
    }

    public ItemsController GetItems()
    {
        return m_items;
    }
    public CameraController GetCameraController()
    {
        return m_cameraController;
    }
    public PlatformController GetPlatformController()
    {
        return m_platformController;
    }
    public BallController GetBallController()
    {
        return m_ballController;
    }
    public Spawner GetSpawner()
    {
        return m_spawner;
    }

    public void SetGameplayBehaviour()
    {
        m_items.DisableAll();
        m_items.EnableGameplayItems();
        m_currentBehaviour = m_gameplay;
        m_currentBehaviour.StartOptions();
    }
    public void SetPause()
    {
        if (m_currentBehaviour == m_gameplay)
        {
            m_items.EnablePauseItems(true);
            m_gameplayBehaviour.SetPause(true);
        }
    }
    public void EndPause()
    {
        if (m_currentBehaviour == m_gameplay)
        {
            m_items.EnablePauseItems(false);
            m_gameplayBehaviour.SetPause(false);
        }
    }
    public void SetMenuBehaviour()
    {
        m_items.DisableAll();
        m_items.EnableMenuItems();
        m_currentBehaviour = m_menu;
        m_currentBehaviour.StartOptions();
    }
    public void SetGameoverBehaviour()
    {
        m_items.DisableAll();
        m_items.EnableGameoverItems();
        m_currentBehaviour = m_gameover;
        m_currentBehaviour.StartOptions();
    }
    public void SetChangeLevelBehaviour()
    {
        m_items.DisableAll();
        m_items.EnableChangeLevelItems();
        m_currentBehaviour = m_changeLevel;
        m_currentBehaviour.StartOptions();
    }
    public void SetExitBehaviour()
    {
        m_items.DisableAll();
        m_items.EnableExitItems();
        m_currentBehaviour = m_exit;
        m_currentBehaviour.StartOptions();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
