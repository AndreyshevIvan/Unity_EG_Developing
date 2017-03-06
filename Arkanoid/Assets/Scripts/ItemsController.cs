using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsController : MonoBehaviour
{

    public MenuItems m_menuItems;
    public GameoverItems m_gameoverItems;
    public GameplayItems m_gameplayItems;
    public PauseItems m_pauseItems;
    public ExitItems m_exitItems;
    public ChangeLevelItems m_changeLevelItems;
    public MapItems m_mapItems;

    public void DisableAll()
    {
        m_menuItems.SetActive(false);
        m_gameoverItems.SetActive(false);
        m_gameplayItems.SetActive(false);
        m_pauseItems.SetActive(false);
        m_exitItems.SetActive(false);
        m_changeLevelItems.SetActive(false);
        m_mapItems.SetActive(false);
    }

    public void EnableMenuItems()
    {
        m_menuItems.SetActive(true);
    }
    public void EnableGameplayItems()
    {
        m_gameplayItems.SetActive(true);
        m_mapItems.SetActive(true);
    }
    public void EnableGameoverItems()
    {
        m_gameoverItems.SetActive(true);
    }
    public void EnablePauseItems(bool isPauseActive)
    {
        m_pauseItems.SetActive(isPauseActive);
        HideGameplayButtons(isPauseActive);
    }
    public void EnableExitItems()
    {
        m_exitItems.SetActive(true);
    }
    public void EnableChangeLevelItems()
    {
        m_changeLevelItems.SetActive(true);
    }

    private void HideGameplayButtons(bool isPauseActive)
    {
        m_gameplayItems.m_gameoverButton.SetActive(!isPauseActive);
        m_gameplayItems.m_pauseButton.SetActive(!isPauseActive);
    }
}
