using UnityEngine;

namespace Tito
{
    public class ActiveSpawnRoom : MonoBehaviour
    {
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private bool active;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                active = true;
                enemySpawner.Spawn();
            }
        }
    }
}