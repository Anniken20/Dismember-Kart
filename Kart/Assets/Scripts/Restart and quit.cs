using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restartandquit : MonoBehaviour
{
    public void GoToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }
    

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }
}
