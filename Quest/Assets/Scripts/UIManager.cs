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
    public delegate void OnCloseChatsEvent();
    public OnCloseChatsEvent onCloseChats;

    PlayerTurnEvents m_playerTurnEvents;

    public MessagesBox m_messageBox;

    public GameObject m_profilePanel;
    public GameObject m_rooms;
    public GameObject m_bookmarks;
    public GameObject m_credits;

    public GameObject[] m_newMsgAnnouncers;

    public GameObject m_chatFirst;
    Vector3 m_chatFirstStartPos;
    bool m_isChatFirstClose = false;

    public Text m_bookmarksField;
    public Text m_playerName;
    public Text m_coldownIgnore;
    bool m_isRoomsPanelOpen = false;
    bool m_isLoadEnded = false;
    bool m_isNewMessageExist = false;

    public bool isLoadEnded
    {
        get { return m_isLoadEnded; }
    }

    readonly Vector3 PANEL_SPEED = new Vector3(3400, 0, 0);

    void Awake()
    {
        m_playerTurnEvents = new PlayerTurnEvents();
        m_messageBox.playerTurnEvent = m_playerTurnEvents;
        m_playerName.text = DataManager.GetPlayerName() + "\n8 8 800 555 03 35";
        CloseAll();

        m_chatFirstStartPos = m_chatFirst.transform.position;
    }

    void Start()
    {
        InitProfilePanel();
    }

    private void FixedUpdate()
    {
        UpdateChatIcons();
        UpdateProfilePanel();
        UpdateNewMsgAnnouners();
        HandleTouch();

    }
    void UpdateChatIcons()
    {

    }
    void UpdateProfilePanel()
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
    void UpdateNewMsgAnnouners()
    {
        foreach (GameObject announcer in m_newMsgAnnouncers)
        {
            announcer.SetActive(m_isNewMessageExist);
        }

        m_isNewMessageExist = false;
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

        if (Input.GetKey(KeyCode.Escape))
        {
            OpenProfile();
        }
    }

    public void CloseChatFirst()
    {
        m_isChatFirstClose = !m_isChatFirstClose;
        Vector3 movement = new Vector3(10000, 0, 0);

        if (m_isChatFirstClose)
        {
            m_chatFirst.transform.position += movement;
        }
        else
        {
            m_chatFirst.transform.position = m_chatFirstStartPos;
        }
    }

    public string GetBookmarks()
    {
        return m_bookmarksField.text;
    }

    public void InitProfilePanel()
    {
        bool isClose = false;

        while (!isClose)
        {
            Vector3 panelPosition = m_profilePanel.transform.position;
            Vector3 moveStep = new Vector3(-10, 0, 0);

            float panelWidthX = m_profilePanel.GetComponent<RectTransform>().rect.width;
            m_profilePanel.transform.Translate(moveStep);

            isClose = panelPosition.x + panelWidthX < 0;
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
    public void NewMessageAnnounce(bool isNewExist)
    {
        if (isNewExist)
        {
            m_isNewMessageExist = isNewExist;
        }
    }

    public void SetBookmarks(string bookmarks)
    {
        m_bookmarksField.text = bookmarks;
    }
    public void SetColdownIgnore(bool isIgnore)
    {
        string text = "Быстрая игра: ";

        if (isIgnore)
        {
            text += "ВКЛ";
        }
        else
        {
            text += "ВЫКЛ";
        }

        m_coldownIgnore.text = text;
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

    void CloseAll()
    {
        m_rooms.SetActive(false);
        m_bookmarks.SetActive(false);
        m_credits.SetActive(false);

        if (onCloseChats != null)
        {
            onCloseChats();
        }
    }

    public void SetMariChat()
    {
        CloseAll();

    }
    public void SetAgentChat()
    {
        CloseAll();

    }
    public void SetFriendChat()
    {
        CloseAll();

    }

    public void OpenProfile()
    {
        m_isRoomsPanelOpen = true;
    }
    public void CloseProfile()
    {
        m_isRoomsPanelOpen = false;
    }

    public void OpenRoomsPage(bool isOpen)
    {
        CloseAll();
        m_rooms.SetActive(isOpen);
    }
    public void OpenBookmarksPage(bool isOpen)
    {
        CloseAll();
        m_bookmarks.SetActive(isOpen);
    }
    public void OpenCreditsPage(bool isOpen)
    {
        CloseAll();
        m_credits.SetActive(isOpen);
    }
}
