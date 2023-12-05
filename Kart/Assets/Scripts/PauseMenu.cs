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
        if (resumeButton != null)
            resumeButton.onClick.AddListener(Resume);
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        if (quitButton != null)
            quitButton.onClick.AddListener(Quit);

        // Initially hide the pause menu
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
        else
            Debug.LogError("PauseMenuUI");
    }

    public void TogglePause()
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
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);

        // Indicate the game is paused
        isPaused = true;
    }

    void Resume()
    {
        // Unpause the game
        Time.timeScale = 1f;

        // Hide the pause menu
        if (pauseMenuUI != null)
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