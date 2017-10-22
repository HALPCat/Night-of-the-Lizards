﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LizardNight
{

    public class BasicEnemy : CharacterBase
    {
        //if we do the skip turn speed thing right?
        private bool skipTurn;

        //has seen player
        public bool triggered = false;
        //player position (if triggered)
        GameObject target;
        int targetY, targetX;

        //combat
        EnemyOne attributes;
        //pathfinding

        Vector3 path;
        int targetIndex;


        [SerializeField]
        protected bool _stationary;

        public bool stationary
        {
            get { return _stationary; }
        }

        // Use this for initialization
        protected override void Start()
        {
            //adds enemy to the game master list
            //GameMaster.GM.AddEnemy(this);

            attributes = GetComponent<EnemyOne>();

            base.Start();

        }

        // Update is called once per frame
        protected override void Update()
        {



        }

        void OnBecameInvisible()
        {
            Debug.Log(this.name + " is now invisible, disabling.");
            GetComponent<BasicEnemy>().enabled = false;
            GameMaster.GM.RemoveEnemy(this);
        }

        void OnBecameVisible()
        {
            Debug.Log(this.name + " is now visible, enabling.");
            GetComponent<BasicEnemy>().enabled = true;
            GameMaster.GM.AddEnemy(this);
        }

        protected override void Move()
        {
            //if not triggered, will move randomly
            int dir = UnityEngine.Random.Range(1, 8);

            while (!canMove(dir))
            {
                dir = UnityEngine.Random.Range(1, 8);
            }

            switch (dir)
            {
                case 1:
                    moveHorizontal(1);
                    break;
                case 2:
                    moveHorizontal(-1);
                    break;
                case 3:
                    moveVertical(1);
                    break;
                case 4:
                    moveVertical(-1);
                    break;
                case 5:
                    moveDiagonal(1, 1);
                    break;
                case 6:
                    moveDiagonal(-1, 1);
                    break;
                case 7:
                    moveDiagonal(1, -1);
                    break;
                case 8:
                    moveDiagonal(-1, -1);
                    break;

            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("entered trigger");
            if (col.tag == "Player")
            {
                triggered = true;
                target = col.gameObject;
            }


        }

        public void Movement()
        {
            //Don't do anything if disabled
            if (!gameObject.GetComponent<BasicEnemy>().enabled)
            {
                return;
            }

            if (!triggered)
            {
                Move();  
            }
            //if triggered by player will pursue
            else
            {

                PathRequestManager.RequestPath(transform.position, target.transform.position, Pursue);


            }
        }

        

        public void Pursue(Vector3 newPos, bool pathSuccesfull, bool isTarget)
        {
            if (pathSuccesfull)
            {
                if (!isTarget)
                {
                    updateLastPosition();
                    transform.Translate(new Vector3(newPos.x - positionX, newPos.y - positionY));
                    Debug.Log("New Position is: " + newPos);
                    positionX = (int)transform.position.x;
                    positionY = (int)transform.position.y;
                    updateNewPosition();
                }
                else
                {
                    target.GetComponent<PlayerScript>().takeDamage(attributes.GetStat<Attribute>(StatType.PhysDamage).StatBaseValue);
                    //Attack();
                }
            }
        }

        public void takeDamage(int damage)
        {
            var health = attributes.GetStat<Vital>(StatType.Health);
            health.StatCurrentValue -= damage;
            Debug.Log("Enemy Health is " + health.StatCurrentValue);

            if (health.StatCurrentValue == 0)
            {
                Debug.Log("You eat the delicious lizard meat of your enemy and heal five points of health");
                target.SendMessage("HealDamage", 5);
                
                Destroy(gameObject);
                
            }
        }

        public void OnDestroy()
        {
            gridHandler.setNode(positionX, positionY, true);
            gridHandler.setCharGrid(positionX, positionY, null);
           
        }
    }



}

