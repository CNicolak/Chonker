using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoopManager : MonoBehaviour
{
    public GameObject[] poops = new GameObject[10];
    public GameObject poopObject;
    public GameObject manager;

    private int _poopLocation;    // 0 or 1
    private int[] _poopLocations = new int[] {3,3,3,3,3,3,3,3,3,3};
    private const int MAX_POO = 10;

    public void generatePoop(int discipline){
        int numPoops = 0;
        bool inLitter;
        //Algorithm for generating poop goes here
        if (discipline > 50) {
            inLitter = Random.value > 0.25f;
        } else {
            inLitter = Random.value > 0.5f; 
        }
        
        Vector3 position;
        if (inLitter) {
            position = new Vector3(-5.15f, 0f, 0f);
            _poopLocation = 1;
            Debug.Log("Poop in litter");
            numPoops = PlayerPrefs.GetInt("poopLitter");
            numPoops++;
            PlayerPrefs.SetInt("poopLitter", numPoops);
        }
        else {
            position = new Vector3(5.15f, 0f, 0f);
            _poopLocation = 0;
            Debug.Log("Poop not in litter");
            numPoops = PlayerPrefs.GetInt("poopNotLitter");
            numPoops++;
            PlayerPrefs.SetInt("poopNotLitter", numPoops);
        }

        int i;
        for(i = 0; i < MAX_POO; i++){
            if (poops[i] == null) break;
        }
        if (i == 5){ //We have max poo
            //trigger clean
        }
        _poopLocations[i] = _poopLocation;
        poops[i] = Instantiate(poopObject, position, Quaternion.identity) as GameObject;
    }

    public void generateLitter(){
        int numPoops = PlayerPrefs.GetInt("poopLitter");
        Vector3 position = new Vector3(-5.15f, 0f, 0f);
        _poopLocation = 1;

        int i;
        for (i = 0; i < numPoops; i++) {
            if (poops[i] == null) {
                poops[i] = Instantiate(poopObject, position, Quaternion.identity) as GameObject;
                _poopLocations[i] = _poopLocation;
            }
        }
    }

    public void generateNotLitter() {
        int numPoops = PlayerPrefs.GetInt("poopNotLitter");
        int numPoopsLitter = PlayerPrefs.GetInt("poopLitter");
        Vector3 position = new Vector3(5.15f, 0f, 0f);
        _poopLocation = 0;

        int i;
        for (i = numPoopsLitter; i < numPoopsLitter + numPoops; i++) {
            if (poops[i] == null) {
                poops[i] = Instantiate(poopObject, position, Quaternion.identity) as GameObject;
                _poopLocations[i] = _poopLocation;
            }
        }
    }

    public int poopLocation{
        get{ return _poopLocation; }
    }
    
    public void cleanPoop(Button button){
        int location = MAX_POO;
        bool found = false;
        bool anotherPoopInBox = false;

        // Cleaning increases happy.
        manager.GetComponent<GameManager>().clean();

        if (button.CompareTag("litterBox")) {
            for(int i = MAX_POO - 1; i >= 0; i--){
                if (!found && _poopLocations[i] == 1) { 
                    found = true; 
                    location = i;
                    int numPoops = PlayerPrefs.GetInt("poopLitter");
                    PlayerPrefs.SetInt("poopLitter", --numPoops);
                    continue; 
                }
                if (found && _poopLocations[i] == 1) {  anotherPoopInBox = true; break; } 
            }
        } else if (button.CompareTag("notLitterBox")){
            for (int i = MAX_POO - 1; i >= 0; i--) {
                if (!found && _poopLocations[i] == 0) { 
                    found = true; 
                    location = i;
                    int numPoops = PlayerPrefs.GetInt("poopNotLitter");
                    PlayerPrefs.SetInt("poopNotLitter", --numPoops);
                    continue; 
                }
                if (found && _poopLocations[i] == 0) { 
                    anotherPoopInBox = true; 
                    break; 
                }
            }
        }
        if (found) {
            _poopLocations[location] = 3;
            Destroy(poops[location]);
            if (!anotherPoopInBox) button.interactable = false;
            if (button.CompareTag("litterBox")) manager.GetComponent<GameManager>().triggerPunishPraisePanelLitter(0);
            else if (button.CompareTag("notLitterBox")) manager.GetComponent<GameManager>().triggerPunishPraisePanelNotLitter(0);
        } else{
            button.interactable = false;
        }
    }
}