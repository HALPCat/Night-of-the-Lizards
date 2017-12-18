using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LizardNight
{

    public class MusicSwitch : MonoBehaviour
    {
        public AudioSource _AudioSource1;
        public AudioSource _AudioSource2;
        public AudioSource _AudioSource3;
        public GridHandler gridHandler;

        public void ChangeSong()
        {
            if (gridHandler.getFloorType() == "bedroom")
            {                
                _AudioSource1.Play();
                _AudioSource2.Stop();
                _AudioSource3.Stop();
            }
            else if (gridHandler.getFloorType() == "swamp")
            {
                _AudioSource1.Stop();
                _AudioSource3.Play();
                _AudioSource2.Stop();
            }

            else
            {
                _AudioSource1.Stop();
                _AudioSource2.Play();
                _AudioSource3.Stop();
            }
        }
    }
}
