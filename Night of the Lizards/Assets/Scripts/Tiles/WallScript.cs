using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight {
    public class WallScript : MonoBehaviour
    {
        [SerializeField]
        Sprite[] wallSprites;

        GridHandler gridHandler;
        
        void Awake()
        {
            gridHandler = FindObjectOfType(typeof(GridHandler)) as GridHandler;
            GameObject topPiece;

            //Bedroom wall
            if (gridHandler.getFloorType().Equals("bedroom")) {
                this.GetComponent<SpriteRenderer>().sprite = wallSprites[0];
                topPiece = (GameObject)Instantiate(Resources.Load("WallTopPiece"), new Vector3(transform.position.x, transform.position.y +1), Quaternion.identity);
                topPiece.transform.parent = this.transform;
            }
            //swamp wall
            else if (gridHandler.getFloorType().Equals("swamp")) {
                this.GetComponent<SpriteRenderer>().sprite = wallSprites[1];
                float r = Random.value;
                if (r < 0.5f) {
                    topPiece = (GameObject)Instantiate(Resources.Load("WallTopPiece"), new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
                    topPiece.transform.parent = this.transform;
                }
            }
            //disco wall
            else {
                this.GetComponent<SpriteRenderer>().sprite = wallSprites[2];
                topPiece = (GameObject)Instantiate(Resources.Load("WallTopPiece"), new Vector3(transform.position.x, transform.position.y+1), Quaternion.identity);
                topPiece.transform.parent = this.transform;
            }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
