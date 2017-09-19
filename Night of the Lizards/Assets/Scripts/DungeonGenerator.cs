using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour {
    public GridHandler gridHandler;
    public GameObject[] dungeonTiles;
    
    void Awake() {
    }

    //OnEnable is called after Awake but before Start
    //fillDungeon is called in Awake and buildDungeon is called in Start
    //setGrid only works in OnEnable because it's in between the two
    //this is retarded
	void OnEnable () {
        //gridHandler.setGrid(0, 0, Resources.Load("Floor") as GameObject);
    }

    

}
