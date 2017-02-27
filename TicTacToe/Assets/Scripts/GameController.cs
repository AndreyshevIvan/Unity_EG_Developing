using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
    public Text[] buttons;

    public GameObject gameplayObjects;
    public GameObject menuObjects;
    public GameObject modeMenuObjects;

    public GameObject gameOverPanel;
    public Text gameOverText;

    public GameObject restartButton;
    public GameObject undoButton;

    private Button lastButton;
    private Text lastButtonText;

    private string side;
    private int moveCount;

    void Awake()
    {
        side = "X";
        SetGameControllerReferenceOnButtons();
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        undoButton.SetActive(false);
        SetBoardInteractable(true);
        moveCount = 0;

        SetMenuScene();
    }

    public void SetMenuScene()
    {
        //ResetAllElements();
        menuObjects.SetActive(true);
    }

    public void SetChangeModeScene()
    {
        ResetAllElements();
        modeMenuObjects.SetActive(true);
    }

    public void SetGameScene()
    {
        ResetAllElements();
        gameplayObjects.SetActive(true);
    }

    private void ResetAllElements()
    {
        gameplayObjects.SetActive(false);
        menuObjects.SetActive(false);
        modeMenuObjects.SetActive(false);
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
        return side;
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
        side = (side == "X") ? "O" : "X";
    }
    void GameOver(string message = "")
    {
        if (message == "")
        {
            SetGameOverText(side + " Wins!");
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
        side = "X";
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
        if ((buttons[0].text == side && buttons[1].text == side && buttons[2].text == side) ||
            (buttons[3].text == side && buttons[4].text == side && buttons[5].text == side) ||
            (buttons[6].text == side && buttons[7].text == side && buttons[8].text == side) ||
            (buttons[0].text == side && buttons[3].text == side && buttons[6].text == side) ||
            (buttons[1].text == side && buttons[4].text == side && buttons[7].text == side) ||
            (buttons[2].text == side && buttons[5].text == side && buttons[8].text == side) ||
            (buttons[0].text == side && buttons[4].text == side && buttons[8].text == side) ||
            (buttons[2].text == side && buttons[4].text == side && buttons[6].text == side))
        {
            return true;
        }

        return false;
    }
}