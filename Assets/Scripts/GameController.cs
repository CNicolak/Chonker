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
	public Sprite[] skinList;
    public GameObject currentSprite;
 

    // Update is called once per frame

    void Start () 
    {
        resetCurrency();
        gameComponents.SetActive(true);
        pauseMenuUI.SetActive(false);
        controlsMenuUI.SetActive(false);
        rulesMenuUI.SetActive(false);
        gameOverMenuUI.SetActive(false);
        winMenuUI.SetActive(false);
        skins(PlayerPrefs.GetInt("looks"));
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


    void skins(int i){
        currentSprite.GetComponent<SpriteRenderer>().sprite = skinList[i];
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
        SceneManager.LoadScene("Minigame2");
    }

    public void quit()
    {
        updateResource();
        resetCurrency();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void win()
    {
        gameComponents.SetActive(false);
        winMenuUI.SetActive(true);
        updateResource();
        Invoke("loadGame", 2f);
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

    private void loadGame()
    {
        SceneManager.LoadScene("Game");
    }

}
