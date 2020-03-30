using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
//using System.Collections.Generic;


public class MenuManager : MonoBehaviour {

    public GameObject flashText;


    // Start is called before the first frame update
    // Use this for initialization
    void Start () {
        //Repeatedly calls a method at the start of the game, every 0.5 seconds.
        InvokeRepeating("flashTheText", 0f, 0.5f);
        //(nameOfMethod, TimeToStartCalling, TimeBeforeCallingMethodAgain)
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonUp(0))
            SceneManager.LoadScene("Game");
    }

    void flashTheText() {
        if (flashText.activeInHierarchy)
            flashText.SetActive(false);
        else 
            flashText.SetActive(true);
    }
}
