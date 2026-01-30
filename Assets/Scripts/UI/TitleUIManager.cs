using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    private Button startButton;
    private Button controlsButton;
    private Button creditsButton;
    private Button exitButton;
    private Button controlsCloseButton;
    private Button creditsCloseButton;

    public GameObject creditsPanel;
    public GameObject controlsPanel;

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
        controlsButton = GameObject.Find("Controls Button").GetComponent<Button>();
        creditsButton = GameObject.Find("Credits Button").GetComponent<Button>();
        exitButton = GameObject.Find("Exit Button").GetComponent<Button>();
        controlsCloseButton = GameObject.Find("Controls Close Button").GetComponent<Button>();
        creditsCloseButton = GameObject.Find("Credits Close Button").GetComponent<Button>();

        // Add listeners to buttons
        startButton.onClick.AddListener(StartGame);
        controlsButton.onClick.AddListener(Controls);
        creditsButton.onClick.AddListener(Credits);
        exitButton.onClick.AddListener(ExitGame);
        controlsCloseButton.onClick.AddListener(CloseControlsPanel);
        creditsCloseButton.onClick.AddListener(CloseCreditsPanel);

        // Hide credits panel
        controlsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    private void StartGame()
    {
        levelLoader.LoadLevel("Main");
    }

    private void Controls()
    {
        controlsPanel.SetActive(true);
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

    private void CloseControlsPanel()
    {
        controlsPanel.SetActive(false);
    }

    private void CloseCreditsPanel()
    {
        creditsPanel.SetActive(false);
    }
}
