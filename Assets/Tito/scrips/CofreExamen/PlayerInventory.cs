
using UnityEngine;

namespace Tito
{
    public class PlayerInventory : MonoBehaviour
    {
        public bool HasKey { get; private set; }

        public void AddKey()
        {
            HasKey = true;
        }

        public bool UseKey()
        {
            if (!HasKey)
                return false;

            HasKey = false;
            return true;
        }
    }
}
