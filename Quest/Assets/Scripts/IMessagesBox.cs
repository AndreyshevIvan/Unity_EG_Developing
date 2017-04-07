using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void PlayerEvent();

public interface IMessagesBox
{
    void ImitatePrint();
    void AddPlayerTurnEvent(PlayerEvent turnEvent);

    void SetChatIcon(Image icon);
    void SetChatName(string name);
    void SetHistory(string chatName);
    void SetPlayerReplica(string[] playerReplica);
    void SetComputerReplica(string[] replica);
}
