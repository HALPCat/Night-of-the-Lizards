using System.Collections;
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
        PlayerScript target;
        int targetY, targetX;
        [SerializeField]
        protected bool _stationary;
        
        public bool stationary 
        {
            get {return _stationary; }
        }

        // Use this for initialization
        protected override void Start()
        {
            //adds enemy to the game master list
            //GameMaster.GM.AddEnemy(this);
           

            base.Start();
            
        }

        // Update is called once per frame
       protected override void Update()
        {
            

          
        }

        void OnBecameInvisible() {
            Debug.Log(this.name + " is now invisible, disabling.");
            GetComponent<BasicEnemy>().enabled = false;
            GameMaster.GM.RemoveEnemy(this);
        }

        void OnBecameVisible() {
            Debug.Log(this.name + " is now visible, enabling.");
            GetComponent<BasicEnemy>().enabled = true;
            GameMaster.GM.AddEnemy(this);
        }

        protected override void Move()
        {
            throw new NotImplementedException();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("entered trigger");
            if (col.tag == "Player")
            {
                triggered = true;
                target = col.GetComponent<PlayerScript>();
            }
            

        }
        public void Movement()
        {
            //Don't do anything if disabled
            if (!gameObject.GetComponent<BasicEnemy>().enabled) {
                return;
            }

            if (!triggered)
                {
                    //if not triggered, will move randomly
                    int dir = UnityEngine.Random.Range(1, 5);

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

                    }
                }
                //if triggered by player will pursue
                else
                {
                    if (Mathf.Abs(target.GetX - transform.position.x) < float.Epsilon)
                    {
                        targetY = target.GetY > transform.position.y ? 1 : -1;
                        moveVertical(targetY);
                    }

                    else
                    {
                        targetX = target.GetX > transform.position.x ? 1 : -1;
                        moveHorizontal(targetX);
                    }

                }
            }
        }
                    


            
           
        

        
     
    }

