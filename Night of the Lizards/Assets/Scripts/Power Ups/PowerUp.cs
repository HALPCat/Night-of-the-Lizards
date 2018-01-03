using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{

    public abstract class PowerUp : MonoBehaviour
    {
        [SerializeField]
        bool playSound = true;

        Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                //Audio
                if (playSound) {
                    Instantiate(Resources.Load("PowerUpSoundPlayer"));
                }
                //Animation
                PillEffectScript pes = GameObject.Find("Power Up FX").GetComponent<PillEffectScript>();
                pes.PlayFX();

                PowerUpPlayer(collision.gameObject);
                Destroy(gameObject);
            }
        }

        protected abstract void PowerUpPlayer(GameObject player);
    }
}