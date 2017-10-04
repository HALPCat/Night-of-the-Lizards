using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {

    void Awake() {
        GameObject topPiece = (GameObject) Instantiate(Resources.Load("WallTopPiece"), new Vector3(transform.position.x, transform.position.y + 1), Quaternion.identity);
        topPiece.transform.parent = this.transform;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
