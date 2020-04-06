using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject healthText;
    public GameObject happinessText;
    public GameObject hungerText;
    public GameObject disciplineText;
    public GameObject wasteText;   // Will be hidden from user.
    public GameObject ageText;
    public GameObject currencyText;

    public GameObject namePanel;   
    public GameObject nameInput;        
    public GameObject nameText;    

    public GameObject pet;
    public GameObject petPanel;
    public GameObject[] petList;

    public GameObject foodPanel;
    public GameObject foodErrText; 
    public GameObject treatQtyText;  
    public Sprite[] foodIcons;  

    public GameObject shopPanel;   
    public GameObject playPanel;
    public GameObject toyQtyText; 
    public Button toyButton;            

    public GameObject poopManager;
    public GameObject punishPraisePanelLitter;
    public GameObject punishPraisePanelNotLitter;


    void Start() {
        if(!PlayerPrefs.HasKey("looks"))
            PlayerPrefs.SetInt("looks", 0);

        createPet(PlayerPrefs.GetInt("looks")); //create pet

        if(!PlayerPrefs.HasKey("currency"))
            PlayerPrefs.SetInt("currency", 500); 

        if(!PlayerPrefs.HasKey("age"))
            PlayerPrefs.SetInt("age", 0); 


        // TEMP FOR TESTING SKIN PANEL
        PlayerPrefs.SetInt("BlackCat", 0); 
        PlayerPrefs.SetInt("Hat", 0);  
        PlayerPrefs.SetInt("Ball", 0);
        PlayerPrefs.SetInt("Fish", 0);    
    }


    void Update() {     // currently cycles all the time.
        healthText.GetComponent<Text>().text = "" + pet.GetComponent<Pet> ().health;
        happinessText.GetComponent<Text>().text = "" + pet.GetComponent<Pet> ().happiness;
        hungerText.GetComponent<Text>().text = "" + pet.GetComponent<Pet> ().hunger;
        disciplineText.GetComponent<Text>().text = "" + pet.GetComponent<Pet> ().discipline;
        wasteText.GetComponent<Text>().text = "" + pet.GetComponent<Pet> ().waste;                 
        ageText.GetComponent<Text>().text = "" + pet.GetComponent<Pet> ().age;
        nameText.GetComponent<Text>().text = "" + pet.GetComponent<Pet> ().name;
        currencyText.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("currency");
        //if (Input.GetKeyUp(KeyCode.Space)) // spacebar command
    }


    public void triggerNamePanel(bool b){
        //namePanel.SetActive(!namePanel.activeInHierarchy); 
        if (b){
            pet.GetComponent<Pet>().name = nameInput.GetComponent<InputField>().text;
            PlayerPrefs.SetString("name", pet.GetComponent<Pet>().name);
        }
        toggle(namePanel);
    }


    // Bottom Menu Bar
    public void buttonBehavior(int i) {
        switch (i) {
        case(0):
        default:    // SKINS BUTTON
            petPanel.SetActive(!petPanel.activeInHierarchy);
            break;
        case(1):    // SHOP BUTTON
            shopPanel.SetActive(!shopPanel.activeInHierarchy);
            break;
        case(2):    // FEED BUTTON
            foodPanel.SetActive(!foodPanel.activeInHierarchy);
            break;
        case(3):    // PLAY BUTTON
            playPanel.SetActive(!playPanel.activeInHierarchy);

            if (PlayerPrefs.GetInt("Ball") == 1){
                toyQtyText.SetActive(false);
                toyButton.interactable = true;
            }
            break;

        case(4):    // QUIT BUTTON
            save();
            Application.Quit();
            break;
        }
    }


    public void createPet(int i){
        if(pet)
            Destroy(pet);
        
        // Creates a new pet and sets to new GameObject variable.
        pet = Instantiate(petList[i], Vector3.zero, Quaternion.identity) as GameObject;

        toggle(petPanel);
        PlayerPrefs.SetInt("looks", i);
    }


    public void switchScene(int i) {
        switch (i) {
        case(0):
        default:    // SHOP SCENE
            save();
            SceneManager.LoadScene("ShopMenu");           
            break;
        case(1):    // MINI-GAME 1
            play(10);
            //SceneManager.LoadScene("ShopMenu");                         
            Debug.Log("Switch to mini-game 1");   
            break;
        case(2):    // MINI-GAME 2
            play(10);
            //SceneManager.LoadScene("ShopMenu");
            Debug.Log("Switch to mini-game 2"); 
            break;
        }
    }


    public void selectFood(int i) {
        switch (i) {
        case(0):
        default:    // FOOD
            pet.GetComponent<Pet>().updateHunger(-5);
            pet.GetComponent<Pet>().updateHappiness(5);
            pet.GetComponent<Pet>().updateWaste(1);
            poopManager.GetComponent<PoopManager>().generatePoop();
            toggle(foodPanel);            
            break;
        case(1):    // TREAT
            pet.GetComponent<Pet>().updateHunger(-50);
             pet.GetComponent<Pet>().updateHappiness(50);           
            pet.GetComponent<Pet>().updateWaste(1);
            poopManager.GetComponent<PoopManager>().generatePoop();
            toggle(foodPanel);
            break;
        }
    }


    public void triggerPunishPraisePanelLitter(int i) {
        punishPraisePanelLitter.SetActive(!punishPraisePanelLitter.activeInHierarchy);
        pet.GetComponent<Pet>().updateWaste(-1);
        if (i > 0) {
                if (i == 1) {
                    //Debug.Log("Praise true in litter");
                    //praise(true);
                    pet.GetComponent<Pet>().updateDiscipline(5);
                } else if (i == 2) {
                    //Debug.Log("Punish false in litter");
                    //punish(false);
                    pet.GetComponent<Pet>().updateDiscipline(-5);
            }
        }
    }


    public void triggerPunishPraisePanelNotLitter(int i) {
        punishPraisePanelNotLitter.SetActive(!punishPraisePanelNotLitter.activeInHierarchy);
        pet.GetComponent<Pet>().updateWaste(-1);
        if (i > 0) {
            if (i == 1) {
                //Debug.Log("Praise false not litter");
                //praise(false);
                pet.GetComponent<Pet>().updateDiscipline(-5);
            } else if (i == 2) {
                //Debug.Log("Punish true not litter");
                //punish(true);
                pet.GetComponent<Pet>().updateDiscipline(5);
            }
        }
    }

    public void disableFood(){
        /*
        disableFood() when waste == 10
            Toggle PoopErr
            Toggle Food Buttons inactive

        enableFood() when waste !== 10
            Toggle PoopErr off
            Toggle Food Buttons active
        */
    }


    public void play(int i){
        int j = 5;
        if (i == 0)
            j = 50;
        pet.GetComponent<Pet>().updateHappiness(i);
        pet.GetComponent<Pet>().updateHealth(j);
        toggle(playPanel);
    }

    private void save(){
        pet.GetComponent<Pet>().savePet();
    }

    public void toggle(GameObject g) {
        if (g.activeInHierarchy)
            g.SetActive(false);
    }

}
