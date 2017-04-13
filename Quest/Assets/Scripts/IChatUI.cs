using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IChatUI
{
    void NewMessageAnnounce(bool isNewExist);
    void SetChatIcon(Sprite icon);
    void SetChatName(string name);
}
