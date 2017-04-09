using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnEvents
{
    PlayerEvent m_event;

    public void AddEvent(ref PlayerEvent playerEvent)
    {
        m_event += playerEvent;
    }
    public void RemoveEvent(ref PlayerEvent playerEvent)
    {
        m_event -= playerEvent;
    }
    public void DoEvents(int newState, string message)
    {
        if (m_event != null)
        {
            m_event(newState, message);
        }
    }
}

public class UIManager : MonoBehaviour, IMessagesBox
{
    PlayerTurnEvents m_playerTurnEvents;

    public MessagesBox m_messageBox;
    public GameObject m_profilePanel;
    public Text m_playerName;
    bool m_isRoomsPanelOpen = false;
    bool m_isLoadEnded = false;

    public bool isLoadEnded
    {
        get { return m_isLoadEnded; }
    }

    readonly Vector3 PANEL_SPEED = new Vector3(3400, 0, 0);

    private void Awake()
    {
        m_playerTurnEvents = new PlayerTurnEvents();
        m_messageBox.playerTurnEvent = m_playerTurnEvents;
        m_playerName.text = DataManager.GetPlayerName() + "\n8 8 800 555 03 35";
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
        Vector3 panelPosition = m_profilePanel.transform.position;
        Vector3 movement = PANEL_SPEED * Time.deltaTime;

        if (m_isRoomsPanelOpen)
        {
            if (panelPosition.x + movement.x < 0)
            {
                m_profilePanel.transform.Translate(movement);
            }
            else
            {
                m_profilePanel.transform.position = new Vector3(0, panelPosition.y, panelPosition.z);
            }
        }

        if (!m_isRoomsPanelOpen)
        {
            float panelWidthX = m_profilePanel.GetComponent<RectTransform>().rect.width;

            if (panelPosition.x + panelWidthX > 0)
            {
                m_profilePanel.transform.Translate(-movement);
            }
        }
    }
    void HandleTouch()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 clickPos = Input.mousePosition;
            RectTransform transform = m_profilePanel.GetComponent<RectTransform>();

            if (!transform.rect.Contains(clickPos))
            {
                CloseProfile();
            }
        }
    }

    public void ImitatePrint()
    {
        m_messageBox.ImitatePrint();
    }

    public void AddPlayerTurnEvent(PlayerEvent turnEvent)
    {
        m_playerTurnEvents.AddEvent(ref turnEvent);
    }
    public void LoadFromHistory(History history)
    {
        m_messageBox.Reload(history);
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
    public void SetPlayerReplics(List<UserReplica> replics)
    {
        m_messageBox.InitPlayerReplics(replics);
    }
    public void SetComputerReplica(UserReplica computerReplica)
    {
        m_messageBox.AddComputerMessage(computerReplica);
    }

    public void OpenProfile()
    {
        m_isRoomsPanelOpen = true;
    }
    public void CloseProfile()
    {
        m_isRoomsPanelOpen = false;
    }
}
