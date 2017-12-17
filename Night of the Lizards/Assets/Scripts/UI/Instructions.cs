using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{
    public class Instructions : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
