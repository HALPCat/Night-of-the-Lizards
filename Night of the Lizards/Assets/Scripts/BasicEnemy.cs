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

        public float dropRate;

        public GameObject lifePowerup, atackPowerUp, defensePowerUp;
        int _level;

        PlayerScript target;
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
            set { _stationary = value; }
        }

        protected override void Awake()
        {
            attributes = GetComponent<EnemyOne>();

            //base.Awake();
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

        public void InitializeAttributes(int level)
        {
            constitution = attributes.GetStat<Attribute>(StatType.Constitution);
            strenght = attributes.GetStat<Attribute>(StatType.Strenght);
            physDamage = attributes.GetStat<Attribute>(StatType.PhysDamage);
            vitalHealth = attributes.GetStat<Vital>(StatType.Health);

            SetLevel(level);
        }

        //used to set enemy level, on creation!!
        public void SetLevel(int level)
        {
            _level = level;
            constitution.ScaleStat(level - 1);
            strenght.ScaleStat(level - 1);

            vitalHealth.SetCurrentValueToMax();
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

        //old bugged move
        protected override void Move()
        {

            //if not triggered, will move randomly
            int dir = UnityEngine.Random.Range(1, 8);

            SpriteRenderer sr = GetComponent<SpriteRenderer>();

            if (canMove(dir))
            {
                switch (dir)
                {
                    case 1:
                        moveHorizontal(1);
                        sr.flipX = false;
                        break;
                    case 2:
                        moveHorizontal(-1);
                        sr.flipX = true;
                        break;
                    case 3:
                        moveVertical(1);
                        break;
                    case 4:
                        moveVertical(-1);
                        break;
                    case 5:
                        moveDiagonal(1, 1);
                        sr.flipX = false;
                        break;
                    case 6:
                        moveDiagonal(1, -1);
                        sr.flipX = false;
                        break;
                    case 7:
                        moveDiagonal(-1, 1);
                        sr.flipX = true;
                        break;
                    case 8:
                        moveDiagonal(-1, -1);
                        sr.flipX = true;
                        break;


                }
            }


        }

        void OnTriggerEnter2D(Collider2D col)
        {

            if (col.tag == "Player")
            {
                triggered = true;
                target = col.gameObject.GetComponent<PlayerScript>();

            }


        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!attributes.mobile)
            {
                triggered = false;
            }
        }

        public void Movement()
        {
            if (attributes.mobile)
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

            else
            {
                if (triggered)
                {
                    target.GetComponent<PlayerScript>().takeDamage(physDamage.StatValue);
                }

            }

            if (attributes.isBoss)
            {
                _stationary = true;
            }

        }




        public void Pursue(Vector3 newPos, bool pathSuccesfull, bool isTarget)
        {
            if (pathSuccesfull)
            {
                if (!isTarget)
                {
                    updateLastPosition();
                    Vector3 movementVector = new Vector3(newPos.x - positionX, newPos.y - positionY);
                    transform.Translate(movementVector);
                    //Animation stuff
                    SpriteRenderer sr = GetComponent<SpriteRenderer>();
                    if (movementVector.x < 0)
                    {
                        sr.flipX = true;
                    }
                    else
                    {
                        sr.flipX = false;
                    }

                    //Debug.Log("New Position is: " + newPos);
                    positionX = (int)transform.position.x;
                    positionY = (int)transform.position.y;
                    updateNewPosition();
                }
                else
                {
                    //Animation
                    Vector3 movementVector = new Vector3(newPos.x - positionX, newPos.y - positionY);
                    SpriteRenderer sr = GetComponent<SpriteRenderer>();
                    if (movementVector.x < 0)
                    {
                        sr.flipX = true;
                    }
                    else
                    {
                        sr.flipX = false;
                    }

                    //Attack
                    target.GetComponent<PlayerScript>().takeDamage(physDamage.StatValue);
                }
            }

        }
        public void takeDamage(int damage)
        {

            vitalHealth.StatCurrentValue -= damage;
            Debug.Log("Enemy Health is " + vitalHealth.StatCurrentValue);

            //Audio
            Instantiate(Resources.Load("HitSoundPlayer"));

            if (vitalHealth.StatCurrentValue == 0)
            {
                target.AddKill();
                target.CharLevel.ModifyExp(20 + 5 * _level);
                Drop();
                Destroy(gameObject);
            }
        }

        private void Drop()
        {
            if (attributes.isBoss)
            {
                Instantiate(lifePowerup, transform.position - new Vector3(0, 0.5f, 0), transform.rotation);
                Instantiate(atackPowerUp, transform.position - new Vector3(1, 0.5f, 0), transform.rotation);
                Instantiate(defensePowerUp, transform.position - new Vector3(-1, 0.5f, 0), transform.rotation);

            }
            else
            {
                float dropChance = UnityEngine.Random.Range(0.0f, 1);
                if (dropChance < dropRate)
                {
                    int drop = UnityEngine.Random.Range(0, 6);
                    if (drop <= 3)
                        Instantiate(lifePowerup, transform.position - new Vector3(0, 0.5f, 0), transform.rotation);
                    if (drop == 4)
                        Instantiate(atackPowerUp, transform.position - new Vector3(0, 0.5f, 0), transform.rotation);
                    if (drop == 5)
                        Instantiate(defensePowerUp, transform.position - new Vector3(0, 0.5f, 0), transform.rotation);
                }

            }
        }

        public void OnDestroy()
        {

            gridHandler.setNode(positionX, positionY, true);
            gridHandler.setCharGrid(positionX, positionY, null);

        }
    }



}

