using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static Action<int, int> OnHealthUpdated;
    public static Action<int> OnPointsUpdated;      
    public static Action OnPlayerWon;
    public static Action OnPlayerDefeated;

    public static void UpdateHealth(int currentHealth, int maxHealth)
    {
        OnHealthUpdated?.Invoke(currentHealth, maxHealth); 
    }

    public static void UpdatePoints(int points)
    {
        OnPointsUpdated?.Invoke(points);
    }

    public static void PlayerWon()
    {
        OnPlayerWon?.Invoke();
    }

    public static void PlayerDefeated()
    {
        OnPlayerDefeated?.Invoke();
    }
}
