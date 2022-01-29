using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeRemaining : MonoBehaviour
{
    public GameObject timeRemainingObj;
    public GameObject gameOverObj;
    private Text timeRemainingText;
    private Text gameOverText;
    private float timeLeft = 90;

    // Start is called before the first frame update
    void Start()
    {
        timeRemainingText = timeRemainingObj.GetComponent<Text>();
        gameOverText = gameOverObj.GetComponent<Text>();
        timeRemainingText.text = "Time Remaining: " + timeLeft;
        gameOverText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            DisplayMessage(gameOverText, "Game Over");
            // GameOver();
        }

        DisplayTime(timeLeft);
    }

    void DisplayTime(float time)
    {
        if (time < 0)
        {
            time = 0;
        }
        else
        {
            float minutes = Mathf.FloorToInt(time / 60);
            float seconds = Mathf.FloorToInt(time % 60);
            timeRemainingText.text = "Time Remainging: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void DisplayMessage(Text textArea, string message)
    {
        if (message == "Game Over")
        {
            textArea.text = "Game Over";
        }
    }
}
