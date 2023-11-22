using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button resumeButton;
    public Button mainMenuButton;
    public Button quitButton;

    private bool isPaused = false;

    void Start()
    {
        // Get references to UI elements
        resumeButton.onClick.AddListener(Resume);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        quitButton.onClick.AddListener(Quit);

        // Initially hide the pause menu
        pauseMenuUI.SetActive(false);
    }

void Update()
    {
        // Check for pause input
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    void Pause()
    {
        // Pause the game
        Time.timeScale = 0f;

        // Show the pause menu
        pauseMenuUI.SetActive(true);

        // Indicate the game is paused
        isPaused = true;
    }

    void Resume()
    {
        // Unpause the game
        Time.timeScale = 1f;

        // Hide the pause menu
        pauseMenuUI.SetActive(false);

        // Indicate the game is not paused
        isPaused = false;
    }

    void ReturnToMainMenu()
    {
        // Load the main menu
        SceneManager.LoadScene("MainMenu");
    }

    void Quit()
    {
        Debug.Log("Quitting the game");
        Application.Quit();
    }
}
