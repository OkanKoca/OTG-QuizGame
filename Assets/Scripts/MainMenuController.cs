using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    public CanvasGroup optionPanel;
    public CanvasGroup howToPlayPanel;
    public CanvasGroup creditsPanel;

    public void PlayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        // OptionPanel'i aç
        optionPanel.alpha = 1;
        optionPanel.blocksRaycasts = true;

        // Diğer panelleri kapat
        howToPlayPanel.alpha = 0;
        howToPlayPanel.blocksRaycasts = false;
        creditsPanel.alpha = 0;
        creditsPanel.blocksRaycasts = false;
    }

    public void OpenHowToPlay()
    {
        // HowToPlayPanel'i aç
        howToPlayPanel.alpha = 1;
        howToPlayPanel.blocksRaycasts = true;

        // Diğer panelleri kapat
        optionPanel.alpha = 0;
        optionPanel.blocksRaycasts = false;
        creditsPanel.alpha = 0;
        creditsPanel.blocksRaycasts = false;
    }

    public void OpenCredits()
    {
        // CreditsPanel'i aç
        creditsPanel.alpha = 1;
        creditsPanel.blocksRaycasts = true;

        // Diğer panelleri kapat
        optionPanel.alpha = 0;
        optionPanel.blocksRaycasts = false;
        howToPlayPanel.alpha = 0;
        howToPlayPanel.blocksRaycasts = false;
    }

    public void Back()
    {
        // Tüm panelleri kapat
        optionPanel.alpha = 0;
        optionPanel.blocksRaycasts = false;
        howToPlayPanel.alpha = 0;
        howToPlayPanel.blocksRaycasts = false;
        creditsPanel.alpha = 0;
        creditsPanel.blocksRaycasts = false;
        Time.timeScale = 1;
    }
}