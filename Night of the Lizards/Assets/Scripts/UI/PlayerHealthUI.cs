using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LizardNight;

public class PlayerHealthUI : MonoBehaviour {

    Text playerHealthText;
    PlayerScript playerScript;

    // Use this for initialization
    void Start () {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        playerHealthText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        playerHealthText.text = "Player health: " + playerScript.GetCurrentHealth();
	}
}
