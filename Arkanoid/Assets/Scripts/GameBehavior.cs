using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GameBehavior
{
    void Init(GameController controller);
    void UpdateBehavior();
}
