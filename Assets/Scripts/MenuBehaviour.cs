using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    
    public void triggerMenuBehav(int i){
        if(i==0){
            Application.Quit();
            //pet.GetComponent<Pet>().savePet();
            //SceneManager.LoadScene("MiniGame1");
        }
        else if(i==1){
            SceneManager.LoadScene("MiniGame1");
        }
        else{
            SceneManager.LoadScene("Game");
        }
    }
    
}
