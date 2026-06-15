using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float timeRemaining = 180f;

    public TMP_Text timerText;
    public TMP_Text scoreText;

    public int score = 0;

    bool gameActive = true;

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        if (!gameActive)
            return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            gameActive = false;
            Debug.Log("Game Over!");
        }

        UpdateTimerText();
    }

    public void AddScore(int amount)
    {
        if (!gameActive)
            return;

        score += amount;
        UpdateScoreText();
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = "Time: " + minutes + ":" + seconds.ToString("00");
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}