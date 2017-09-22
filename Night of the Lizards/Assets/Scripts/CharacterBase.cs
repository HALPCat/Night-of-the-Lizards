using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{

    public abstract class CharacterBase : MonoBehaviour
    {
        public const string HorizontalAxis = "Horizontal";
        public const string VerticalAxis = "Vertical";
        
        //just for testing
        protected int health = 3;

        //Poisition variables
        protected int positionX;
        protected int positionY;

        //For checking what's on the map
        public GridHandler gridHandler;

 
        // Use this for initialization
        protected virtual void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        protected virtual void Awake()
        {

        }

        protected abstract void Move();

       protected void moveVertical(int tiles)
        {
            //Move to new position if it is not a wall
            if (!gridHandler.getGrid(positionX, positionY + tiles).name.Equals("Wall"))
            {
                transform.Translate(new Vector3(0, tiles));

            }
            else
            {
                Debug.Log("Couldn't move");
            }
        }
       protected void moveHorizontal(int tiles)
        {
            //Move to new position if it is not a wall
            if (!gridHandler.getGrid(positionX + tiles, positionY).name.Equals("Wall"))
            {
                transform.Translate(new Vector3(tiles, 0));
                
            }
            else
            {
                Debug.Log("Couldn't move");
            }
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