﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //public GameObject healthText;
    public GameObject happinessText;
    public GameObject hungerText;

    //public GameObject ageText;

    public GameObject currencyText;

    public GameObject namePanel;   
    public GameObject nameInput;        
    public GameObject nameText;    

    public GameObject pet;
    public GameObject petPanel;
    public GameObject[] petList;


    public GameObject foodPanel;
     public GameObject shopPanel;   
    public Sprite[] foodIcons;  
    // For food and toys, put a text that shows inventory number on hand.

    public GameObject poopManager;
    public GameObject punishPraisePanel;

    void Start() {
        if(!PlayerPrefs.HasKey("looks"))
            PlayerPrefs.SetInt("looks", 0);

        createPet(PlayerPrefs.GetInt("looks")); //create pet
        //PlayerPrefs.SetInt("currency", 450);

        if(!PlayerPrefs.HasKey("currency"))
            PlayerPrefs.SetInt("currency", 500);
        

    }

    void Update() {

        // currently cycles all the time.
        //healthText.GetComponent<Text>().text = "" + pet.GetComponent<Pet> ().health;
        happinessText.GetComponent<Text>().text = "" + pet.GetComponent<Pet> ().happiness;
        hungerText.GetComponent<Text>().text = "" + pet.GetComponent<Pet> ().hunger;
        //ageText.GetComponent<Text>().text = "" + pet.GetComponent<Pet> ().age;
        nameText.GetComponent<Text>().text = "" + pet.GetComponent<Pet> ().name;
        currencyText.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("currency");
        //if (Input.GetKeyUp(KeyCode.Space)) // spacebar changes pet
        //    createPet(1);
    }

    // if triggered set to opposite to current state
    public void triggerNamePanel(bool b){
        namePanel.SetActive(!namePanel.activeInHierarchy); 

        if (b){
            pet.GetComponent<Pet>().name = nameInput.GetComponent<InputField>().text;
            PlayerPrefs.SetString("name", pet.GetComponent<Pet>().name);
        }
    }

    // Bottom Menu Bar
    public void buttonBehavior(int i) {
        switch (i) {
        case(0):
        default:    // SKINS BUTTON
            petPanel.SetActive(!petPanel.activeInHierarchy);
            break;
        case(1):    // SHOP BUTTON
            SceneManager.LoadScene("ShopMenu");
            //shopPanel.SetActive(!shopPanel.activeInHierarchy);
            break;
        case(2):    // FEED BUTTON
            foodPanel.SetActive(!foodPanel.activeInHierarchy);
            break;
        case(3):    // PLAY BUTTON
            //todo trigger mini-games
            break;
        case(4):    // QUIT BUTTON
            pet.GetComponent<Pet>().savePet();
            Application.Quit();
            break;
        }
    }

    public void createPet(int i){
        if(pet)
            Destroy(pet);
        pet = Instantiate(petList[i], Vector3.zero, Quaternion.identity) as GameObject;
        // pet = Instantiate(petList[i], new Vector3(0f,-3f,0f), Quaternion.identity) as GameObject;
        // Creates a new pet and sets to new GameObject variable.

        toggle(petPanel);
        PlayerPrefs.SetInt("looks", i);

    }

    public void selectFood(int i) {
        toggle(foodPanel);
    }

    //public void openShop(int i) {
    //    SceneManager.LoadScene("ShopMenu");
    //}

    public void triggerPunishPraisePanel(int i) {
        punishPraisePanel.SetActive(!punishPraisePanel.activeInHierarchy);

        if (i > 0) {
            if (i == 1) {
                //pet.GetComponent<Pet>().praise();
                praise();
                poopManager.GetComponent<PoopManager>().generatePoop();
            }else if (i == 2){
                //pet.GetComponent<Pet>().punish();
                punish();
                poopManager.GetComponent<PoopManager>().generatePoop();
            }
        }
    }


    public void punish(){
    }

    public void praise(){
    }



    public void toggle(GameObject g) {
        if (g.activeInHierarchy)
            g.SetActive(false);
    }
}
