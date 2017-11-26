using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LizardNight
{
    public class DungeonFloorText : MonoBehaviour
    {

        Text dungeonFloorText;
        GridHandler gridHandler;

        // Use this for initialization
        void Start()
        {
            gridHandler = GameObject.Find("Grid System").GetComponent<GridHandler>();
            dungeonFloorText = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            if(gridHandler.getDungeonFloor() == 0) {
                dungeonFloorText.text = "Tutorial Floor";
            }else {
                dungeonFloorText.text = "Dungeon Floor: " + gridHandler.getDungeonFloor();
            }
        }
    }
}
