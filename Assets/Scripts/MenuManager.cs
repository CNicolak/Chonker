using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public GameObject flashText;
    public GameObject eggSkin;


    // Start is called before the first frame update
    // Use this for initialization
    void Start () {
        //Repeatedly calls a method at the start of the game, every 0.5 seconds.
        InvokeRepeating("flashTheText", 0f, 0.5f);
        egg();  
        //(nameOfMethod, TimeToStartCalling, TimeBeforeCallingMethodAgain)
    }

    // Update is called once per frame
    void Update() {
        //if (Input.GetMouseButtonUp(0))
            //SceneManager.LoadScene("Game");
            //PlayerPrefs.SetInt("age", 1);           
    }

    void flashTheText() {
        if (flashText.activeInHierarchy)
            flashText.SetActive(false);
        else 
            flashText.SetActive(true);
    }

    public void quit(){
        Application.Quit();
    }

    public void start(){
        SceneManager.LoadScene("Game");
        PlayerPrefs.SetInt("age", 1);  
    }

    private void newGame(){
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Start");
    }

    void egg(){

        if (PlayerPrefs.GetInt("age") == 0 ) {
            eggSkin.SetActive(true);        
        } else  {
            eggSkin.SetActive(false);
        }             
    }


}
