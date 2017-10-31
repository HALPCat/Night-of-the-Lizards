using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    void spawnEnemy(GameObject enemyPrefab) {
        GameObject enemy;
        enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
