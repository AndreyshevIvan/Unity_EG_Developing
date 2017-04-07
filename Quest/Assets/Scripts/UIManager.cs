using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IMessagesBox
{
    public GameObject m_roomsPanel;
    bool m_isRoomsPanelOpen = false;
    bool m_isLoadEnded = false;

    public bool isLoadEnded
    {
        get { return m_isLoadEnded; }
    }

    readonly Vector3 PANEL_SPEED = new Vector3(3400, 0, 0);

    public void InitChat()
    {

    }

    private void FixedUpdate()
    {
        UpdateChatIcons();
        UpdateRoomsPanel();
        HandleTouch();
    }
    void UpdateChatIcons()
    {

    }
    void UpdateRoomsPanel()
    {
        Vector3 panelPosition = m_roomsPanel.transform.position;
        Vector3 movement = PANEL_SPEED * Time.deltaTime;

        if (m_isRoomsPanelOpen)
        {
            if (panelPosition.x + movement.x < 0)
            {
                m_roomsPanel.transform.Translate(movement);
            }
            else
            {
                m_roomsPanel.transform.position = new Vector3(0, panelPosition.y, panelPosition.z);
            }
        }

        if (!m_isRoomsPanelOpen)
        {
            float panelWidthX = m_roomsPanel.GetComponent<RectTransform>().rect.width;

            if (panelPosition.x + panelWidthX > 0)
            {
                m_roomsPanel.transform.Translate(-movement);
            }
        }
    }
    void HandleTouch()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 clickPos = Input.mousePosition;
            RectTransform transform = m_roomsPanel.GetComponent<RectTransform>();

            if (!transform.rect.Contains(clickPos))
            {
                CloseProfile();
            }
        }
    }

    public void ImitatePrint()
    {

    }

    public void SetChatIcon(Image icon)
    {

    }
    public void SetChatName(string name)
    {

    }
    public void SetHistory(string chatName)
    {

    }
    public void SetPlayerReplica(string[] playerReplica)
    {

    }
    public void SetComputerReplica(string[] replica)
    {

    }

    public void OpenProfile()
    {
        m_isRoomsPanelOpen = true;
    }
    public void CloseProfile()
    {
        m_isRoomsPanelOpen = false;
    }

    public IEnumerator LoadAllHistories(string[] chatNames)
    {
        m_isLoadEnded = true;
        while (true)
        {
            yield return null;
        }
    }
}
