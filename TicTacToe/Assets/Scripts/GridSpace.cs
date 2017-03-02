using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{
    public AudioEffects audioEffects;

    public Button button;
    public Text buttonText;
    private GameController gameController;

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }

    public void SetSpace()
    {
        audioEffects.SetGrid();
        buttonText.text = gameController.GetPlayerSide();
        button.interactable = false;
        gameController.Turn();
        gameController.UpdateUndoButton(button);
    }

    public void PlayerTurn()
    {
        if (gameController.IsPlayerTurn())
        {
            gameController.PlayerTurn();
            SetSpace();
        }
    }
}