using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool gameIsPaused = false;
    private int currencyEarned = 0;

    public GameObject pauseMenuUI;
    public GameObject controlsMenuUI;
    public GameObject rulesMenuUI;
    public GameObject gameOverMenuUI;
    public GameObject winMenuUI;
    public GameObject gameComponents;


    // Update is called once per frame

    void Start () 
    {
        gameComponents.SetActive(true);
        pauseMenuUI.SetActive(false);
        controlsMenuUI.SetActive(false);
        rulesMenuUI.SetActive(false);
        gameOverMenuUI.SetActive(false);
        winMenuUI.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        	if (gameIsPaused)
        	{
        		resume();
        	}
        	else
        	{
        		pause();
        	}
        }
    }

    void resume()
    {
    	pauseMenuUI.SetActive(false);
        controlsMenuUI.SetActive(false);
        rulesMenuUI.SetActive(false);
        gameOverMenuUI.SetActive(false);
        winMenuUI.SetActive(false);
        gameComponents.SetActive(true);
    	Time.timeScale = 1f;
    	gameIsPaused = false;

    }

    void pause()
    {
        gameComponents.SetActive(true);
    	pauseMenuUI.SetActive(true);
    	Time.timeScale = 0f;
    	gameIsPaused = true;
    }

    public void loadControls()
    {
        gameComponents.SetActive(false);
        pauseMenuUI.SetActive(false);
        controlsMenuUI.SetActive(true);

    }

    public void controlsBack()
    {
        controlsMenuUI.SetActive(false);
        gameComponents.SetActive(true);
        pauseMenuUI.SetActive(true);
    }

    public void loadRules()
    {
        gameComponents.SetActive(false);
        pauseMenuUI.SetActive(false);
        rulesMenuUI.SetActive(true);
    }

    public void rulesBack()
    {
        rulesMenuUI.SetActive(false);
        gameComponents.SetActive(true);
        pauseMenuUI.SetActive(true);
    }

    public void gameOver()
    {
   
        gameComponents.SetActive(false);
        gameOverMenuUI.SetActive(true);
    }

    public void retry()
    {
        resetCurrency();
        SceneManager.LoadScene("Minigame2");
    }

    public void quit()
    {
        updateResource();
        resetCurrency();
        SceneManager.LoadScene("Game");
    }

    public void win()
    {
        gameComponents.SetActive(false);
        winMenuUI.SetActive(true);
        Invoke("updateResource()", 2f);
        resetCurrency();
        SceneManager.LoadScene("Game");
    }

    public void addCurrency()
    {
        currencyEarned = currencyEarned + 10;
        Debug.Log(currencyEarned);
    }

    public void resetCurrency()
    {
        currencyEarned = 0;
    }

    public int getCurrency()
    {
        return currencyEarned;
    }

    public void updateResource()
    {
        if (PlayerPrefs.HasKey("currency"))
        {
            int temp = PlayerPrefs.GetInt("currency");
            temp += currencyEarned;
            Debug.Log(temp);
            PlayerPrefs.SetInt("currency", temp);
       }
    }

}
