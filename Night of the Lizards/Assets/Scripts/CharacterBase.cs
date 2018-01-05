using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{

    public abstract class CharacterBase : MonoBehaviour
    {
        public const string HorizontalAxis = "Horizontal";
        public const string VerticalAxis = "Vertical";
        public const string DiagonalUpAxis = "Diagonal Up";
        public const string DiagonalDownAxis = "Diagonal Down";
        public const string Wait = "Wait";

        Animator anim;

        protected Attribute constitution, strenght, physDamage, armor;
        protected Vital vitalHealth;

        //Poisition variables
        [SerializeField]
        protected int positionX;
        [SerializeField]
        protected int positionY;

        //position last turn
        protected int lastX;
        protected int lastY;

        //For checking what's on the map
        public GridHandler gridHandler;

        //character level controller!
        private CharLevel _charLevel;
        

        public CharLevel CharLevel
        {
            get { return _charLevel; }
            set { _charLevel = value; }
        }

        // Use this for initialization
        protected virtual void Start()
        {
            anim = GetComponent<Animator>();

            if (gridHandler == null)
            {
                Debug.Log(this.name + " gridHandler is null, finding a GridHandler");
                gridHandler = GameObject.Find("Grid System").GetComponent<GridHandler>();
            }
            if (gridHandler == null)
            {
                Debug.LogError(this.name + " couldn't find a GridHandler!");
            }
            positionX = (int)transform.position.x;
            positionY = (int)transform.position.y;
            Debug.Log("character X and Y set to actual X and Y from CharacterBase");
            //Debug.Log(positionX + positionY);
            gridHandler.setNode(positionX, positionY, false);
            updateNewPosition();

        }

        protected virtual void Awake()
        {
            

            if (CharLevel == null)
            {
                CharLevel = GetComponent<CharLevel>();
                if (CharLevel == null)
                {
                    Debug.LogWarning("No CharLevel assigned to Character");
                }
            }

                            
        }

        protected abstract void Update();

        protected abstract void Move();

        protected bool canMove(int direction)
        {
            bool isFree = false;
            switch (direction)
            {
                case 1:
                    isFree = gridHandler.getWalkable(positionX + 1, positionY); break; //right
                case 2:
                    isFree = gridHandler.getWalkable(positionX - 1 , positionY); break; // left
                case 3:
                    isFree = gridHandler.getWalkable(positionX, positionY + 1); break; //up
                case 4:
                    isFree = gridHandler.getWalkable(positionX, positionY - 1); break; //down
                case 5:
                    isFree = gridHandler.getWalkable(positionX + 1, positionY + 1); break; //up right
                case 6:
                    isFree = gridHandler.getWalkable(positionX + 1, positionY - 1); break; //down right
                case 7:
                    isFree = gridHandler.getWalkable(positionX - 1, positionY + 1); break; //up left
                case 8:
                    isFree = gridHandler.getWalkable(positionX - 1, positionY - 1); break; //down left
            }

           
            return isFree;
        }

        protected void moveVertical(int tiles)
        {
            //Animation
            anim.Play("Move", 0);

            updateLastPosition();
            transform.Translate(new Vector3(0, tiles));
            positionY = (int)transform.position.y;
            updateNewPosition();

        }
        protected void moveHorizontal(int tiles)
        {
            //For flipping sprite and animations
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (tiles < 0) {
                sr.flipX = true;
            } else {
                sr.flipX = false;
            }
            anim.Play("Move", 0);

            updateLastPosition();
            transform.Translate(new Vector3(tiles, 0));
            positionX = (int)transform.position.x;
            updateNewPosition();


        }

        protected void moveDiagonal(int tileX, int tileY)
        {
            //For flipping sprite and animations
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (tileX < 0) {
                sr.flipX = true;
            } else {
                sr.flipX = false;
            }
            anim.Play("Move", 0);

            updateLastPosition();
            transform.Translate(new Vector3(tileX, tileY));
            positionX = (int)transform.position.x;
            positionY = (int)transform.position.y;
            updateNewPosition();

        }

        protected void updateLastPosition()
        {
            gridHandler.setCharGrid(positionX, positionY, null);
            gridHandler.setNode(positionX, positionY, true);
            lastY = positionY;
            lastX = positionX;
        }
        protected void updateNewPosition()
        {
            gridHandler.setCharGrid(positionX, positionY, this.gameObject);
            gridHandler.setNode(positionX, positionY, false);
           
        }

        public int GetX
        {
            get { return positionX; }
        }

        public int GetY
        {
            get { return positionY; }
        }

        
    }
}