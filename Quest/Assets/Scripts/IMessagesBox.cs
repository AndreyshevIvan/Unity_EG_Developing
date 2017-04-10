using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void PlayerEvent(int newState, string message);

public interface IMessagesBox
{
    void ImitatePrint();
    void AddPlayerTurnEvent(PlayerEvent turnEvent);
    void NewMessageAnnounce(bool isNewExist);
    void LoadFromHistory(History history);

    void SetChatIcon(Image icon);
    void SetChatName(string name);
    void SetHistory(string chatName);

    void SetPlayerReplics(List<UserReplica> replics);
    void SetComputerReplica(UserReplica replica);
}
