using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    // buttons
    private Button backToMenuButton;
    private Button restartButton;
    
    // UI
    public TextMeshProUGUI scoreCounterText;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverText;
    public GameObject speedUpText;
    public GameObject newHighScoreText;
    private int numFlashes = 3;
    private float flashesWaitTime = 0.2f;

    // other managers
    public SpawnManager spawnManager;
    public LevelLoader levelLoader;

    // scores and levels
    private int points;
    private static int highScore;
    private bool firstTimeHighScore;
    public float level;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateButtons();
        UpdateHighScore(highScore);

        points = 0;
        firstTimeHighScore = true;
        level = 1f;
    }

    private void CreateButtons()
    {
        // Initialize buttons
        backToMenuButton = GameObject.Find("Back To Menu Button").GetComponent<Button>();
        restartButton = GameObject.Find("Restart Button").GetComponent<Button>();

        // Add listeners to buttons
        backToMenuButton.onClick.AddListener(BackToMenu);
        restartButton.onClick.AddListener(Restart);

        // Hide Restart Button
        restartButton.gameObject.SetActive(false);
    }

    private void BackToMenu()
    {
        levelLoader.LoadLevel("Title");
    }

    private void Restart()
    {
        levelLoader.LoadLevel("Main");
    }

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;

        if (points > highScore)
        {
            UpdateHighScore(points);
        }

        if (points == 1)
        {
            scoreCounterText.text = points + " Point";  
        }
        else
        {
            scoreCounterText.text = points + " Points";
        }

        if (points % 10 == 0)
        {
            LevelUp();
        }
    }

    private void UpdateHighScore(int newHighScore)
    {
        highScore = newHighScore;
        highScoreText.text = "High Score: " + highScore;

        if (firstTimeHighScore)
        {
            StartCoroutine(FlashText(newHighScoreText, numFlashes, flashesWaitTime));
            firstTimeHighScore = false;
        }
    }

    public void LevelUp()
    {
        level += 0.25f;
        StartCoroutine(FlashText(speedUpText, numFlashes, flashesWaitTime));
        spawnManager.SpeedUp(level);
    }

    IEnumerator FlashText(GameObject text, int flashTimes, float waitTime)
    {
        for (int i = 0; i < flashTimes; i++)
        {
            text.SetActive(true);
            yield return new WaitForSeconds(waitTime);
            text.SetActive(false);
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void GameOver()
    {
        gameOverText.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
}
