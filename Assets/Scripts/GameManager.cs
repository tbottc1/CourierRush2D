using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public float timeRemaining = 180f;

    public TMP_Text timerText;
    public TMP_Text scoreText;
    public TMP_Text gameOverText;

    public int score = 0;
    public int deliveries = 0;

    public bool gameActive = true;

    void Start()
    {
        gameOverText.gameObject.SetActive(false);

        UpdateScoreText();
        UpdateTimerText();
    }

    void Update()
    {
        if (!gameActive)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            return;
        }

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            EndGame();
        }

        UpdateTimerText();
    }

    public void AddScore(int amount)
    {
        if (!gameActive)
            return;

        score += amount;
        deliveries++;

        UpdateScoreText();
    }

    void EndGame()
    {
        gameActive = false;

        gameOverText.text = "Clocked out! You ended with " + deliveries +
                            " deliveries for a total of " + score +
                            " points.\nHit the space bar to clock back in!";

        gameOverText.gameObject.SetActive(true);

        Debug.Log(gameOverText.text);
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