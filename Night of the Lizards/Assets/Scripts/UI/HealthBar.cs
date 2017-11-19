using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LizardNight
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        int reactiveFallSpeed;
        [SerializeField]
        PlayerScript player;
        

        RectTransform healthBarBase;

        [SerializeField]
        RectTransform healthBarActive;
        [SerializeField]
        Text healthText;
        [SerializeField]
        RectTransform healthBarReactive;
        [SerializeField]
        RectTransform healthBarLost;

        Transform playerHealthTransform;

        // Use this for initialization
        void Start()
        {

            healthBarBase = GetComponent<RectTransform>();
            
            playerHealthTransform = healthText.GetComponent<Transform>();


            
            //activeHealth.sizeDelta = new Vector2(170, 30);
        }

        // Update is called once per frame
        void Update()
        {
            //Base size
            healthBarBase.sizeDelta = new Vector2(player.GetMaxHealth() * 3 + 10, 40);
            //Active size
            healthBarActive.sizeDelta = new Vector2(player.GetCurrentHealth() * 3, 30);
            //Lost size
            healthBarLost.sizeDelta = new Vector2(player.GetMaxHealth() * 3, 30);

            //Health text position and text content
            healthText.text = ""+player.GetCurrentHealth();
            playerHealthTransform.localPosition = new Vector2(healthBarBase.sizeDelta.x + 5, 20);


            //Reactive width
            if(healthBarActive.sizeDelta.x > healthBarReactive.sizeDelta.x) {
                healthBarReactive.sizeDelta = new Vector2(healthBarActive.sizeDelta.x, 30);
            }else {
                healthBarReactive.sizeDelta = new Vector2(healthBarReactive.sizeDelta.x - Time.deltaTime * reactiveFallSpeed, healthBarReactive.sizeDelta.y);
            }



            //Debug.Log(player.GetMaxHealth()); //45

        }
    }
}
