using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Tito
{
    public class Coin : MonoBehaviour
    {
        private CoinType coinType;
        private CoinPool pool;

        public CoinType CoinType => coinType;

        public void Initialize(CoinType type, CoinPool pool, Vector3 position, Quaternion rotation)
        {
            this.coinType = type;
            this.pool = pool;
            transform.SetPositionAndRotation(position, rotation);
            gameObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CoinPool targetPool = pool != null ? pool : CoinPool.InstanceCoinPool;
                if (targetPool != null)
                {
                    targetPool.ReturnCoin(this);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}