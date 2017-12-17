using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight {
    public class FloorScript : MonoBehaviour
    {
        [SerializeField]
        Sprite[] floorSprites;

        GridHandler gridHandler;
        
        void Awake()
        {
            gridHandler = FindObjectOfType(typeof(GridHandler)) as GridHandler;

            //Bedroom floor
            if (gridHandler.getFloorType().Equals("bedroom")) {
                this.GetComponent<SpriteRenderer>().sprite = floorSprites[0];
            }
            //swamp floor
            else if (gridHandler.getFloorType().Equals("swamp")) {
                this.GetComponent<SpriteRenderer>().sprite = floorSprites[1];
            }
            //disco floor
            else {
                this.GetComponent<SpriteRenderer>().sprite = floorSprites[2];
            }
        }
    }

}
