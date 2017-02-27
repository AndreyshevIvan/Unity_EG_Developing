using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class GameController : MonoBehaviour
{
    public Text[] buttons;

    public GameObject gameOverPanel;
    public Text gameOverText;

    public GameObject restartButton;
    public GameObject undoButton;

    private Button lastButton;
    private Text lastButtonText;

    private string playerSide;
    private int moveCount;

    void Awake()
    {
        playerSide = "X";
        SetGameControllerReferenceOnButtons();
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        undoButton.SetActive(false);
        SetBoardInteractable(true);
        moveCount = 0;
    }

    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void SetLastButton(Button button, Text text)
    {
        lastButton = button;
        lastButtonText = text;
    }

    public void UndoLastTurn()
    {
        undoButton.SetActive(false);
        lastButton.interactable = true;
        lastButtonText.text = "";
        --moveCount;
        ChangeSides();
    }

    public void EndTurn()
    {
        ++moveCount;

        if (IsSomobodyWin())
        {
            GameOver();
        }
        else if (moveCount >= 9)
        {
            GameOver("It's a draw!");
        }
        else
        {
            ChangeSides();
            undoButton.SetActive(true);
        }
    }

    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
    }

    void GameOver(string message = "")
    {
        if (message == "")
        {
            SetGameOverText(playerSide + " Wins!");
        }
        else
        {
            SetGameOverText(message);
        }
        SetBoardInteractable(false);
        restartButton.SetActive(true);
        undoButton.SetActive(false);
    }

    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    public void RestartGame()
    {
        playerSide = "X";
        moveCount = 0;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        SetBoardInteractable(true);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].text = "";
        }
    }

    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    public bool IsSomobodyWin()
    {
        if ((buttons[0].text == playerSide && buttons[1].text == playerSide && buttons[2].text == playerSide) ||
            (buttons[3].text == playerSide && buttons[4].text == playerSide && buttons[5].text == playerSide) ||
            (buttons[6].text == playerSide && buttons[7].text == playerSide && buttons[8].text == playerSide) ||
            (buttons[0].text == playerSide && buttons[3].text == playerSide && buttons[6].text == playerSide) ||
            (buttons[1].text == playerSide && buttons[4].text == playerSide && buttons[7].text == playerSide) ||
            (buttons[2].text == playerSide && buttons[5].text == playerSide && buttons[8].text == playerSide) ||
            (buttons[0].text == playerSide && buttons[4].text == playerSide && buttons[8].text == playerSide) ||
            (buttons[2].text == playerSide && buttons[4].text == playerSide && buttons[6].text == playerSide))
        {
            return true;
        }

        return false;
    }
}