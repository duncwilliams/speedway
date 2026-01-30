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
    public TextMeshProUGUI gasNumberText;
    public GameObject gameOverText;
    public GameObject speedUpText;
    public GameObject newHighScoreText;
    public GameObject maxSpeedText;
    private int numFlashes = 3;
    private float flashesWaitTime = 0.2f;

    // other managers
    public SpawnManager spawnManager;
    public TextureScroll textureScroll;
    public LevelLoader levelLoader;

    // scores and speed
    private decimal miles;
    private static decimal highScore;
    private bool firstTimeHighScore;
    public bool gameOver;
    private int speed;
    private int maxSpeed = 8;
    private int burnRate;
    public int gas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateButtons();
        UpdateHighScore(highScore);

        miles = 0.001m;
        firstTimeHighScore = true;
        gameOver = false;
        speed = 1;
        burnRate = 1;
        gas = 100;
    }

    void FixedUpdate()
    {
        if (!gameOver)
        {
            AddMiles();   
        }
    }

    private void CreateButtons()
    {
        // Initialize buttons
        backToMenuButton = GameObject.Find("Back To Menu Button").GetComponent<Button>();
        restartButton = GameObject.Find("Restart Button").GetComponent<Button>();

        // Add listeners to buttons
        backToMenuButton.onClick.AddListener(BackToMenu);
        restartButton.onClick.AddListener(Restart);

        // Hide Back to Menu and Restart Button
        backToMenuButton.gameObject.SetActive(false);
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

    public void AddMiles()
    {
        miles += 0.001m;
        scoreCounterText.text = miles + " miles";

        if ((miles > 0.100m) && (miles > highScore))
        {
            UpdateHighScore(miles);
        }

        if (miles % .500m == 0m)
        {
            SpeedUp();
        }

        if (miles % .020m == 0m)
        {
            BurnGas();
        }
    }

    private void UpdateHighScore(decimal newHighScore)
    {
        highScore = newHighScore;
        highScoreText.text = "High Score: " + highScore + " mi.";

        if (firstTimeHighScore)
        {
            StartCoroutine(FlashText(newHighScoreText, numFlashes, flashesWaitTime));
            firstTimeHighScore = false;
        }
    }

    private void SpeedUp()
    {
        speed++;

        // increase spawn rate of objects
        if (speed < maxSpeed)
        {
            StartCoroutine(FlashText(speedUpText, numFlashes, flashesWaitTime));
            SoundManager.Instance.PlaySound("speedup");
            spawnManager.SpeedUp();
            textureScroll.SpeedUpScroll();

            // increase burn rate at around middle of max speed
            if (speed == 4)
            {
                burnRate++;
            }
        }
        else if (speed == maxSpeed)
        {
            StartCoroutine(FlashText(maxSpeedText, numFlashes + 2, flashesWaitTime));
            SoundManager.Instance.PlaySound("maxspeed");
            spawnManager.SpeedUp();
            textureScroll.SpeedUpScroll();

            // increase burn rate again at max speed
            burnRate++;
        }
    }

    public void FuelUp()
    {
        gas += 6;

        if (gas > 100)
        {
            gas = 100;
        }

        UpdateGasNumberText();
    }

    private void BurnGas()
    {
        gas -= burnRate;

        // protection against negative gas values
        if (gas < 0)
        {
            gas = 0;
        }

        UpdateGasNumberText();

        if (gas == 0)
        {
            GameOver();
        }
    }

    private void UpdateGasNumberText()
    {
        if (gas >= 75)
        {
            gasNumberText.color = Color.green;
        }
        else if (gas >= 25)
        {
            gasNumberText.color = Color.yellow;
        }
        else
        {
           gasNumberText.color = Color.red; 
        }

        gasNumberText.text = gas.ToString();
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
        gameOver = true;

        spawnManager.StopSpawning();

        // set gas to zero if game over by car explosion
        if (gas != 0)
        {
            gas = 0;
            UpdateGasNumberText();
        }

        gameOverText.SetActive(true);
        restartButton.gameObject.SetActive(true);
        backToMenuButton.gameObject.SetActive(true);
    }
}
