using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public PlayerManager playerManager;
    public Button redButton;
    public Button blueButton;
    public Button greenButton;
    // Start is called before the first frame update
    void Start()
    {
        redButton.onClick.AddListener(SetRedColor);
        blueButton.onClick.AddListener(SetBlueColor);
        greenButton.onClick.AddListener(SetGreenColor);
    }

    public void SetRedColor()
    {
        Color customRed;
        if (ColorUtility.TryParseHtmlString("#CF2141", out customRed))
        {
            playerManager.sprite.color = customRed;
        }
    }

    public void SetBlueColor()
    {
        Color customBlue;
        if (ColorUtility.TryParseHtmlString("#2B6BFF", out customBlue))
        {
            playerManager.sprite.color = customBlue;
        }
    }

    public void SetGreenColor()
    {
        Color customGreen;
        if (ColorUtility.TryParseHtmlString("#00FF6E", out customGreen))
        {
            playerManager.sprite.color = customGreen;
        }
    }
}
