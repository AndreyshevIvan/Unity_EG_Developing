using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void PlayerEvent(int newState);

public interface IMessagesBox
{
    void ImitatePrint();
    void AddPlayerTurnEvent(PlayerEvent turnEvent);

    void SetChatIcon(Image icon);
    void SetChatName(string name);
    void SetHistory(string chatName);

    void SetPlayerReplics(List<PlayerReplica> replics);
    void SetComputerReplica(string replica);
}
