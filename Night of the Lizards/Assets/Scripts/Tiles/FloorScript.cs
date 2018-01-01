using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight {
    public class FloorScript : MonoBehaviour
    {
        [SerializeField]
        Sprite[] bedroomSprites;
        [SerializeField]
        Sprite[] swampSprites;
        [SerializeField]
        Sprite[] discoSprites;

        GridHandler gridHandler;
        
        void Awake()
        {
            gridHandler = FindObjectOfType(typeof(GridHandler)) as GridHandler;

            //Bedroom floor
            if (gridHandler.getFloorType().Equals("bedroom")) {
                this.GetComponent<SpriteRenderer>().sprite = bedroomSprites[0];
            }
            //swamp floor
            else if (gridHandler.getFloorType().Equals("swamp")) {
                float r = Random.value;
                if (r < 0.5f) {
                    this.GetComponent<SpriteRenderer>().sprite = swampSprites[0];
                } else {
                    this.GetComponent<SpriteRenderer>().sprite = swampSprites[1];
                }
            }
            //disco floor
            else {
                float r = Random.value;
                if (r < 0.5f) {
                    this.GetComponent<SpriteRenderer>().sprite = discoSprites[0];
                } else {
                    this.GetComponent<SpriteRenderer>().sprite = discoSprites[1];
                }
            }
        }
    }

}
