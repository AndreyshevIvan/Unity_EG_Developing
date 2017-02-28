using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    public Text buttonText;
    public int addingSize = 4;

    public Color normalColor;
    public Color selectingColor;

    private Color disableImageColor;
    public Image buttonIcon;

    public void ButtonOnTarget()
    {
        buttonText.color = selectingColor;
        buttonText.fontSize += addingSize;

        if (buttonIcon != null)
        {
            disableImageColor = buttonIcon.color;
            buttonIcon.color = Color.white;
        }
    }

    public void EndTarget()
    {
        buttonText.color = normalColor;
        buttonText.fontSize -= addingSize;

        if (buttonIcon != null)
        {
            buttonIcon.color = disableImageColor;
        }
    }
}