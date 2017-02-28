using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum GameMode
{
    PVP,
    AVP,
    AVA,
}

public class GameController : MonoBehaviour
{

    public Text[] buttons;

    public GameObject gameplayObjects;
    public GameObject menuObjects;
    public GameObject choiceObjects;

    public GameObject gameOverPanel;
    public Text gameOverText;

    public GameObject restartButton;
    public GameObject undoButton;

    private Button lastButton;
    private Text lastButtonText;

    private GameMode mode;

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
        ResetAllElements();
        menuObjects.SetActive(true);
    }
    public void SetGameScene()
    {
        ResetAllElements();
        gameplayObjects.SetActive(true);
        RestartGame();
    }
    public void SetChoiceScene()
    {
        ResetAllElements();
        choiceObjects.SetActive(true);
    }
    public void SetLastButton(Button button, Text text)
    {
        lastButton = button;
        lastButtonText = text;
    }
    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }
    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }
    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }
    public void SetPVPMode()
    {
        mode = GameMode.PVP;
    }
    public void SetAVPMode()
    {
        mode = GameMode.AVP;
    }
    public void SetAVAMode()
    {
        mode = GameMode.AVA;
    }
    public void SetPlayerFirst()
    {

    }
    public void SetComputerFirst()
    {

    }

    public string GetPlayerSide()
    {
        return side;
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

    public void ResetAllElements()
    {
        gameplayObjects.SetActive(false);
        menuObjects.SetActive(false);
        choiceObjects.SetActive(false);
    }
    void GameOver(string message = "")
    {
        if (message == "")
        {
            SetGameOverText(side + " Wins");
        }
        else
        {
            SetGameOverText(message);
        }
        SetBoardInteractable(false);
        restartButton.SetActive(true);
        undoButton.SetActive(false);
    }
    public void RestartGame()
    {
        side = "X";
        moveCount = 0;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        undoButton.SetActive(false);
        SetBoardInteractable(true);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].text = "";
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
    public void ExitGame()
    {
        Application.Quit();
    }
}