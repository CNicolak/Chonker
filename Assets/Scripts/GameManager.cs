using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GameManager : MonoBehaviour {

    public GameObject happinessText;
    public GameObject hungerText;

    public GameObject namePanel;   
    public GameObject nameInput;        
    public GameObject nameText;    

    public GameObject pet;
    public GameObject petPanel;
    public GameObject[] petList;


    public GameObject foodPanel;
    public Sprite[] foodIcons;  
    // For food and toys, put a text that shows inventory number on hand.


    void Start() {
        if(!PlayerPrefs.HasKey("looks"))
            PlayerPrefs.SetInt("looks", 0);
        //createPet(0);  // Testing purposes.
        createPet(PlayerPrefs.GetInt("looks")); //create pet
    }

    void Update() {

        // currently cycles all the time.
        happinessText.GetComponent<Text>().text = "" + pet.GetComponent<Pet> ().happiness;
        hungerText.GetComponent<Text>().text = "" + pet.GetComponent<Pet> ().hunger;
        nameText.GetComponent<Text>().text = "" + pet.GetComponent<Pet> ().name;

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


    public void buttonBehavior(int i) {
        switch (i) {
        case(0):
        default:    // SKINS BUTTON
            petPanel.SetActive(!petPanel.activeInHierarchy);
            break;
        case(1):    // SHOP BUTTON
            //todo trigger Shop Scene
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
        // Creates a new pet and sets to new GameObject variable.

        toggle(petPanel);
        PlayerPrefs.SetInt("looks", i);

    }

    public void selectFood(int i) {
        toggle(foodPanel);
    }


    public void toggle(GameObject g) {
        if (g.activeInHierarchy)
            g.SetActive(false);
    }

}
