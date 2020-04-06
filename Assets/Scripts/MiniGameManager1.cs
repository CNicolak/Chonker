using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MiniGameManager1 : MonoBehaviour
{
    
    public Sprite [] cardFaces;
    public Sprite cardBack;
    public GameObject [] cards;
    public Text matchText;
    public int money=0;
    
    private bool init=false;
    private int matches=13;
    

    // Update is called once per frame
    void Update()
    {
        if(!init){
            initializeCards();
        }
        if(Input.GetMouseButtonUp(0)){
            checkCards();
        }
    }
    
    void initializeCards(){
        for(int i=0;i<2;i++){
            for(int j=1;j<14;j++){
                bool test=false;
                int choice =0; 
                while(!test){
                    choice=Random.Range(0,cards.Length);
                    test=!(cards[choice].GetComponent<Card>().getInitialized());
                }
                cards[choice].GetComponent<Card>().setCardValue(j);
                cards[choice].GetComponent<Card>().setInitialized(true);
            }
        }
        
        foreach(GameObject c in cards){
            c.GetComponent<Card>().setUpGraphics();
        }
        
        if(!init){
            init=true;
        }
    }
    
    public Sprite getCardBack(){
        return cardBack;
    }
    
    public Sprite getCardFront(int i){
        return cardFaces[i-1];
    }
    
    void checkCards(){
        List<int>c=new List<int>();
        for(int i=0; i<cards.Length; i++){
            if(cards[i].GetComponent<Card>().getState()==1){
                c.Add(i);
            }
        }
        
        if(c.Count==2){
            CardComparison(c);
        }
    }
    
    void CardComparison(List<int> c){
        Card.doNot=true;
        
        int x=0;
        
        if(cards[c[0]].GetComponent<Card>().getCardValue()==cards[c[1]].GetComponent<Card>().getCardValue()){
            x=2;
            matches--;
            money+=5;
            matchText.text="Number of Matches: "+ matches;
            if(matches==0){
                //call funtion here to make currency increase;
                //if(Player.Prefs.HasKey("currency",0)){
                    //int temp=PlayerPrefs.GetInt("currency");
                    //temp+=money;
                    //PlayerPrefs.SetInt("currency", temp);
                    money=0;
                //}
                
                //make money =0;
                SceneManager.LoadScene("MainScene");
            }
        }
        
        for(int i=0;i<c.Count;i++){
            cards[c[i]].GetComponent<Card>().setState(x);
            cards[c[i]].GetComponent<Card>().falseCheck();
            
        }
    }
    
    
}
