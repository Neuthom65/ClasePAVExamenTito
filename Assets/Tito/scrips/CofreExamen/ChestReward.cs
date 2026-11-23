
using UnityEngine;

namespace Tito
{
    public class ChestReward : MonoBehaviour
    {
        [Header("Recompensa")]
        [SerializeField] private GameObject rewardPrefab;
        [SerializeField] private Transform spawnPoint;

        private bool spawned;

        public void SpawnReward()
        {
            if (spawned || rewardPrefab == null)
                return;

            spawned = true;

            Vector3 position = spawnPoint != null
                ? spawnPoint.position
                : transform.position + Vector3.up;

            Quaternion rotation = spawnPoint != null
                ? spawnPoint.rotation
                : Quaternion.identity;

            Instantiate(rewardPrefab, position, rotation);
        }
    }
}
