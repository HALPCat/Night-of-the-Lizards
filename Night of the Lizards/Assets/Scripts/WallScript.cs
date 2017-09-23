using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {

    void Awake() {
        Instantiate(Resources.Load("WallTopPiece"), new Vector3(transform.position.x, transform.position.y + 1), Quaternion.identity);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
