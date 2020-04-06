using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoopManager : MonoBehaviour
{
    public GameObject[] poops = new GameObject[5];
    public GameObject poopObject;
    public GameObject manager;

    private int _poopLocation;    // 0 or 1
    private int[] _poopLocations = new int[5];
    private const int MAX_POO = 5;

    public void Start() {
        for(int i = 0; i < MAX_POO; i++){
            poops[i] = null;
            _poopLocations[i] = 3;
        }
    }
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
        if (button.CompareTag("litterBox")) {
            for(location = MAX_POO - 1; location >= 0; location--){
                if (_poopLocations[location] == 1) break;
            }
            Destroy(poops[location]);
        } else if (button.CompareTag("notLitterBox")){
            for (location = MAX_POO -1; location >= 0; location--) {
                if (_poopLocations[location] == 0) break;
            }
            Destroy(poops[location]);
        }
        _poopLocations[location] = 3;

        manager.GetComponent<GameManager>().triggerPunishPraisePanel(0);
    }
}
