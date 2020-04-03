using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoopManager : MonoBehaviour
{
    public GameObject poop;
    public GameObject poopObject;
    public GameObject manager;
    
    public void generatePoop(){
       if(poop){
            Destroy(poop);
       }

        //Algorithm for generating poop goes here
        bool inLitter = Random.value >0.5f;

        Vector3 position;
        if (inLitter) position = new Vector3(-5.15f, 0f, 0f);
        else position = new Vector3(5.15f, 0f, 0f);

        poop = Instantiate(poopObject, position, Quaternion.identity) as GameObject;
    }
    
    public void cleanPoop(Button button){
        button.interactable = false;
        manager.GetComponent<GameManager>().triggerPunishPraisePanel(0);
    }
}
