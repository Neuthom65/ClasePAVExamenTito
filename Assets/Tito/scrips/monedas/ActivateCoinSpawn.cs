using UnityEngine;

namespace Tito
{
    public class ActivateCoinSpawn : MonoBehaviour
    {
        [SerializeField] private CoinSpawner coinSpawner;
        [SerializeField] private bool active;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                active = true;
                coinSpawner.Spawn();
            }
        }
    }
}