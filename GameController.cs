using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameController : MonoBehaviour
{
    public GameObject resultsScreen;
    public UImanager chronometer;
    public TMP_Text finalTimeText;
    public TMP_Text resultText;
    public GameObject retryButton;
    public GameObject nextLevelButton;
    public GameObject mainMenuButton;

    private void OnEnable()
    {
        EventManager.OnPlayerWon += OnPlayerWin;
        EventManager.OnPlayerDefeated += OnPlayerLose;
    }
     
    private void OnDisable()
    {
        EventManager.OnPlayerWon -= OnPlayerWin;
        EventManager.OnPlayerDefeated -= OnPlayerLose;
    }

    public void OnPlayerWin()
    {
        chronometer.StopChronometer();
        ShowResultsScreen("LEVEL CLEAR");
        retryButton.SetActive(true);
        nextLevelButton.SetActive(true);
        mainMenuButton.SetActive(true);
    }

    public void OnPlayerLose()
    {
        chronometer.StopChronometer();
        ShowResultsScreen("BAD");
        retryButton.SetActive(true);
        nextLevelButton.SetActive(false);
        mainMenuButton.SetActive(true);
    }

    void ShowResultsScreen(string result)
    {
        resultsScreen.SetActive(true);
        finalTimeText.text = chronometer.FormatElapsedTime();
        resultText.text = result;
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
