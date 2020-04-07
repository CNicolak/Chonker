using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitMiniGame1 : MonoBehaviour
{   
    public void triggerQuit(int i){
        //GameObject manager=GameObject.FindGameObjectWithTag("Manager");
        
        if(i==0){
            //manager.GetComponent<MiniGameManager1>().setMoney(0);
            SceneManager.LoadScene("Game");
        }
    }
}
