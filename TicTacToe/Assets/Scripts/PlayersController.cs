using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
    public GameObject body;
    public Text title;
    public string side;

    private string name = "";
    public Color normalColor;

    public string GetSide()
    {
        return side;
    }
    public string GetTitle()
    {
        return side + " - " + name;
    }
    public Vector3 GetCrownPosition()
    {
        return title.transform.position;
    }

    public void SetSide(string playerSide)
    {
        side = playerSide;
    }
    public void SetActive(bool toggle)
    {
        body.SetActive(toggle);
    }
    public void SetName(string playerName)
    {
        name = playerName;
    }
    public void SetTitle()
    {
        title.text = side + " - " + name;
    }

    public void Hide()
    {
        title.color = Color.gray;
        body.GetComponent<Image>().color = Color.gray;
    }
    public void Reset()
    {
        title.color = normalColor;
        body.GetComponent<Image>().color = Color.white;
    }
}

public class PlayersController : MonoBehaviour
{
    public Player playerFirst;
    public Player playerSecond;
    public Player computerFirst;
    public Player computerSecond;

    public GameObject crown;

    private Player first;
    private Player second;

    public void NormalInit()
    {
        playerFirst.SetSide("X");
        computerFirst.SetSide("X");
        playerSecond.SetSide("O");
        computerSecond.SetSide("O");
        InitTitles();
    }
    public void InvertInit()
    {
        playerFirst.SetSide("O");
        computerFirst.SetSide("O");
        playerSecond.SetSide("X");
        computerSecond.SetSide("X");
        InitTitles();
    }
    private void InitTitles()
    {
        first.SetTitle();
        second.SetTitle();
    }

    public void Reset()
    {
        crown.SetActive(false);
        if (first != null && second != null)
        {
            first.Reset();
            second.Reset();
        }
    }

    private void HideElements()
    {
        playerFirst.SetActive(false);
        playerSecond.SetActive(false);
        computerFirst.SetActive(false);
        computerSecond.SetActive(false);
        crown.SetActive(false);
    }
    public void SetPVPMode()
    {
        HideElements();
        first = playerFirst;
        first.SetName("player");
        second = playerSecond;
        second.SetName("player");
        ActivatePlayers();
    }
    public void SetAVPMode()
    {
        HideElements();
        first = playerFirst;
        first.SetName("player");
        second = computerSecond;
        second.SetName("computer");
        ActivatePlayers();
    }
    public void SetAVAMode()
    {
        HideElements();
        first = computerFirst;
        first.SetName("computer");
        second = computerSecond;
        second.SetName("computer");
        ActivatePlayers();
    }
    private void ActivatePlayers()
    {
        first.SetActive(true);
        second.SetActive(true);
    }

    public string GetFirstPlayerSide()
    {
        return first.GetSide();
    }
    public string GetSecondPlayerSide()
    {
        return second.GetSide();
    }

    public void HighlightWinner(string winSide)
    {
        crown.SetActive(true);

        if (first.GetSide() == winSide)
        {
            crown.transform.position = first.GetCrownPosition();
            second.Hide();
        }
        else
        {
            crown.transform.position = second.GetCrownPosition();
            first.Hide();
        }
    }
}