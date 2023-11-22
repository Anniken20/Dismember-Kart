using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public PauseMenu pauseMenu; // Reference to the PauseMenu script

    void Start()
    {
        if (pauseMenu == null)
        {
            Debug.LogError("PauseMenu script");
            return;
        }

        // Initially hide the pause menu
        pauseMenu.gameObject.SetActive(false);
    }

    void Update()
    {
        // Check for pause input
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Toggle pause through the PauseMenu script
            pauseMenu.TogglePause();
        }
    }
}