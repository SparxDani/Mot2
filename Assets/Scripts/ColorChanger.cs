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
    public HealthManager healthManager;
    
















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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            SpriteRenderer enemySprite = other.GetComponent<SpriteRenderer>();
            if (enemySprite != null)
            {
                if (enemySprite.color != playerManager.sprite.color)
                {
                    healthManager.TakeDamage(10);
                }
            }
        }
    }
}
