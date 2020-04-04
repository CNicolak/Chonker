using UnityEngine;
using System;
using System.Collections;

public class Pet : MonoBehaviour {

    [SerializeField]    
    private int _happiness;     // 100 to 0

    [SerializeField]
    private int _hunger;        // 100 to 0

    [SerializeField]
    private int _health;        // 100 to 0

    [SerializeField] 
    private string _name;

    [SerializeField]
    private int _discipline;        // 100 to 0

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

        // If obj is above 2.9f, Jump is true.
        GetComponent<Animator>().SetBool("Jump", gameObject.transform.position.y >-2.9f);

        if(Input.GetMouseButtonUp(0)){
            //Debug.Log("Clicked");
            Vector2 v = new Vector2(Input.mousePosition.x, Input.mousePosition.y); // grabs mouse location
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(v), Vector2.zero); // check for collision
            if (hit) {
                Debug.Log(hit.transform.gameObject.name);
                if(hit.transform.gameObject.tag == "pet"){
                    _clickCount++;
                    if(_clickCount >= 3){
                        _clickCount = 0;
                        updateHappiness(1);
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000000));
                    }
                }
            }
        }
    }


    void updateStatus() { // access saves

        if(!PlayerPrefs.HasKey("_hunger")){ 
            _hunger = 100;
            PlayerPrefs.SetInt("_hunger", _hunger);
        } else {
            _hunger = PlayerPrefs.GetInt("_hunger");
        }

        if(!PlayerPrefs.HasKey("_happiness")){
            _happiness = 100;
            PlayerPrefs.SetInt("_happiness", _happiness);
        } else {
            _happiness = PlayerPrefs.GetInt("_happiness");
        }

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

        //Debug.Log(getTimeSpan().ToString());
        Debug.Log(getTimeSpan().TotalHours);

        if(_serverTime)
            updateServer();
        else
            InvokeRepeating("updateDevice", 0f, 30f);
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

    public string name{
        get{ return _name; }
        set{ _name = value; }
    }

    // Increase/Decrease Happiness Meter
    public void updateHappiness(int i){
        happiness += i;
        if(happiness > 100)
            happiness = 100;
    }

    // Increase/Decrease Discipline Meter
    public void updateDiscipline(int i){
        discipline += i;
        if(discipline > 100)
            discipline = 100;
    }

/*
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
*/


    public void savePet(){
        if (!_serverTime)
            updateDevice(); // Store in PlayerPrefs
        PlayerPrefs.SetInt("_hunger", _hunger);
        PlayerPrefs.SetInt("_happiness", _happiness); 
        PlayerPrefs.SetInt("_health", _health);
        PlayerPrefs.SetInt("_discipline", _happiness);
    }

}
