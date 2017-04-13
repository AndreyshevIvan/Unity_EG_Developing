using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void PlayerEvent(int newState, string message);

public interface IMessagesBox
{
    void ImitatePrint();
    void AddPlayerTurnEvent(PlayerEvent turnEvent);
    void LoadFromHistory(History history);
    void InitPlayerAnswers(List<UserReplica> replics);
    void AddComputerMessage(UserReplica replica);
    void SetVisible(bool isVisible);
}
