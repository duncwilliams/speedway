using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    private Button startButton;
    private Button exitButton;
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
        exitButton = GameObject.Find("Exit Button").GetComponent<Button>();

        // Add listeners to buttons
        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void StartGame()
    {
        levelLoader.LoadLevel("Main");
    }

    private void ExitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
	    Application.Quit();
        #endif
    }
}
