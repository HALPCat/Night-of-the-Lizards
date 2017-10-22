using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{


    public class PlayerScript : CharacterBase
    {

        //This is for movement
        float timer = 0.25f;


        public int PositionX { get { return positionX; } }
        public int PositionY { get { return positionY; } }


        // Update is called once per frame
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
                //This is spaghetti and should be tied to the button name rather than actual keys
                if (canMove(3))
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
                //This is spaghetti and should be tied to the button name rather than actual keys
                if (canMove(4))
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
                //This is spaghetti and should be tied to the button name rather than actual keys
                if (canMove(2))
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
                //This is spaghetti and should be tied to the button name rather than actual keys
                if (canMove(1))
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

            else if (Input.GetAxis(DiagonalUpAxis) > 0)
            {
                if (canMove(5))
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
                if (canMove(7))
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
                if (canMove(6))
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
                if (canMove(8))
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
        }
        

        private void theEnd()
        {
            if (health <= 0)
                GameMaster.GM.GameOver();
        }



    }
}