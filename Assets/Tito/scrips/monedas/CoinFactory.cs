using System.Collections.Generic;
using UnityEngine;

namespace Tito
{
    public class CoinFactory : MonoBehaviour
    {
        [SerializeField] private List<CoinFactoryEntry> coinPrefabs;
        private Dictionary<CoinType, GameObject> prefabDictionary;

        private void Awake()
        {
            InitializeDictionary();
        }

        private void InitializeDictionary()
        {
            if (prefabDictionary != null) return;

            prefabDictionary = new Dictionary<CoinType, GameObject>();

            if (coinPrefabs == null) return;

            foreach (CoinFactoryEntry current in coinPrefabs)
            {
                if (current != null && current.CoinPrefab != null)
                {
                    prefabDictionary[current.CoinType] = current.CoinPrefab;
                }
            }
        }

        public GameObject CreateCoin(CoinType type, Vector3 position, Quaternion rotation)
        {
            InitializeDictionary();

            if (prefabDictionary.TryGetValue(type, out GameObject prefab))
            {
                if (prefab != null)
                {
                    return Instantiate(prefab, position, rotation);
                }
            }

            Debug.LogError($"[CoinFactory] No existe un prefab en el diccionario para el tipo: {type}", this);
            return null;
        }
    }
}