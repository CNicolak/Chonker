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

    /**public void Start() {
        Debug.Log("POOP MANAGER START CALLED");
        for(int i = 0; i < MAX_POO; i++){
            poops[i] = null;
            _poopLocations[i] = 3;
        }
    }*/
    public void generatePoop(){
       //if(poop){
            //Destroy(poop);
       //}

        //Algorithm for generating poop goes here
        bool inLitter = Random.value >0.5f;

        Vector3 position;
        if (inLitter) {
            position = new Vector3(-5.15f, 0f, 0f);
            _poopLocation = 1;
            Debug.Log("Poop in litter");
        }
        else {
            position = new Vector3(5.15f, 0f, 0f);
            _poopLocation = 0;
            Debug.Log("Poop not in litter");
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
    
    public int poopLocation{
        get{ return _poopLocation; }
    }
    
    public void cleanPoop(Button button){
        int location = MAX_POO;
        bool found = false;
        bool anotherPoopInBox = false;
        if (button.CompareTag("litterBox")) {
            for(int i = MAX_POO - 1; i >= 0; i--){
                if (!found && _poopLocations[i] == 1) { Debug.Log("POOP IS IN LITTER: " + i); found = true; location = i; continue; }
                if (found && _poopLocations[i] == 1) { Debug.Log("ANOTHER POOP IS HERE"); anotherPoopInBox = true; break; } 
            }
        } else if (button.CompareTag("notLitterBox")){
            for (int i = MAX_POO - 1; i >= 0; i--) {
                if (!found && _poopLocations[i] == 0) { Debug.Log("POOP IS NOT IN LITTERBOX: " + i); found = true; location = i; continue; }
                if (found && _poopLocations[i] == 0) { Debug.Log("ANOTHER POOP IS HERE"); anotherPoopInBox = true; break; }
            }
        }
        if (found) {
            _poopLocations[location] = 3;
            Destroy(poops[location]);
            if (!anotherPoopInBox) button.interactable = false;
        }else{
            button.interactable = false;
        }

        manager.GetComponent<GameManager>().triggerPunishPraisePanel(0);
    }
}
