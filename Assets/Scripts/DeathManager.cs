using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class DeathManager : MonoBehaviour {

    public GameObject flashText;
    public GameObject nameText;
    public GameObject skinImgG;
    public GameObject skinImgW;    


    // Start is called before the first frame update
    // Use this for initialization
    void Start () {
        //Repeatedly calls a method at the start of the game, every 0.5 seconds.
        InvokeRepeating("flashTheText", 0f, 0.5f);

        if (PlayerPrefs.GetInt("looks") == 0 || PlayerPrefs.GetInt("looks") == 1) {
            skinImgW.SetActive(true);
            skinImgG.SetActive(false);            

        } else if (PlayerPrefs.GetInt("looks") == 2 || PlayerPrefs.GetInt("looks") == 3) {
            skinImgW.SetActive(false);
            skinImgG.SetActive(true);   
        }
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonUp(0)) {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Start");
        }
    }

    void flashTheText() {
        if (flashText.activeInHierarchy)
            flashText.SetActive(false);
        else 
            flashText.SetActive(true);
    }
}
