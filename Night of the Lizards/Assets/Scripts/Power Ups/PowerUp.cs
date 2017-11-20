using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{

    public abstract class PowerUp : MonoBehaviour
    {
        Rigidbody2D _rigidbody;

               private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                PowerUpPlayer(collision.gameObject);
                Destroy(gameObject);
            }
        }

        protected abstract void PowerUpPlayer(GameObject player);
    }
}