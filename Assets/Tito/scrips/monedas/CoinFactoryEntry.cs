using System;
using UnityEngine;
namespace Tito
{
    [Serializable]
    public class CoinFactoryEntry
    {
        [SerializeField] private CoinType coinType;
        [SerializeField] private GameObject coinPrefab;

        public CoinType CoinType { get => coinType;  }
        public GameObject CoinPrefab { get => coinPrefab; }
    }
}
