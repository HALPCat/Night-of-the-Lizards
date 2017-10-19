using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    GameObject enemyPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump")) {
            GameObject enemy;
            enemy = Instantiate(enemyPrefab, GetComponentInParent<Transform>().position, Quaternion.identity);
            Debug.Log("Enemy spawned");
        }
	}
}
