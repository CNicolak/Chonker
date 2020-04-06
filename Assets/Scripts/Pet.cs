using UnityEngine;
using System;
using System.Collections;

public class Pet : MonoBehaviour {

    // Initialization
    [SerializeField] 
    private string _name;
    [SerializeField]
    private int _health;       // 0 to 100
    [SerializeField]    
    private int _happiness;    // 0 to 100
    [SerializeField]
    private int _hunger;      // 0 to 100
    [SerializeField]
    private int _discipline;   // 0 to 100
    [SerializeField]
    private int _waste;       // 0 to 10
    [SerializeField]
    private int _age;        // 0 to 5 (Egg, Baby, Child, Adult, Senior, Dead)

    private bool _serverTime;
    private int _clickCount;

    void Start() {
        //PlayerPrefs.SetString("then", "03/27/2020 17:00:00"); //Debugging
        updateStatus();

        if(!PlayerPrefs.HasKey("name"))
            PlayerPrefs.SetString("name", "Chonker");
        _name = PlayerPrefs.GetString ("name");
    }

    void Update(){

        // Allows user to click pet.
        // If obj is above 2.9f, Jump is true.
        GetComponent<Animator>().SetBool("Jump", gameObject.transform.position.y >-2.9f);

        if(Input.GetMouseButtonUp(0)){
            //Debug.Log("Clicked");
            Vector2 v = new Vector2(Input.mousePosition.x, Input.mousePosition.y); // grabs mouse location
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(v), Vector2.zero); // check for collision
            if (hit) {
                //Debug.Log(hit.transform.gameObject.name);
                if(hit.transform.gameObject.tag == "pet"){
                    _clickCount++;
                    if(_clickCount >= 3){
                        _clickCount = 0;
                        //updateHappiness(1);
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000000));
                    }
                }
            }
        }
    }


    public void updateStatus() { 

        // Access saves to update meters. Adds initial value for first load.
        if(!PlayerPrefs.HasKey("_health")){ 
            _health = 100;
            PlayerPrefs.SetInt("_health", _health);
        } else {
            _health = PlayerPrefs.GetInt("_health");
        }

        if(!PlayerPrefs.HasKey("_happiness")){
            _happiness = 100;
            PlayerPrefs.SetInt("_happiness", _happiness);
        } else {
            _happiness = PlayerPrefs.GetInt("_happiness");
        }

        if(!PlayerPrefs.HasKey("_hunger")){ 
            _hunger = 100;
            PlayerPrefs.SetInt("_hunger", _hunger);
        } else {
            _hunger = PlayerPrefs.GetInt("_hunger");
        }

        if(!PlayerPrefs.HasKey("_discipline")){ 
            _discipline = 0;
            PlayerPrefs.SetInt("_discipline", _discipline);
        } else {
            _discipline = PlayerPrefs.GetInt("_discipline");
        }

        if(!PlayerPrefs.HasKey("_waste")){ 
            _waste = 0;
            PlayerPrefs.SetInt("_waste", _waste);
        } else {
            _waste = PlayerPrefs.GetInt("_waste");
        }


        // Checks the time the last time the game was opened.
        if(!PlayerPrefs.HasKey("then"))
            PlayerPrefs.SetString("then", getStringTime());

        TimeSpan ts = getTimeSpan();

        // For every hour player hasn't played, subtract 2 from hunger
        // Convert TotalHours to an int for meter subtraction
        _hunger -= (int)(ts.TotalHours * 2);     
        if(_hunger < 0)                         
            _hunger = 0;
        _happiness -= (int)((100 - _hunger) * (ts.TotalHours / 5));
        if(_happiness < 0)                         
            _happiness = 0;

        if(_waste < 0)                         
            _waste = 0;  

        //Debug.Log(getTimeSpan().ToString());
        Debug.Log(getTimeSpan().TotalHours);

        if(_serverTime)
            updateServer();
        else
            InvokeRepeating("updateDevice", 0f, 10f);
    }

    void updateServer(){
    }

    void updateDevice(){
        PlayerPrefs.SetString("then", getStringTime());
    }

    TimeSpan getTimeSpan() {
        if(_serverTime)
            return new TimeSpan();
        else   
            return DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("then"));
    }


    string getStringTime(){
        DateTime now =  DateTime.Now;
        return now.Month + "/" + now.Day + "/" + now.Year + " " + now.Hour + ":" + now.Minute + ":" + now.Second;
    }

    // Initialization
    public int hunger{
        get{ return _hunger; }
        set{ _hunger = value; }
    }
    public int happiness{
        get{ return _happiness; }
        set{ _happiness = value; }
    }
    public int health{
        get{ return _health; }
        set{ _health = value; }
    }
    public int discipline{
        get{ return _discipline; }
        set{ _discipline = value; }
    }
    public int waste{
        get{ return _waste; }
        set{ _waste = value; }
    }
    public string name{
        get{ return _name; }
        set{ _name = value; }
    }
    public int age{
        get{ return _age; }
        set{ _age = value; }
    }


    // Update Meters. Add boundaries/range.
     public void updateHealth(int i){
        health += i;
        if(health > 100) {
            health = 100;
        } else if (health < 0) {
            health = 0;
        }
    }

    public void updateHappiness(int i){
        happiness += i;
        if(happiness > 100) {
            happiness = 100;
        } else if (happiness < 0) {
            happiness = 0;
        }
    }
    public void updateHunger(int i){
        hunger += i;
        if(hunger > 100) {
            hunger = 100;
        } else if (hunger < 0) {
            hunger = 0;
        }
    }
    public void updateDiscipline(int i){
        discipline += i;
        if(discipline > 100) {
            discipline = 100;
        } else if (discipline < 0) {
            discipline = 0;
        }      
    }

    public void updateWaste(int i){
        waste += i;
        if(waste > 10) {
            waste = 10;
        } else if (waste < 0) {
            waste = 0;
        }             
    }


    // Saves the pet data to PlayerPrefs
    public void savePet(){
        if (!_serverTime)
            updateDevice(); // Store in PlayerPrefs

        PlayerPrefs.SetInt("_hunger", _hunger);
        PlayerPrefs.SetInt("_happiness", _happiness); 
        PlayerPrefs.SetInt("_health", _health);
        PlayerPrefs.SetInt("_discipline", _discipline);
        PlayerPrefs.SetInt("_waste", _waste);      
    }

}
