using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{
    public class EnemySpawner : MonoBehaviour
    {

        [SerializeField]

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void spawnEnemy(GameObject enemyPrefab)
        {
            spawnEnemy(enemyPrefab, 1);
        }

        void spawnEnemy(GameObject enemyPrefab, int level)
        {
            GameObject enemy;
            enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            BasicEnemy enemySheet = enemy.GetComponent<BasicEnemy>();

            enemySheet.SetLevel(level);
                       
        }
    }
}
