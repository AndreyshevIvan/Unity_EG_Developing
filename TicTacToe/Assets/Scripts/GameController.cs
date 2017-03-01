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
    public PlayersController playersController;

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

    public Color winnerFields;
    public Color normalFields;

    private GameMode mode;

    private string side;
    private int moveCount;

    private int winnerFirst = -1;
    private int winnerSecond = -1;
    private int winnerThird = -1;

    private float computerDelay;

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

    private void Update()
    {

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
        playersController.SetPVPMode();
        playersController.NormalInit();
    }
    public void SetAVPMode()
    {
        mode = GameMode.AVP;
        playersController.SetAVPMode();
    }
    public void SetAVAMode()
    {
        mode = GameMode.AVA;
        playersController.SetAVAMode();
        playersController.NormalInit();
    }
    public void SetPlayerFirst()
    {
        playersController.NormalInit();
    }
    public void SetComputerFirst()
    {
        playersController.InvertInit();
    }

    public string GetPlayerSide()
    {
        return side;
    }
    public GameMode GetMode()
    {
        return mode;
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
        PaintWinnerFields();
        if (message == "")
        {
            playersController.HighlightWinner(side);
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
    private void PaintWinnerFields()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i != winnerFirst && i != winnerSecond && i != winnerThird)
            {
                buttons[i].color = normalFields;
            }
        }
    }
    public void RestartGame()
    {
        side = "X";
        moveCount = 0;
        playersController.Reset();
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        undoButton.SetActive(false);
        SetBoardInteractable(true);

        winnerFirst = -1;
        winnerSecond = -1;
        winnerThird = -1;

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].text = "";
            buttons[i].color = winnerFields;
        }
    }
    private bool IsSomobodyWin()
    {
        // 0 1 2
        // 3 4 5
        // 6 7 8
        bool isSomobodyWin = (
        CheckCombinationAndMemorization(0, 1, 2) ||
        CheckCombinationAndMemorization(3, 4, 5) ||
        CheckCombinationAndMemorization(6, 7, 8) ||
        CheckCombinationAndMemorization(0, 3, 6) ||
        CheckCombinationAndMemorization(1, 4, 7) ||
        CheckCombinationAndMemorization(2, 5, 8) ||
        CheckCombinationAndMemorization(0, 4, 8) ||
        CheckCombinationAndMemorization(2, 4, 6)
        );

        return isSomobodyWin;
    }
    private bool CheckCombinationAndMemorization(int firstField, int secondField, int thirdField)
    {
        if (buttons[firstField].text == side &&
            buttons[secondField].text == side &&
            buttons[thirdField].text == side)
        {
            DefineWinFields(firstField, secondField, thirdField);
            return true;
        }

        return false;
    }
    private void DefineWinFields(int firstField, int secondField, int thirdField)
    {
        winnerFirst = firstField;
        winnerSecond = secondField;
        winnerThird = thirdField;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}