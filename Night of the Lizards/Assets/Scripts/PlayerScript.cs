using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LizardNight
{


    public class PlayerScript : CharacterBase
    {

        //This is for movement
        float timer = 0.25f;

        //the character sheet
       public BaseClass attributes;
       
      

        public int PositionX { get { return positionX; } }
        public int PositionY { get { return positionY; } }

        GameObject stairsDown;
        [SerializeField]

        protected override void Awake()
        {
            attributes = GetComponent<BaseClass>();
            initiateStats();

            base.Awake();
            CharLevel.OnCharLevelUp += OnLevelUp;

        }
        protected override void Update()
        {
            //doesn't act if not it's turn
            if (!GameMaster.GM.playersTurn)
                return;

            Move();


        }

        public void teleportToBeginning()
        {
            GameObject stairsUp;
            stairsUp = GameObject.Find("StairsUp(Clone)");
            if (stairsUp == null)
            {
                Debug.LogError("PlayerScript couldn't find StairsUp(Clone)!");
            }
            else
            {
                transform.position = new Vector3(stairsUp.transform.position.x, stairsUp.transform.position.y, 0);
                positionX = (int)transform.position.x;
                positionY = (int)transform.position.y;
                Debug.Log("Player teleported to top stairs from StairsUpScript, Player X and Y updated");
            }
        }

        protected override void Move()
        {
            //Going up
            if (Input.GetAxisRaw(VerticalAxis) > 0)
            {

                int direction = 3;

                if (!canMove(direction))
                    if (CanAttack(direction))
                    {
                        Attack(direction);
                    }
                if (canMove(direction))
                {

                    moveVertical(1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
                timer += Time.deltaTime;
                if (timer >= 0.25f && canMove(3))
                {
                    moveVertical(1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
            }

            //Going down
            else if (Input.GetAxisRaw(VerticalAxis) < 0)
            {
                int direction = 4;

                if (!canMove(direction))
                    if (CanAttack(direction))
                    {
                        Attack(direction);
                    }
                if (canMove(direction))
                {
                    moveVertical(-1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
                timer += Time.deltaTime;
                if (timer >= 0.25f && canMove(4))
                {
                    moveVertical(-1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
            }

            //Going left
            else if (Input.GetAxisRaw(HorizontalAxis) < 0)
            {
                int direction = 2;

                if (!canMove(direction))
                    if (CanAttack(direction))
                    {
                        Attack(direction);
                    }
                if (canMove(direction))
                {
                    moveHorizontal(-1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
                timer += Time.deltaTime;
                if (timer >= 0.25f && canMove(2))
                {
                    moveHorizontal(-1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
            }

            //Going right
            else if (Input.GetAxisRaw(HorizontalAxis) > 0)
            {
                int direction = 1;

                if (!canMove(direction))
                    if (CanAttack(direction))
                    {
                        Attack(direction);
                    }
                if (canMove(direction))
                {
                    moveHorizontal(1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
                timer += Time.deltaTime;
                if (timer >= 0.25f && canMove(1))
                {
                    moveHorizontal(1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
            }

            if (Input.GetButtonDown("Jump"))
            {
                stairsDown = GameObject.Find("StairsDown(Clone)");
                if (transform.position == stairsDown.transform.position)
                {
                    gridHandler.NewFloor();
                }
            }

            else if (Input.GetAxis(DiagonalUpAxis) > 0)
            {
                int direction = 5;

                if (!canMove(direction))
                    if (CanAttack(direction))
                    {
                        Attack(direction);
                    }
                if (canMove(direction))
                {
                    moveDiagonal(1, 1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
                if (timer >= 0.25f && canMove(5))
                {
                    moveDiagonal(1, 1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
            }
            else if (Input.GetAxis(DiagonalUpAxis) < 0)
            {
                int direction = 7;

                if (!canMove(direction))
                    if (CanAttack(direction))
                    {
                        Attack(direction);
                    }
                if (canMove(direction))
                {
                    moveDiagonal(-1, 1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
                if (timer >= 0.25f && canMove(7))
                {
                    moveDiagonal(-1, 1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
            }
            else if (Input.GetAxis(DiagonalDownAxis) > 0)
            {
                int direction = 6;

                if (!canMove(direction))
                    if (CanAttack(direction))
                    {
                        Attack(direction);
                    }
                if (canMove(direction))
                {
                    moveDiagonal(1, -1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
                if (timer >= 0.25f && canMove(6))
                {
                    moveDiagonal(1, -1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
            }
            else if (Input.GetAxis(DiagonalDownAxis) < 0)
            {
                int direction = 8;

                if (!canMove(direction))
                    if (CanAttack(direction))
                    {
                        Attack(direction);
                    }
                if (canMove(direction))
                {
                    moveDiagonal(-1, -1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
                if (timer >= 0.25f && canMove(8))
                {
                    moveDiagonal(-1, -1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
            }

            //else if (Input.GetAxis(Wait) >0)
            //{
            //    timer = 0;
            //    GameMaster.GM.playersTurn = false;

            //    if (timer >= 0.25f)
            //    {
            //        timer = 0;
            //        GameMaster.GM.playersTurn = false;
            //    }
            //}
        }

        protected void initiateStats()
        {
            constitution = attributes.GetStat<Attribute>(StatType.Constitution);
            strenght = attributes.GetStat<Attribute>(StatType.Strenght);
            physDamage = attributes.GetStat<Attribute>(StatType.PhysDamage);
            vitalHealth = attributes.GetStat<Vital>(StatType.Health);
            armor = attributes.GetStat<Attribute>(StatType.ArmorClass);
        }

        protected bool CanAttack(int direction)
        {
            bool isEnemy = false;

            switch (direction)
            {
                case 1:
                    isEnemy = gridHandler.getCharGrid(positionX + 1, positionY) != null ? true : false; return isEnemy; //right
                case 2:
                    isEnemy = gridHandler.getCharGrid(positionX - 1, positionY) != null ? true : false; return isEnemy; // left
                case 3:
                    isEnemy = gridHandler.getCharGrid(positionX, positionY + 1) != null ? true : false; return isEnemy; //up
                case 4:
                    isEnemy = gridHandler.getCharGrid(positionX, positionY - 1) != null ? true : false; return isEnemy; //down
                case 5:
                    isEnemy = gridHandler.getCharGrid(positionX + 1, positionY + 1) != null ? true : false; return isEnemy; //up right
                case 6:
                    isEnemy = gridHandler.getCharGrid(positionX + 1, positionY + -1) != null ? true : false; return isEnemy; //down right
                case 7:
                    isEnemy = gridHandler.getCharGrid(positionX + -1, positionY + 1) != null ? true : false; return isEnemy; //up left
                case 8:
                    isEnemy = gridHandler.getCharGrid(positionX + -1, positionY + -1) != null ? true : false; return isEnemy; //down left
            }
            //switch (direction)
            //{
            //    case 1:
            //        isEnemy = gridHandler.getCharGrid(positionX + 1, positionY).tag == "enemy" ? true : false; break; //right
            //    case 2:
            //        isEnemy = gridHandler.getCharGrid(positionX - 1, positionY).tag == "enemy" ? true : false; break; // left
            //    case 3:
            //        isEnemy = gridHandler.getCharGrid(positionX - 1, positionY).tag == "enemy" ? true : false; break; //up
            //    case 4:
            //        isEnemy = gridHandler.getCharGrid(positionX, positionY - 1).tag == "enemy" ? true : false; break; //down
            //    case 5:
            //        isEnemy = gridHandler.getCharGrid(positionX + 1, positionY + 1).tag == "enemy" ? true : false; break; //up right
            //    case 6:
            //        isEnemy = gridHandler.getCharGrid(positionX + 1, positionY + -1).tag == "enemy" ? true : false; break; //down right
            //    case 7:
            //        isEnemy = gridHandler.getCharGrid(positionX + -1, positionY + 1).tag == "enemy" ? true : false; break; //up left
            //    case 8:
            //        isEnemy = gridHandler.getCharGrid(positionX + -1, positionY + -1).tag == "enemy" ? true : false; break; //down left
            //}


            return isEnemy;
        }

        protected void Attack(int direction)
        {



            switch (direction)
            {
                case 1:
                    gridHandler.getCharGrid(positionX + 1, positionY).SendMessage("takeDamage", physDamage.StatValue); ;
                    break;
                case 2:
                    gridHandler.getCharGrid(positionX - 1, positionY).SendMessage("takeDamage", physDamage.StatValue); ;
                    break;
                case 3:
                    gridHandler.getCharGrid(positionX, positionY + 1).SendMessage("takeDamage", physDamage.StatValue); ; ; break; //up
                case 4:
                    gridHandler.getCharGrid(positionX, positionY - 1).SendMessage("takeDamage", physDamage.StatValue); ; break; //down
                case 5:
                    gridHandler.getCharGrid(positionX + 1, positionY + 1).SendMessage("takeDamage", physDamage.StatValue); break; //up right
                case 6:
                    gridHandler.getCharGrid(positionX + 1, positionY + -1).SendMessage("takeDamage", physDamage.StatValue); break; //down right
                case 7:
                    gridHandler.getCharGrid(positionX + -1, positionY + 1).SendMessage("takeDamage", physDamage.StatValue); break; //up left
                case 8:
                    gridHandler.getCharGrid(positionX + -1, positionY + -1).SendMessage("takeDamage", physDamage.StatValue); break; //down left
            }
            GameMaster.GM.playersTurn = false;
        }

        public void takeDamage(int damage)
        {
            int finalDamage = damage - armor.StatValue;
            if (finalDamage < 1)
                finalDamage = 1;
            vitalHealth.StatCurrentValue -= finalDamage;
            Debug.Log("Player Health is " + vitalHealth.StatCurrentValue);

            if (vitalHealth.StatCurrentValue == 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene(0);
            }
        }

        public int GetCurrentHealth()
        {
            return vitalHealth.StatCurrentValue;
        }

        public int GetMaxHealth()
        {
            var health = attributes.GetStat<Vital>(StatType.Health);

            return health.StatBaseValue;
        }

        public void HealDamage(int damage)
        {
            vitalHealth.StatCurrentValue += damage;
            Debug.Log("Health is " + vitalHealth.StatCurrentValue);

        }

        public void OnLevelUp(object sender, LevelChangeEventArgs args)
        {
            LevelUp();
        }

        protected void LevelUp()
        {

            constitution.ScaleStat(CharLevel.Level);
            strenght.ScaleStat(CharLevel.Level);

            vitalHealth.SetCurrentValueToMax();
            Debug.Log("Level up");
        }

        public void AttackPowerUp(float damageBonus)
        {
            physDamage.AddModifier(new StatModTotalAdd(damageBonus + physDamage.StatModifierValue));
            physDamage.UpdateModifiers();

        }

        public void DefensePowerUp(float armorLevel)
        {
            armor.AddModifier(new StatModTotalAdd(armorLevel + armor.StatModifierValue));
            armor.UpdateModifiers();

        }
    }
}