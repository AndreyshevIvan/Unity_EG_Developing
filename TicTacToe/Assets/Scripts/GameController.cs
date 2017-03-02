using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    private Bot jonnyBot;

    public BackgroundAudio bgAudio;
    public AudioEffects audioEffects;

    public Text[] buttons;

    public GameObject gameplayObjects;
    public GameObject menuObjects;
    public GameObject choiceObjects;

    public GameObject gameOverPanel;
    public Text gameOverText;

    public GameObject restartButton;
    public GameObject undoButton;
    public GameObject currentTurn;

    private Button lastButton;

    public Color winnerFields;
    public Color normalFields;

    private GameMode futureGameMode;
    private GameMode mode = GameMode.PAUSE;

    private bool isPlayerFirstInMatch = true;
    private bool isPlayerTurn;

    private string turnSide;
    private int moveCount;

    void Awake()
    {
        InitGameControllerReferenceOnButtons();
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
        UpdateCurrentTurn();

        if (mode == GameMode.AVA)
        {
            jonnyBot.Update();
            if (jonnyBot.IsReady())
            {
                jonnyBot.InvertSide();
                jonnyBot.Turn(buttons);
            }
        }

        if (mode == GameMode.AVP)
        {
            if (!isPlayerTurn)
            {
                jonnyBot.Update();
                if (jonnyBot.IsReady())
                {
                    jonnyBot.Turn(buttons);
                    isPlayerTurn = !isPlayerTurn;
                }
            }
        }
    }
    public void UpdateUndoButton(Button button)
    {
        lastButton = button;
    }
    private void UpdateCurrentTurn()
    {
        if (currentTurn != null)
        {
            currentTurn.GetComponentInChildren<Text>().text = turnSide + " turn";
        }
    }

    public void RestartGame()
    {
        SetGameplayInterface();
        SetBoardInteractable(true);

        if (isPlayerFirstInMatch)
        {
            jonnyBot = new AttackBot("O");
        }
        else
        {
            jonnyBot = new AttackBot("X");
        }

        turnSide = "X";
        moveCount = 0;

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].text = "";
            buttons[i].color = winnerFields;
        }

        jonnyBot.Restart();
        mode = futureGameMode;
        isPlayerTurn = isPlayerFirstInMatch;
    }

    private void ResetInterfaces()
    {
        gameplayObjects.SetActive(false);
        menuObjects.SetActive(false);
        choiceObjects.SetActive(false);
    }
    public void SetMenuScene()
    {
        bgAudio.PlayMenuMusic();
        ResetInterfaces();
        mode = GameMode.PAUSE;
        menuObjects.SetActive(true);
    }
    public void SetGameScene()
    {
        bgAudio.PlayGameplayMusic();
        ResetInterfaces();
        gameplayObjects.SetActive(true);
        RestartGame();
    }
    public void SetChoiceScene()
    {
        ResetInterfaces();
        choiceObjects.SetActive(true);
    }
    private void SetGameplayInterface()
    {
        playersController.Reset();
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        undoButton.SetActive(false);
        currentTurn.SetActive(true);
    }
    private void SetGameOverInterface(string message)
    {
        SetBoardInteractable(false);
        restartButton.SetActive(true);
        undoButton.SetActive(false);
        currentTurn.SetActive(false);
        CreateGameOverMessage(message);
    }

    private void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] != null && buttons[i].GetComponentInParent<Button>() != null)
            {
                buttons[i].GetComponentInParent<Button>().interactable = toggle;
            }
        }
    }
    public void SetPVPMode()
    {
        futureGameMode = GameMode.PVP;
        playersController.SetPVPMode();
        playersController.NormalInit();
        isPlayerFirstInMatch = true;
        SetGameScene();
    }
    public void SetAVPMode()
    {
        futureGameMode = GameMode.AVP;
        SetGameScene();
    }
    public void SetAVAMode()
    {
        audioEffects.ComputerHello();
        futureGameMode = GameMode.AVA;
        playersController.SetAVAMode();
        playersController.NormalInit();
        SetGameScene();
    }
    public void SetPlayerFirst()
    {
        audioEffects.PlayerHello();
        playersController.SetAVPMode();
        playersController.NormalInit();
        isPlayerFirstInMatch = true;
        SetAVPMode();
    }
    public void SetComputerFirst()
    {
        audioEffects.ComputerHello();
        playersController.SetAVPMode();
        playersController.InvertInit();
        isPlayerFirstInMatch = false;
        SetAVPMode();
    }

    public string GetPlayerSide()
    {
        return turnSide;
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
    public void UndoLastTurn()
    {
        undoButton.SetActive(false);
        lastButton.interactable = true;
        lastButton.GetComponentInChildren<Text>().text = "";
        --moveCount;
        ChangeSides();

        if (mode == GameMode.AVP)
        {
            jonnyBot.Restart();
            isPlayerTurn = !isPlayerTurn;
        }
    }
    public void PlayerTurn()
    {
        if (mode != GameMode.PAUSE && mode != GameMode.PVP)
            isPlayerTurn = false;
    }
    public void Turn()
    {
        ++moveCount;

        if (IsSomobodyWin() || moveCount >= 9)
        {
            GameOver();
        }
        else
        {
            undoButton.SetActive(true);
            ChangeSides();
        }
    }
    private void ChangeSides()
    {
        turnSide = (turnSide == "X") ? "O" : "X";
    }

    private void GameOver()
    {
        PlayGameOverEffect();
        mode = GameMode.PAUSE;
        jonnyBot.Stop();
        string message = "It`s a draw";

        if (IsSomobodyWin())
        {
            playersController.HighlightWinner(turnSide);
            message = playersController.GetWinnerName(turnSide) + " Wins";
        }

        SetGameOverInterface(message);
    }
    private void CreateGameOverMessage(string message)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = message;
    }
    private void PlayGameOverEffect()
    {
        if (mode == GameMode.AVP)
        {
            if (IsPlayerWin())
            {
                audioEffects.Winner();
            }
            else
            {
                audioEffects.Looser();
            }
        }
        else
        {
            audioEffects.Winner();
        }
    }

    private bool IsPlayerWin()
    {
        string playerSide = playersController.GetFirstPlayerSide();

        return (playerSide == turnSide);
    }
    private bool IsSomobodyWin()
    {
        // 0 1 2
        // 3 4 5
        // 6 7 8
        bool isSomobodyWin = (
            CheckCombinationAndPrint(0, 1, 2) ||
            CheckCombinationAndPrint(3, 4, 5) ||
            CheckCombinationAndPrint(6, 7, 8) ||
            CheckCombinationAndPrint(0, 3, 6) ||
            CheckCombinationAndPrint(1, 4, 7) ||
            CheckCombinationAndPrint(2, 5, 8) ||
            CheckCombinationAndPrint(0, 4, 8) ||
            CheckCombinationAndPrint(2, 4, 6)
        );

        return isSomobodyWin;
    }
    private bool CheckCombinationAndPrint(int firstField, int secondField, int thirdField)
    {
        if (buttons[firstField].text == turnSide &&
            buttons[secondField].text == turnSide &&
            buttons[thirdField].text == turnSide)
        {
            PaintWinnerFields(firstField, secondField, thirdField);
            return true;
        }

        return false;
    }
    private void PaintWinnerFields(int winnerFirst, int winnerSecond, int winnerThird)
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