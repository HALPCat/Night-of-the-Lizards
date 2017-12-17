using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{
    public class StairsScript : MonoBehaviour
    {
        [SerializeField]
        Sprite[] stairsUpSprites;
        [SerializeField]
        Sprite[] stairsDownSprites;

        GridHandler gridHandler;

        void Awake()
        {
            gridHandler = FindObjectOfType(typeof(GridHandler)) as GridHandler;

            if (this.name.Equals("StairsUp(Clone)")) {
                //Bedroom
                if (gridHandler.getFloorType().Equals("bedroom")) {
                    this.GetComponent<SpriteRenderer>().sprite = stairsUpSprites[0];
                }
                //swamp
                else if (gridHandler.getFloorType().Equals("swamp")) {
                    this.GetComponent<SpriteRenderer>().sprite = stairsUpSprites[1];
                }
                //disco
                else {
                    this.GetComponent<SpriteRenderer>().sprite = stairsUpSprites[2];
                }
            } else if (this.name.Equals("StairsDown(Clone)")) {
                //Bedroom
                if (gridHandler.getFloorType().Equals("bedroom")) {
                    this.GetComponent<SpriteRenderer>().sprite = stairsDownSprites[0];
                }
                //swamp
                else if (gridHandler.getFloorType().Equals("swamp")) {
                    this.GetComponent<SpriteRenderer>().sprite = stairsDownSprites[1];
                }
                //disco
                else {
                    this.GetComponent<SpriteRenderer>().sprite = stairsDownSprites[2];
                }
            }else {
                Debug.LogError("This isn't an up or down stair lol idk");
            }
        }
    }
}
