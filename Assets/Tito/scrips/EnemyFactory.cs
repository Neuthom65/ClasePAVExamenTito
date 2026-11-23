using System.Collections.Generic;
using UnityEngine;

namespace Tito
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private List<EnemyFactoryEntry> enemyPrefabs;
        private Dictionary<EnemyType, GameObject> prefabDictionary;

        private void Awake()
        {
            prefabDictionary = new Dictionary<EnemyType, GameObject>();

            foreach (EnemyFactoryEntry current in enemyPrefabs)
            {
                prefabDictionary.Add(current.EnemyType, current.EnemyPrefab);
            }
        }

        public GameObject CreateEnemy(EnemyType type, Vector3 position, Quaternion rotation)
        {
            if (prefabDictionary.TryGetValue(type, out GameObject prefab))
            {
                return Instantiate(prefab, position, rotation);
            }

            return null;
        }
    }
}