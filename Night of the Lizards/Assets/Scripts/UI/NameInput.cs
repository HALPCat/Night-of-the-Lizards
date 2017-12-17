using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LizardNight
{

    public class NameInput : MonoBehaviour
    {
        public InputField charName;
        public PlayerScript player;


        private void OnEnable()
        {
            Time.timeScale = 0f;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;
        }
        private void OnDestroy()
        {
            player.Die();
        }

        public void onGetName ()
        {
            player.playerName = charName.text;            
            Destroy(this.gameObject);
        }
    }
}
