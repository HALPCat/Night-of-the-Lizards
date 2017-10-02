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
        [SerializeField]
        protected int positionX;
        [SerializeField]
        protected int positionY;

        //position last turn
        protected int lastX;
        protected int lastY;

        //For checking what's on the map
        public GridHandler gridHandler;


 
        // Use this for initialization
        protected virtual void Start()
        {
            positionX = (int)transform.position.x;
            positionY = (int)transform.position.y;
            gridHandler.setNode(positionX, positionY, false);

        }

        // Update is called once per frame
       protected virtual void Update()
        {
            //Lets keep positions updated
            
           
        }

        protected virtual void Awake()
        {
            
            

        }

        protected abstract void Move();

       protected void moveVertical(int tiles)
        {
            //Move to new position if it is not a wall
            if (gridHandler.getWalkable(positionX, positionY + tiles))
            {
                updateLastPosition();
                transform.Translate(new Vector3(0, tiles));
                positionY = (int)transform.position.y;
                updateNewPosition();

            }
            else
            {
                Debug.Log("Couldn't move");
            }
        }
       protected void moveHorizontal(int tiles)
        {
            //Move to new position if it is not a wall
            if (gridHandler.getWalkable(positionX + tiles, positionY))
            {
                updateLastPosition();
                transform.Translate(new Vector3(tiles, 0));
                positionX = (int)transform.position.x;
                updateNewPosition();               

            }
            else
            {
                Debug.Log("Couldn't move");
            }
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