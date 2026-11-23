using System.Collections.Generic;
using UnityEngine;

namespace Tito
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private CoinPool coinPool;
        [SerializeField] private CoinType coinType;
        [SerializeField] private List<Transform> wayPoints;

        public void Spawn()
        {
            foreach (Transform current in wayPoints)
            {
                CoinPool.InstanceCoinPool.GetCoin(coinType, current.position, current.rotation);
            }
        }

        public void Despawn()
        {
            CoinPool.InstanceCoinPool.ReturnAllCoins(coinType);
        }
    }
}