using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent<int> OnPointsUpdated;
    public static UnityEvent<int> OnHealthUpdated;
    public static UnityEvent OnPlayerDefeated = new UnityEvent();
    public static UnityEvent OnPlayerWon = new UnityEvent();


}
