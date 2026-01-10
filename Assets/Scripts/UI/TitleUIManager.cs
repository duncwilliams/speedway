using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    private Button startButton;
    private Button creditsButton;
    private Button exitButton;
    private Button closeButton;

    public  GameObject creditsPanel;

    public LevelLoader levelLoader;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateButtons();
    }

    private void CreateButtons()
    {
        // Initialize buttons
        startButton = GameObject.Find("Start Button").GetComponent<Button>();
        creditsButton = GameObject.Find("Credits Button").GetComponent<Button>();
        exitButton = GameObject.Find("Exit Button").GetComponent<Button>();
        closeButton = GameObject.Find("Close Button").GetComponent<Button>();

        // Add listeners to buttons
        startButton.onClick.AddListener(StartGame);
        creditsButton.onClick.AddListener(Credits);
        exitButton.onClick.AddListener(ExitGame);
        closeButton.onClick.AddListener(ClosePanel);

        // Hide credits panel
        creditsPanel.SetActive(false);
    }

    private void StartGame()
    {
        levelLoader.LoadLevel("Main");
    }

    private void Credits()
    {
        creditsPanel.SetActive(true);
    }

    private void ExitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
	    Application.Quit();
        #endif
    }

    private void ClosePanel()
    {
        creditsPanel.SetActive(false);
    }
}
