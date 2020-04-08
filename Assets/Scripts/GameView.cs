using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameView : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject controlsMenuUI;
    public GameObject rulesMenuUI;
    public GameObject gameOverMenuUI;
    public GameObject winMenuUI;
    public GameObject gameComponents;

    void Start()
    {
        gameComponents.SetActive(true);
        pauseMenuUI.SetActive(false);
        controlsMenuUI.SetActive(false);
        rulesMenuUI.SetActive(false);
        gameOverMenuUI.SetActive(false);
        winMenuUI.SetActive(false);
       
    }

    public void displayGame()
    {
        pauseMenuUI.SetActive(false);
        controlsMenuUI.SetActive(false);
        rulesMenuUI.SetActive(false);
        gameOverMenuUI.SetActive(false);
        winMenuUI.SetActive(false);
        gameComponents.SetActive(true);
    }

    public void displayPause()
    {
        gameComponents.SetActive(true);
        pauseMenuUI.SetActive(true);
    }

    public void displayControls()
    {
        gameComponents.SetActive(false);
        pauseMenuUI.SetActive(false);
        controlsMenuUI.SetActive(true);
    }

    public void displayRules()
    {
        gameComponents.SetActive(false);
        pauseMenuUI.SetActive(false);
        rulesMenuUI.SetActive(true);
    }

    public void closeControls()
    {
        controlsMenuUI.SetActive(false);
        gameComponents.SetActive(true);
        pauseMenuUI.SetActive(true);
    }

    public void closeRules()
    {
        rulesMenuUI.SetActive(false);
        gameComponents.SetActive(true);
        pauseMenuUI.SetActive(true);
    }

    public void displayGameOver()
    {
        gameComponents.SetActive(false);
        gameOverMenuUI.SetActive(true);
    }

    public void displayWin()
    {
        gameComponents.SetActive(false);
        winMenuUI.SetActive(true);
    }
}
