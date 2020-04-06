using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    
    public static bool doNot=false;
    [SerializeField]
    private int state;
    [SerializeField]
    private int cardValue;
    [SerializeField]
    private bool initalized=false;
    
    private Sprite cardBack;
    private Sprite cardFront;
    
    private GameObject manager;
    
    // Start is called before the first frame update
    void Start()
    {
        state=1;
        manager=GameObject.FindGameObjectWithTag("Manager");
    }
    
    public void setUpGraphics(){
        cardBack=manager.GetComponent<MiniGameManager1>().getCardBack();
        cardFront=manager.GetComponent<MiniGameManager1>().getCardFront(cardValue);
        
        flipCard();
        
    }
    
    public void flipCard(){
        if(state==0){
            state=1;
        }
        else if(state==1){
            state=0;
        }
        
        if(state==0 && !doNot){
            GetComponent<Image>().sprite=cardBack;
            
        }
        else if(state==1 && !doNot){
            GetComponent<Image>().sprite=cardFront;
        }
    }
    
    public int getCardValue(){
        return cardValue;
    }
    
    public void setCardValue(int val){
        cardValue=val;
    }
    
    public int getState(){
        return state;
    }
    
    public void setState(int val){
        state=val;
    }
    
    public bool getInitialized(){
        return initalized;
    }
    
    public void setInitialized(bool val){
        initalized=val;
    }
    
    public void falseCheck(){
        StartCoroutine(pause());
    }
    
    IEnumerator pause(){
        yield return new WaitForSeconds(1);
        if(state==0){
            GetComponent<Image>().sprite=cardBack;
        }
        else if(state==1){
            GetComponent<Image>().sprite=cardFront;
        }
        doNot=false;
    }
    
}
