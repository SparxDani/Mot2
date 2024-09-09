using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UImanager : MonoBehaviour
{
    [Header("Health UI")]
    public Image healthBar;
    public int maxHealth = 100;

    [Header("Chronometer UI")]
    public TMP_Text chronometerText;
    private float elapsedTime;
    private bool isChronometerRunning;

    [Header("Points UI")]
    public TMP_Text pointsText;
    private int currentPoints;

    [Header("Results Screen UI")]
    public GameObject resultsScreen;
    public TMP_Text finalTimeText;
    public TMP_Text resultText;
    public GameObject retryButton;
    public GameObject nextLevelButton;
    public GameObject mainMenuButton;

    private void OnEnable()
    {
        EventManager.OnHealthUpdated += UpdateHealthBar;
        EventManager.OnPointsUpdated += UpdatePointsUI; 
        EventManager.OnPlayerWon += ShowVictoryScreen;
        EventManager.OnPlayerDefeated += ShowDefeatScreen;
    }

    private void OnDisable()
    {
        EventManager.OnHealthUpdated -= UpdateHealthBar;
        EventManager.OnPointsUpdated -= UpdatePointsUI;
        EventManager.OnPlayerWon -= ShowVictoryScreen;
        EventManager.OnPlayerDefeated -= ShowDefeatScreen;
    }

    void Start()
    {
        elapsedTime = 0f;
        isChronometerRunning = true;
        resultsScreen.SetActive(false);
        currentPoints = 0;
        UpdatePointsUI(currentPoints); 
    }

    void Update()
    {
        if (isChronometerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateChronometerUI();
        }
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }

    public void UpdatePointsUI(int points)
    {
        currentPoints = points;
        pointsText.text = "Points: " + currentPoints.ToString();
    }

    private void UpdateChronometerUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 100F) % 100F);
        chronometerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    public void StopChronometer()
    {
        isChronometerRunning = false;
    }

    private void ShowVictoryScreen()
    {
        ShowResultsScreen("LEVEL CLEAR");
        retryButton.SetActive(true);
        nextLevelButton.SetActive(true);
        mainMenuButton.SetActive(true);
    }

    private void ShowDefeatScreen()
    {
        ShowResultsScreen("BAD");
        retryButton.SetActive(true);
        nextLevelButton.SetActive(false);
        mainMenuButton.SetActive(true);
    }

    private void ShowResultsScreen(string result)
    {
        resultsScreen.SetActive(true);
        StopChronometer();
        finalTimeText.text = FormatElapsedTime();
        resultText.text = result;
    }

    public string FormatElapsedTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 100F) % 100F);

        return string.Format("Final Time: {0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}
