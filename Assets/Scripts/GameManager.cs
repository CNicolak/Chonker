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
    //public GameObject skin1Text;   // Unneeded, default skin
    public GameObject skin2Text;
    public GameObject skin3Text;
    public GameObject skin4Text;  
     //public Button skin1Button;   // Unneeded, default skin
    public Button skin2Button;
    public Button skin3Button;
    public Button skin4Button;     

    public GameObject foodPanel;
    public GameObject foodErrText;
    public GameObject foodQtyText;    
    public GameObject treatQtyText; 
    public Button treatButton;
    public Button foodButton;        
    public Sprite[] foodIcons;  

    public GameObject shopPanel;   
    public GameObject playPanel;
    public GameObject toyQtyText; 
    public Button toyButton;            

    public GameObject poopManager;
    public GameObject punishPraisePanelLitter;
    public GameObject punishPraisePanelNotLitter;

    public GameObject[] buttonList;        // For Test Panel
    public GameObject timePanel;     
    public GameObject FirstLoginInput;
    public GameObject LastLoginInput;
    public GameObject FirstLoginText;
    public GameObject LastLoginText;              


    void Start() {
        if(!PlayerPrefs.HasKey("looks"))
            PlayerPrefs.SetInt("looks", 0);

        createPet(PlayerPrefs.GetInt("looks")); //create pet

        if(!PlayerPrefs.HasKey("currency"))
            PlayerPrefs.SetInt("currency", 500); 

        if(!PlayerPrefs.HasKey("age"))
            PlayerPrefs.SetInt("age", 0);

        if (!PlayerPrefs.HasKey("poopLitter"))
            PlayerPrefs.SetInt("poopLitter", 0);

        if (!PlayerPrefs.HasKey("poopNotLitter"))
            PlayerPrefs.SetInt("poopNotLitter", 0);

        poopManager.GetComponent<PoopManager>().generateLitter();
        poopManager.GetComponent<PoopManager>().generateNotLitter();

    }


    void Update() {     // currently cycles all the time.
        healthText.GetComponent<Text>().text = "" + pet.GetComponent<Pet> ().health;
        happinessText.GetComponent<Text>().text = "" + pet.GetComponent<Pet> ().happiness;
        hungerText.GetComponent<Text>().text = "" + pet.GetComponent<Pet> ().hunger;
        disciplineText.GetComponent<Text>().text = "" + pet.GetComponent<Pet> ().discipline;
        wasteText.GetComponent<Text>().text = "" + pet.GetComponent<Pet> ().waste;                 
        nameText.GetComponent<Text>().text = "" + pet.GetComponent<Pet> ().Petname;
        currencyText.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("currency");
        ageName(pet.GetComponent<Pet>().age);

        //if (Input.GetKeyUp(KeyCode.Space)) // spacebar command
    }

    public void ageName(int i){
        switch (i) {
        case(0):
        default:    // Pet default
            ageText.GetComponent<Text>().text = "Egg";       
            break;
        case(1): 
            ageText.GetComponent<Text>().text = "Baby";
            break;
        case(2):
            ageText.GetComponent<Text>().text = "Child"; 
            break;
        case(3):
            ageText.GetComponent<Text>().text = "Adult"; 
            break;    
         case(4):
            ageText.GetComponent<Text>().text = "Senior"; 
            break;     
        case(5):
            ageText.GetComponent<Text>().text = "Dead"; 
            break;                              
        }
    }

    public void triggerNamePanel(bool b){
        //namePanel.SetActive(!namePanel.activeInHierarchy); 
        Debug.Log("entered trigger name panel");
        if (b){
            pet.GetComponent<Pet>().Petname = nameInput.GetComponent<InputField>().text;
            PlayerPrefs.SetString("name", pet.GetComponent<Pet>().Petname);
        }
        toggle(namePanel);
    }


    // Bottom Menu Bar
    public void buttonBehavior(int i) {
        switch (i) {
        case(0):
        default:    // SKINS BUTTON
            petPanel.SetActive(!petPanel.activeInHierarchy);

            // Enable Buttons
            if (PlayerPrefs.GetInt("Hat") == 1) {
                skin2Button.interactable = true;
                skin2Text.SetActive(false);
                }
            if (PlayerPrefs.GetInt("BlackCat") == 1) {
                skin3Button.interactable = true;
                skin3Text.SetActive(false);
                }
             if (PlayerPrefs.GetInt("BlackCat") == 1 & PlayerPrefs.GetInt("Hat") == 1) {
                skin4Button.interactable = true;
                skin4Text.SetActive(false);
                }                               

            break;

        case(1):    // SHOP BUTTON
            save();
            shopPanel.SetActive(!shopPanel.activeInHierarchy);
            break;

        case(2):    // FEED BUTTON
            foodPanel.SetActive(!foodPanel.activeInHierarchy);

            foodQtyText.GetComponent<Text>().text = "Unlimited";

            // Enable Treat Button
            treatQtyText.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("Fish");
            if (PlayerPrefs.GetInt("Fish") > 0) {
                treatButton.interactable = true;
            } else {
                treatButton.interactable = false;
                //PlayerPrefs.SetInt("Fish", 0); //Prevents negative qty if glitch.
            }
            break;

        case(3):    // PLAY BUTTON
            save();
            playPanel.SetActive(!playPanel.activeInHierarchy);

            // Enable Toy Button
            if (PlayerPrefs.GetInt("Ball") == 1) {
                toyButton.interactable = true;
                toyQtyText.SetActive(false); 
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

        //toggle(petPanel);
        PlayerPrefs.SetInt("looks", i);
    }

    public void changeSkin(int i){
        createPet(i);
        toggle(petPanel);
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
            SceneManager.LoadScene("Minigame2");                         
            Debug.Log("Switch to mini-game 1");   
            break;
        case(2):    // MINI-GAME 2
            play(10);
            SceneManager.LoadScene("MiniGame1");
            Debug.Log("Switch to mini-game 2"); 
            break;
        }
    }


    public void selectFood(int i) {
        bool maxWaste = pet.GetComponent<Pet>().wasteLimitReached;
        switch (i) {
        case(0):
        default:    // FOOD
            if (maxWaste == true) {
                foodErrText.SetActive(true);
            } else {
                foodErrText.SetActive(false);
                feed(-5, 5); 
            }
                      
            break;
        case(1):    // TREAT / FISH
            if (maxWaste == true) {
                foodErrText.SetActive(true);
            } else {
                foodErrText.SetActive(false);        
                PlayerPrefs.SetInt("Fish", PlayerPrefs.GetInt("Fish")-1);
                treatQtyText.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("Fish");  
                feed(-50, 50);
            }                
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


	public void feed(int i, int j){
        pet.GetComponent<Pet>().updateHunger(i);
        pet.GetComponent<Pet>().updateHappiness(j);
        pet.GetComponent<Pet>().updateWaste(1);	
        poopManager.GetComponent<PoopManager>().generatePoop();
        toggle(foodPanel);
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
        else
            g.SetActive(true);
    }


    // FOR TESTING BUTTONS ----------------------------
    public void testSuite(int i) {
        switch (i) {
        case(0):
        default:    // Delete Save File
            PlayerPrefs.DeleteAll();
            break;
        case(1):    // Reload "Game"
            SceneManager.LoadScene("Game");    
            break;
        case(2):    // Add Currency
            PlayerPrefs.SetInt("currency", PlayerPrefs.GetInt("currency") + 10000);    
            break;
        case(3):    // Remove Items
            PlayerPrefs.SetInt("BlackCat", 0); 
            PlayerPrefs.SetInt("Hat", 0);  
            PlayerPrefs.SetInt("Ball", 0);
            PlayerPrefs.SetInt("Fish", 0);        
            break;
        case(4):    // Set Time
            //Format: "04/05/2020 12:00:00"
            toggle(timePanel);
            Debug.Log(pet.GetComponent<Pet>()._serverTime);

            // Refresh Visible text fields
            FirstLoginText.GetComponent<Text>().text = "" + PlayerPrefs.GetString("firstLogin");
            LastLoginText.GetComponent<Text>().text = "" + PlayerPrefs.GetString("then");
            FirstLoginInput.GetComponent<InputField>().text = PlayerPrefs.GetString("firstLogin");
            LastLoginInput.GetComponent<InputField>().text = PlayerPrefs.GetString("then");
            break;
                                
        }
    }

      public void setTime(){
            // setLastPlayed
            PlayerPrefs.SetString("firstLogin", FirstLoginInput.GetComponent<InputField>().text);
            PlayerPrefs.SetString("then", LastLoginInput.GetComponent<InputField>().text);
            pet.GetComponent<Pet>().setLastPlayed(LastLoginInput.GetComponent<InputField>().text);

            // Refresh Text fields
            FirstLoginText.GetComponent<Text>().text = "" + PlayerPrefs.GetString("firstLogin");
            LastLoginText.GetComponent<Text>().text = "" + PlayerPrefs.GetString("then");
            
            Debug.Log(pet.GetComponent<Pet>()._serverTime);
            Debug.Log(pet.GetComponent<Pet>().lastLogin);
            save();
            //pet.GetComponent<Pet>().setLastPlayed(LastLoginInput.GetComponent<InputField>().text);  
    }  

    // -----------------------------------------------


}
