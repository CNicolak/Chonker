using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoopManager : MonoBehaviour
{
    public GameObject poop;
    public GameObject poopObject;
    public GameObject manager;
    
    [SerializeField]    
    private bool _poopLocation;     // 0 or 1


    public void generatePoop(){
       //if(poop){
       //     Destroy(poop);
       //}

        //Algorithm for generating poop goes here
        bool inLitter = Random.value >0.5f;

        Vector3 position;
        if (inLitter) {
            position = new Vector3(-5.15f, 0f, 0f);
            _poopLocation = true;
            Debug.Log("Poop in litter");
        }
        else {
            position = new Vector3(5.15f, 0f, 0f);
            _poopLocation = false;
            Debug.Log("Poop not in litter");
        }

        poop = Instantiate(poopObject, position, Quaternion.identity) as GameObject;
    }
    
    public bool poopLocation{
        get{ return _poopLocation; }
    }
    
    public void cleanPoop(Button button){
        button.interactable = false;
        Destroy(poop);
        manager.GetComponent<GameManager>().triggerPunishPraisePanel(0);
    }
}
