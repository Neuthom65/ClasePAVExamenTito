
using UnityEngine;

namespace Tito
{
    [RequireComponent(typeof(Collider))]
    public class Key : MonoBehaviour
    {
        private bool collected;

        private void OnTriggerEnter(Collider other)
        {
            if (collected)
                return;

            PlayerInventory inventory =
                other.GetComponent<PlayerInventory>();

            if (inventory == null)
                inventory = other.GetComponentInParent<PlayerInventory>();

            if (inventory == null)
                inventory = other.GetComponentInChildren<PlayerInventory>();

            if (inventory != null)
            {
                inventory.AddKey();
                collected = true;

                Debug.Log("Llave recogida");

                Destroy(gameObject);
            }
        }
    }
}
