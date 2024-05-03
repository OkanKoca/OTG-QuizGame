using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizPauseController : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused = false;
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        isPaused = true;
        
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isPaused = false;
        
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        
    }
    public void QuitGame()
    {
        // we can ask users if they are sure 
        Application.Quit();
    }
}
