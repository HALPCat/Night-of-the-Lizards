using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeScript : MonoBehaviour {
    //For checking what's on the map
    public GridHandler gridHandler;

    //This is for movement
    float timer = 0;

    //Poisition variables
    int positionX;
    int positionY;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        //Lets keep positions updated
        positionX = (int)transform.position.x;
        positionY = (int)transform.position.y;

        //Going up
        if (Input.GetAxisRaw("Vertical") > 0) {
            //This is spaghetti and should be tied to the button name rather than actual keys
            if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
                moveVertical(1);
                timer = 0;
            }
            timer += Time.deltaTime;
            if(timer >= 0.25f) {
                moveVertical(1);
                timer = 0;
            }
        }

        //Going down
        else if (Input.GetAxisRaw("Vertical") < 0) {
            //This is spaghetti and should be tied to the button name rather than actual keys
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
                moveVertical(-1);
                timer = 0;
            }
            timer += Time.deltaTime;
            if (timer >= 0.25f) {
                moveVertical(-1);
                timer = 0;
            }
        }

        //Going left
        else if (Input.GetAxisRaw("Horizontal") < 0) {
            //This is spaghetti and should be tied to the button name rather than actual keys
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
                moveHorizontal(-1);
                timer = 0;
            }
            timer += Time.deltaTime;
            if (timer >= 0.25f) {
                moveHorizontal(-1);
                timer = 0;
            }
        }

        //Going right
        else if (Input.GetAxisRaw("Horizontal") > 0) {
            //This is spaghetti and should be tied to the button name rather than actual keys
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
                moveHorizontal(1);
                timer = 0;
            }
            timer += Time.deltaTime;
            if (timer >= 0.25f) {
                moveHorizontal(1);
                timer = 0;
            }
        }
    }

    void moveVertical(int tiles) {
        //Move to new position if it is not a wall
        if (!gridHandler.getGrid(positionX, positionY + tiles).name.Equals("Wall")) {
            transform.Translate(new Vector3(0, tiles));
        } else {
            Debug.Log("Couldn't move");
        }
    }
    void moveHorizontal(int tiles) {
        //Move to new position if it is not a wall
        if (!gridHandler.getGrid(positionX + tiles, positionY).name.Equals("Wall")) {
            transform.Translate(new Vector3(tiles, 0));
        }else {
            Debug.Log("Couldn't move");
        }
    }
}
