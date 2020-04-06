using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI score;
    // Update is called once per frame

    void Start()
    {
    	score = GetComponent<TextMeshProUGUI>();
    	
    }
    void Update()
    {
        score.text = FindObjectOfType<GameController>().getCurrency().ToString("0");
    }
}
