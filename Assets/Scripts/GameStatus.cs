using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    public GameObject timeRemainingObj;
    public GameObject gameStatObj;
    public GameObject gameOperObj;
    private Text timeRemainingText;
    private Text gameStatText;
    private Text gameOperText;
    private float timeLeft = 90;

    // Start is called before the first frame update
    void Start()
    {
        timeRemainingText = timeRemainingObj.GetComponent<Text>();
        gameStatText = gameStatObj.GetComponent<Text>();
        gameOperText = gameOperObj.GetComponent<Text>();
        timeRemainingText.text = "Time Remaining: " + timeLeft;
        gameStatText.text = "";
        gameOperText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        } else {
            PauseGame(timeLeft);
        }

        DisplayTime(timeLeft);

        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame(timeLeft);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (timeLeft > 0)
            {
                ResumeGame();
            } else {
                RestartGame();
            }
        }
    }

    void DisplayTime(float time)
    {
        if (time < 0)
        {
            time = 0;
        } else {
            float minutes = Mathf.FloorToInt(time / 60);
            float seconds = Mathf.FloorToInt(time % 60);
            timeRemainingText.text = "Time Remainging: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void PauseGame(float time)
    {
        Time.timeScale = 0;

        if (time > 0)
        {
            DisplayMessage(gameStatText, "Game Paused");
            DisplayMessage(gameOperText, "Resume");
        } else {
            DisplayMessage(gameStatText, "Game Over");
            DisplayMessage(gameOperText, "Restart");
        }
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        gameStatText.text = "";
        gameOperText.text = "";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        gameStatText.text = "";
        gameOperText.text = "";
    }

    void DisplayMessage(Text textArea, string message)
    {
        if (message == "Game Over")
        {
            textArea.text = "Game Over";
        }

        if (message == "Game Paused")
        {
            textArea.text = "Game Paused";
        }

        if (message == "Resume")
        {
            textArea.text = "Resume";
        }

        if (message == "Restart")
        {
            textArea.text = "Restart";
        }
    }
}
