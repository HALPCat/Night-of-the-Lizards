using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LizardNight
{
    public class GameMaster : MonoBehaviour
    {
        //so it's readable by characters
        public static GameMaster GM = null;
        public bool playersTurn = true;
        public float turnTimer = 0.1f;

        private List<BasicEnemy> enemies;
        private bool enemiesTurn, paused = false;

        public Canvas pauseScreen;


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
              
            }
            if (!paused)
            {
                if (playersTurn || enemiesTurn)
                    return;

                StartCoroutine(EnemyTurn());

            }

        }

        public void PauseGame()
        {
            paused = !paused;
            pauseScreen.gameObject.SetActive(paused);
        }

        //in teh end
        public void GameOver()
        {
            enabled = false;
        }

        private void Awake()
        {
            //to make sure only one GM exists
            if (GM == null)
                GM = this;
            else if (GM != this)
                Destroy(gameObject);

            //creates a list for enemis on the level
            enemies = new List<BasicEnemy>();

        }

        //add the enemies to the list
        public void AddEnemy(BasicEnemy enemy)
        {
            enemies.Add(enemy);
        }

        //remove the enemies from the list
        public void RemoveEnemy(BasicEnemy enemy)
        {
            enemies.Remove(enemy);
        }

        //coroutine for enemies turn
        IEnumerator EnemyTurn()
        {
            enemiesTurn = true;

            //turn delay
            yield return new WaitForSeconds(turnTimer);


            //if there are no enemies
            if (enemies.Count == 0)
            {
                yield return null;
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].stationary)
                    enemies[i].Movement();

                yield return null;
            }

            //player can now move
            enemiesTurn = false;

            playersTurn = true;

        }
        public void ClearEnemies()
        {
            foreach (BasicEnemy enemy in enemies)
            {
                enemies.Remove(enemy);
                Destroy(enemy.gameObject);
            }
        }

    }

}
