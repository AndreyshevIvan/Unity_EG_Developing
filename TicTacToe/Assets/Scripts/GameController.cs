using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum TurnQueue
{
    NONE,
    PLAYER,
    COMPUTER,
}

public enum GameMode
{
    PAUSE,
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
    public GameObject turnQueueTable;

    private Button lastButton;
    private Text lastButtonText;

    public Color winnerFields;
    public Color normalFields;

    private TurnQueue turnQueue;
    private GameMode mode = GameMode.PAUSE;

    private string side;
    private int moveCount;

    private int winnerFirst = -1;
    private int winnerSecond = -1;
    private int winnerThird = -1;

    private float computerDelay;
    private float maxDelay = 1.2f;

    void Awake()
    {
        side = "X";
        moveCount = 0;
        computerDelay = 0;

        InitGameControllerReferenceOnButtons();
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        undoButton.SetActive(false);

        SetMenuScene();
    }
    void InitGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    private void Update()
    {
        switch(mode)
        {
            case GameMode.PVP:
                {

                }
                break;
            case GameMode.AVP:
                {

                }
                break;
            case GameMode.AVA:
                {
                }
                break;
            case GameMode.PAUSE:
                {

                }
                break;
        }
    }
    public void UpdateUndoButton(Button button, Text text)
    {
        lastButton = button;
        lastButtonText = text;
    }
    private void UpdateCurrentTurn()
    {
        turnQueueTable.GetComponentInChildren<Text>().text = side + " turn";
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

    public void ResetInterfaces()
    {
        gameplayObjects.SetActive(false);
        menuObjects.SetActive(false);
        choiceObjects.SetActive(false);
    }
    public void SetMenuScene()
    {
        ResetInterfaces();
        menuObjects.SetActive(true);
    }
    public void SetGameScene()
    {
        ResetInterfaces();
        gameplayObjects.SetActive(true);
        RestartGame();
    }
    public void SetChoiceScene()
    {
        ResetInterfaces();
        choiceObjects.SetActive(true);
    }

    private void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInParent<Button>().interactable = toggle;
        }
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

    public void UndoLastTurn()
    {
        undoButton.SetActive(false);
        lastButton.interactable = true;
        lastButtonText.text = "";
        --moveCount;
        ChangeSides();
    }
    public void Turn()
    {
        ++moveCount;

        if (IsSomobodyWin() || moveCount >= 9)
        {
            GameOver();
        }

        ChangeSides();
    }
    void ChangeSides()
    {
        side = (side == "X") ? "O" : "X";
    }

    void GameOver()
    {
        mode = GameMode.PAUSE;
        PaintWinnerFields();

        if (IsSomobodyWin())
        {
            playersController.HighlightWinner(side);
            string winnerName = playersController.GetWinnerName(side);
            CreateGameOverText(winnerName + " Wins");
        }
        else
        {
            CreateGameOverText("It`s a draw");
        }

        SetBoardInteractable(false);
        restartButton.SetActive(true);
        undoButton.SetActive(false);
    }
    void CreateGameOverText(string text)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = text;
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

    public void ExitGame()
    {
        Application.Quit();
    }
}