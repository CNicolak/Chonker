using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

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

    private int _weight;      // Modifier set when pet ages.
    private int agingFactor = 1;      

    private int _clickCount;

    public bool wasteLimitReached = false;
    public bool _serverTime = false;
    public String lastLogin = "";


    void Start() {
        //PlayerPrefs.SetString("then", "04/05/2020 12:00:00"); //Debugging
        updateStatus();
        agePet();
    }

    void Update(){
        clickPet();
    }

    public void clickPet(){

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
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000000));
                    }
                }
            }
        }
    }


    public void updateStatus() { 

        // Access saves to update meters. Adds initial value for first load.
        if(!PlayerPrefs.HasKey("name")) { 
            _name = "Chonker";
            PlayerPrefs.SetString("name", "Chonker");
        } else {            
        _name = PlayerPrefs.GetString ("name");
        }

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
            _hunger = 70;
            PlayerPrefs.SetInt("_hunger", _hunger);
        } else {
            _hunger = PlayerPrefs.GetInt("_hunger");
        }

        if(!PlayerPrefs.HasKey("_discipline")){ 
            _discipline = 10;
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

        if(!PlayerPrefs.HasKey("_weight")){ 
            _weight = 1;
            PlayerPrefs.SetInt("_weight", _weight);
        } else {
            _weight = PlayerPrefs.GetInt("_weight");
        }

        // Checks the time the last time the game was opened.
        if(!PlayerPrefs.HasKey("then"))
            PlayerPrefs.SetString("then", getStringTime());

         // Sets first login time (for egg)
        if(!PlayerPrefs.HasKey("firstLogin"))
            PlayerPrefs.SetString("firstLogin", getStringTime());           

        TimeSpan ts = getTimeSpan();     
        /*
        Calculations for first boot since user last played.
        ts. = Convert TotalHours to an int for meter subtraction.
        For every hour player hasn't played, do ____.
        */
        updateHunger( (((int)(ts.TotalHours * 2)) + _weight) ); // Increase by 2
        
        //updateHunger( (int)(ts.TotalHours * 2) ); // Increase by 2
        updateHappiness( ( (int)((hunger) * (ts.TotalHours / 5)) ) * (-1) );
        updateDiscipline( ( (int)(ts.TotalHours * 0.005) ) * (-1) ); // Decrease
        
        if(_serverTime)
            //updateServer(serverDate);
            updateServer(lastLogin);
        else
            InvokeRepeating("updateDevice", 0f, 10f);
        
        //Debug.Log(getTimeSpan().ToString());
        //Debug.Log(getTimeSpan().TotalHours);
    
    }

    void updateServer(String s){
        //PlayerPrefs.SetString("then", "04/05/2020 12:00:00");
        PlayerPrefs.SetString("then", s);
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

    TimeSpan getLifeSpan() {
        return DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("firstLogin"));
    }

    string getStringTime(){
        DateTime now =  DateTime.Now;
        return now.Month + "/" + now.Day + "/" + now.Year + " " + now.Hour + ":" + now.Minute + ":" + now.Second;
    }

    void agePet(){
        Debug.Log("Pet Lifespan:");
        Debug.Log(getLifeSpan().TotalHours);

        if (getLifeSpan().TotalHours < 1 * agingFactor) {
            age = 0;      // Egg
            _weight = 0;
        } else if (getLifeSpan().TotalHours < 2 * agingFactor){
            age = 1;       // Baby
            _weight = 1;
          } else if (getLifeSpan().TotalHours < 3 * agingFactor){
            age = 2;        // Child
            _weight = 2;
        } else if (getLifeSpan().TotalHours < 4 * agingFactor){
            age = 3;        // Adult
            _weight = 3;
         } else {
            age = 4;        // Senior
            _weight = 1;
        }
        //petDeath();
    }

    void petDeath(){
        SceneManager.LoadScene("Dead"); 
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
    public string Petname{
        get{ return _name; }
        set{ _name = value; }
    }
    public int age{
        get{ return _age; }
        set{ _age = value; }
    }

    // ---------------------

    // Update Meters. Add boundaries/range.
     public void updateHealth(int i){
        health += i;
        if(health > 100) {
            health = 100;
        } else if (health < 0) {
            health = 0;
            petDeath();
        }
    }

    public void updateHappiness(int i){
        happiness += i;
        if(happiness > 100) {
            happiness = 100;
        } else if (happiness < 0) {
            happiness = 0;
        }
        if (_happiness == 0)        
            updateHealth(-25);       
    }

    public void updateHunger(int i){
        hunger += i;
        if(hunger > 100) {
            hunger = 100;
        } else if (hunger < 0) {
            hunger = 0;
        }
        //if (_hunger > 90)        
        //    updateHealth(-10);         
    }

    public void updateDiscipline(int i){
        discipline += i;
        if(discipline > 100) {
            discipline = 100;
        } else if (discipline < 0) {
            discipline = 0;
        }   
        if (_discipline < 0)        
            updateHealth(-5);           
    }

    public void updateWaste(int i){
        waste += i;
        if(waste >= 10) {
            waste = 10;
            wasteLimitReached = true;
        } else if (waste < 0) {
            waste = 0;
            wasteLimitReached = false;
        }
        if (_waste > 5)
            updateHealth(-5);   
        if (_waste < 10)
            wasteLimitReached = false;            
    }

/*
    public void updateWellness(){
        if (_happiness < 25)        
            updateHealth(-30);         
        if (_hunger > 90)        
            updateHealth(-10); 
        if (_discipline < 20)        
            updateHealth(-10);
        if (_waste > 5)        
            updateHealth(-25);    
    }
*/

    // FOR TESTING
    public void setLastPlayed(String s){
        _serverTime = true;
        lastLogin = s;
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
