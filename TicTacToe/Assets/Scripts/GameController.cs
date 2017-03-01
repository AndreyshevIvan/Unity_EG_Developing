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

public class Bot
{
    public Bot()
    {
        m_time = 0;
        m_isActive = false;

        SetDelay();
    }

    private float m_time;
    private float m_delay;
    private bool m_isActive;

    public bool IsReady()
    {
        return m_time >= m_delay;
    }
    public void Start()
    {
        m_isActive = true;
    }
    public void Stop()
    {
        m_isActive = false;
    }
    public void Restart()
    {
        Start();
        m_time = 0;
    }
    public void Update()
    {
        if (m_isActive && m_time <= m_delay)
        {
            Debug.Log("Add time");
            m_time += Time.deltaTime;
        }
    }
    public void Turn(Text[] buttons)
    {
        GridSpace button = GetTurn(buttons);

        if (button != null)
        {
            button.SetSpace();

            Restart();
            SetDelay();

            Debug.Log("Test 3");
        }
    }

    private GridSpace GetTurn(Text[] buttons)
    {
        if (IsButtonValid(buttons[0]) ||
            IsButtonValid(buttons[1]) ||
            IsButtonValid(buttons[2]) ||
            IsButtonValid(buttons[3]) ||
            IsButtonValid(buttons[4]) ||
            IsButtonValid(buttons[5]) ||
            IsButtonValid(buttons[6]) ||
            IsButtonValid(buttons[7]) ||
            IsButtonValid(buttons[8]))
        {
            int turn = 0;
            bool isTurnValid = false;

            while (!isTurnValid)
            {
                turn = Random.Range(0, 9);
                if (IsButtonValid(buttons[turn]))
                {
                    isTurnValid = true;
                }
            }

            GridSpace button = buttons[turn].GetComponentInParent<GridSpace>();

            return button;
        }

        return null;
    }
    private bool IsButtonValid(Text button)
    {
        return (button != null && button.text == "");
    }
    private void SetDelay()
    {
        m_delay = Random.Range(0.1f, 0.2f);
    }
}

public class GameController : MonoBehaviour
{
    public PlayersController playersController;

    Bot jonnyBot;

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

    private string side;
    private int moveCount;

    private int winnerFirst = -1;
    private int winnerSecond = -1;
    private int winnerThird = -1;

    void Awake()
    {
        jonnyBot = new Bot();
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
        jonnyBot.Update();

        if (mode == GameMode.AVA)
        {
            if (jonnyBot.IsReady())
                jonnyBot.Turn(buttons);
        }

        if (mode == GameMode.AVP)
        {
            if (!isPlayerTurn)
            {
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
            currentTurn.GetComponentInChildren<Text>().text = side + " turn";
        }
    }

    public void RestartGame()
    {
        SetGameplayInterface();
        EnableBoardForPlayer();

        if (isPlayerFirstInMatch)
        {
            side = playersController.GetFirstPlayerSide();
        }
        else
        {
            side = playersController.GetSecondPlayerSide();
        }
        moveCount = 0;
        winnerFirst = -1;
        winnerSecond = -1;
        winnerThird = -1;

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
        ResetInterfaces();
        mode = GameMode.PAUSE;
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
        PaintWinnerFields();
        DisableBoardForPlayer();
        restartButton.SetActive(true);
        undoButton.SetActive(false);
        currentTurn.SetActive(false);
        CreateGameOverMessage(message);
    }

    private void EnableBoardForPlayer()
    {
        SetBoardInteractable(true);
    }
    private void DisableBoardForPlayer()
    {
        SetBoardInteractable(false);
    } 
    private void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] != null && buttons[i].GetComponentInParent<Button>() != null)
                buttons[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }
    public void SetPVPMode()
    {
        futureGameMode = GameMode.PVP;
        playersController.SetPVPMode();
        playersController.NormalInit();
        SetGameScene();
    }
    public void SetAVPMode()
    {
        futureGameMode = GameMode.AVP;
        SetGameScene();
    }
    public void SetAVAMode()
    {
        futureGameMode = GameMode.AVA;
        playersController.SetAVAMode();
        playersController.NormalInit();
        SetGameScene();
    }
    public void SetPlayerFirst()
    {
        playersController.SetAVPMode();
        playersController.NormalInit();
        isPlayerFirstInMatch = true;
        SetAVPMode();
    }
    public void SetComputerFirst()
    {
        playersController.SetAVPMode();
        playersController.InvertInit();
        isPlayerFirstInMatch = false;
        SetAVPMode();
    }

    public string GetPlayerSide()
    {
        return side;
    }

    public void UndoLastTurn()
    {
        undoButton.SetActive(false);
        lastButton.interactable = true;
        lastButton.GetComponentInChildren<Text>().text = "";
        --moveCount;
        ChangeSides();
    }
    public void PlayerTurn()
    {
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
        side = (side == "X") ? "O" : "X";
    }

    private void GameOver()
    {
        mode = GameMode.PAUSE;
        jonnyBot.Stop();
        string message = "It`s a draw";

        if (IsSomobodyWin())
        {
            playersController.HighlightWinner(side);
            message = playersController.GetWinnerName(side) + " Wins";
        }

        SetGameOverInterface(message);
    }
    private void CreateGameOverMessage(string message)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = message;
    }

    private bool IsSomobodyWin()
    {
        // 0 1 2
        // 3 4 5
        // 6 7 8
        bool isSomobodyWin = (
        CheckCombinationAndMemorize(0, 1, 2) ||
        CheckCombinationAndMemorize(3, 4, 5) ||
        CheckCombinationAndMemorize(6, 7, 8) ||
        CheckCombinationAndMemorize(0, 3, 6) ||
        CheckCombinationAndMemorize(1, 4, 7) ||
        CheckCombinationAndMemorize(2, 5, 8) ||
        CheckCombinationAndMemorize(0, 4, 8) ||
        CheckCombinationAndMemorize(2, 4, 6)
        );

        return isSomobodyWin;
    }
    private bool CheckCombinationAndMemorize(int firstField, int secondField, int thirdField)
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