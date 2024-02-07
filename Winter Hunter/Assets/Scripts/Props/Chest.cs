using System;
using UnityEngine;

namespace Props
{
    public class Chest : MonoBehaviour
    {
        public bool canOpen;
        
        public virtual void PickUp()
        {
            Destroy(gameObject);
        }
    }
}
