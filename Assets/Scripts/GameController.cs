using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool gameIsPaused = false;
    private int currencyEarned = 0;

  
	public Sprite[] skinList;
    public GameObject currentSprite;
 

    // Update is called once per frame

    void Start () 
    {
        resetCurrency();
 
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
        FindObjectOfType<GameView>().displayGame();
        Time.timeScale = 1f;
    	gameIsPaused = false;

    }

    void pause()
    {
        FindObjectOfType<GameView>().displayPause();
        Time.timeScale = 0f;
    	gameIsPaused = true;
    }

    public void loadControls()
    {
        FindObjectOfType<GameView>().displayControls();

    }

    public void controlsBack()
    {
        FindObjectOfType<GameView>().closeControls();
    }

    public void loadRules()
    {
        FindObjectOfType<GameView>().displayRules();
    }

    public void rulesBack()
    {
        FindObjectOfType<GameView>().closeRules();
    }

    public void gameOver()
    {
        FindObjectOfType<GameView>().displayGameOver();
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
        FindObjectOfType<GameView>().displayWin();
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
