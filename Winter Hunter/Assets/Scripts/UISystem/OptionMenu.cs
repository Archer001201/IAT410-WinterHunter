using System;
using UnityEngine;

namespace UISystem
{
    /*
     * Control option menu
     */
    public class OptionMenu : MonoBehaviour
    {
        private void OnEnable()
        {
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }
    }
}
