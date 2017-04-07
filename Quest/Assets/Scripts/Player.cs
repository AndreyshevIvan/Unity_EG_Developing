using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : User
{
    public Player(IMessagesBox messageBox)
        : base(messageBox)
    {

    }

    public override bool SendMessage()
    {
        return base.SendMessage();
    }
}
