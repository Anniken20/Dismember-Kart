using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("Map111");
    }
    
    public void Play2Button()
    {
        SceneManager.LoadScene("Map222");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
