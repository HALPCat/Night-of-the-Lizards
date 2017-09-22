using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{


    public class PlayeScript : CharacterBase
    {
             
        //This is for movement
        float timer = 0;



        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //doesn't act if not it's turn
            if (!GameMaster.GM.playersTurn)
                return;
            
            //Lets keep positions updated
            positionX = (int)transform.position.x;
            positionY = (int)transform.position.y;
                       
            Move();
            
        }

        protected override void Move()
        {
            //Going up
            if (Input.GetAxisRaw(VerticalAxis) > 0)
            {
                //This is spaghetti and should be tied to the button name rather than actual keys
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    moveVertical(1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
                timer += Time.deltaTime;
                if (timer >= 0.25f)
                {
                    moveVertical(1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
            }

            //Going down
            else if (Input.GetAxisRaw(VerticalAxis) < 0)
            {
                //This is spaghetti and should be tied to the button name rather than actual keys
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    moveVertical(-1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
                timer += Time.deltaTime;
                if (timer >= 0.25f)
                {
                    moveVertical(-1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
            }

            //Going left
            else if (Input.GetAxisRaw(HorizontalAxis) < 0)
            {
                //This is spaghetti and should be tied to the button name rather than actual keys
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    moveHorizontal(-1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
                timer += Time.deltaTime;
                if (timer >= 0.25f)
                {
                    moveHorizontal(-1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
            }

            //Going right
            else if (Input.GetAxisRaw(HorizontalAxis) > 0)
            {
                //This is spaghetti and should be tied to the button name rather than actual keys
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    moveHorizontal(1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
                timer += Time.deltaTime;
                if (timer >= 0.25f)
                {
                    moveHorizontal(1);
                    timer = 0;
                    GameMaster.GM.playersTurn = false;
                }
            }
        }

     

        private void theEnd()
        {
            if (health <= 0)
                GameMaster.GM.GameOver();
        }

       
        
    }
}