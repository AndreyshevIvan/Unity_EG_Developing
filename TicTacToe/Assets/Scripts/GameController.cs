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

abstract class Bot
{
    private float m_time = 0;
    private float m_delay = 1;
    private bool m_isActive = false;

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
            m_time += Time.deltaTime;
        }
    }
    public virtual void Turn(Text[] buttons)
    {
        if (IsEmptyFieldExist(buttons))
        {
            GridSpace button = GetTurn(buttons);

            if (button != null)
            {
                button.SetSpace();

                Restart();
                SetDelay();
            }
        }
    }

    public abstract GridSpace GetTurn(Text[] buttons);

    private bool IsEmptyFieldExist(Text[] buttons)
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
            return true;
        }

        return false;
    }
    protected bool IsButtonValid(Text button)
    {
        return (button != null && button.text == "");
    }
    private void SetDelay()
    {
        m_delay = Random.Range(0.4f, 1.0f);
    }
}

class RandomBot : Bot
{
    public override GridSpace GetTurn(Text[] buttons)
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
}

public class GameController : MonoBehaviour
{
    public PlayersController playersController;
    private RandomBot jonnyBot;

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

    private string side;
    private int moveCount;

    void Awake()
    {
        jonnyBot = new RandomBot();
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
            currentTurn.GetComponentInChildren<Text>().text = side + " turn";
        }
    }

    public void RestartGame()
    {
        SetGameplayInterface();
        SetBoardInteractable(true);

        if (isPlayerFirstInMatch)
        {
            side = playersController.GetFirstPlayerSide();
        }
        else
        {
            side = playersController.GetSecondPlayerSide();
        }
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
        return side;
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
        side = (side == "X") ? "O" : "X";
    }

    private void GameOver()
    {
        PlayGameOverEffect();
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

        return (playerSide == side);
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
        if (buttons[firstField].text == side &&
            buttons[secondField].text == side &&
            buttons[thirdField].text == side)
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