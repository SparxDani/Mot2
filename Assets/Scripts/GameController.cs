using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject resultsScreen;
    public Chronometer chronometer;
    public TMP_Text finalTimeText;
    public TMP_Text resultText;
    public GameObject retryButton;
    public GameObject nextLevelButton;
    public GameObject mainMenuButton;

    void Start()
    {
        resultsScreen.SetActive(false);
    }

    public void OnPlayerWin()
    {
        chronometer.StopTimer();
        ShowResultsScreen("LEVEL CLEAR");
        retryButton.SetActive(true);
        nextLevelButton.SetActive(true);
        mainMenuButton.SetActive(true);
    }

    public void OnPlayerLose()
    {
        chronometer.StopTimer();
        ShowResultsScreen("BAD");
        retryButton.SetActive(true);
        nextLevelButton.SetActive(false);
        mainMenuButton.SetActive(true);
    }

    void ShowResultsScreen(string result)
    {
        resultsScreen.SetActive(true);
        float elapsedTime = chronometer.GetElapsedTime();
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 100F) % 100F);

        finalTimeText.text = string.Format("Final Time: {0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
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
        SceneManager.LoadScene("MainMenú");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            OnPlayerWin();
        }
    }
}
