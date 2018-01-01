using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight {
    public class WallTopScript : MonoBehaviour {
        [SerializeField]
        Sprite[] bedroomSprites;
        [SerializeField]
        Sprite[] swampSprites;
        [SerializeField]
        Sprite[] discoSprites;

        GridHandler gridHandler;

        int wallX;
        int wallY;

        bool north = false;

        void Awake() {
            gridHandler = FindObjectOfType(typeof(GridHandler)) as GridHandler;
            if (gridHandler != null) {
                //Debug.Log("WallTopScript found grid handler!");
            }
            
            wallX = (int)transform.position.x;

            //If not a swamp, put the top tile a bit up
            if (!gridHandler.getFloorType().Equals("swamp")) {
                wallY = (int)transform.position.y - 1;
            }
            //Exactly on top if a swamp
            else {
                wallY = (int)transform.position.y;
            }
        }

        // Use this for initialization
        void Start() {
            checkSurroundings();
            updateSprite();

        }

        // Update is called once per frame
        void Update() {

        }

        void checkSurroundings() {
            //wallY at the top row always equals dungeonHeight-1 because indexing starts at 0..
            if (wallY < gridHandler.DungeonHeight - 1) {
                if (gridHandler.getTileGrid(wallX, wallY + 1).name.Equals("Wall(Clone)")) {
                    north = true;
                }
            }
        }

        void updateSprite() {
            if (north) {
                //Bedroom
                if (gridHandler.getFloorType().Equals("bedroom")) {
                    this.GetComponent<SpriteRenderer>().sprite = bedroomSprites[0];
                }
                //Swamp
                else if (gridHandler.getFloorType().Equals("swamp")) {
                    this.GetComponent<SpriteRenderer>().sprite = swampSprites[0];
                }
                //Disco
                else {
                    this.GetComponent<SpriteRenderer>().sprite = discoSprites[0];
                }
            } else {
                //Bedroom
                if (gridHandler.getFloorType().Equals("bedroom")) {
                    this.GetComponent<SpriteRenderer>().sprite = bedroomSprites[1];
                }
                //Swamp
                else if (gridHandler.getFloorType().Equals("swamp")) {
                    this.GetComponent<SpriteRenderer>().sprite = swampSprites[0];
                }
                //Disco
                else {
                    this.GetComponent<SpriteRenderer>().sprite = discoSprites[1];
                }
            }

        }
    }
}

