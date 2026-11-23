using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Tito
{
    public class CoinPool : MonoBehaviour
    {
        [SerializeField] private CoinFactory factory;
        [SerializeField] private int capacity = 4;
        [SerializeField] private int size = 12;

        private Dictionary<CoinType, ObjectPool<Coin>> pools;
        private List<Coin> activeList = new List<Coin>();

        public static CoinPool InstanceCoinPool { private set; get; }

        private void Awake()
        {
            if (InstanceCoinPool == null)
            {
                InstanceCoinPool = this;
                pools = new Dictionary<CoinType, ObjectPool<Coin>>();
                if (factory == null)
                {
                    factory = GetComponent<CoinFactory>();
                    if (factory == null)
                    {
                        factory = FindAnyObjectByType<CoinFactory>();
                    }
                }
                return;
            }
            Destroy(gameObject);
        }
        // ==========================================

        private ObjectPool<Coin> CrearPool(CoinType type)
        {
            return new ObjectPool<Coin>(
                createFunc: () =>
                {
                    if (factory == null)
                    {
                        factory = GetComponent<CoinFactory>();
                        if (factory == null) factory = FindAnyObjectByType<CoinFactory>();
                    }

                    if (factory == null)
                    {
                        Debug.LogError("[CoinPool] No se encontró ninguna CoinFactory para crear monedas.", this);
                        return null;
                    }

                    GameObject obj = factory.CreateCoin(type, Vector3.zero, Quaternion.identity);
                    if (obj == null)
                    {
                        Debug.LogError($"[CoinPool] CoinFactory no pudo crear un objeto para el tipo {type}.", this);
                        return null;
                    }

                    obj.transform.SetParent(transform);
                    Coin coin = obj.GetComponent<Coin>();
                    if (coin == null) coin = obj.AddComponent<Coin>();
                    return coin;
                },
                actionOnGet: coin =>
                {
                    if (coin != null) coin.gameObject.SetActive(true);
                },
                actionOnRelease: coin =>
                {
                    if (coin != null) coin.gameObject.SetActive(false);
                },
                actionOnDestroy: coin =>
                {
                    if (coin != null) Destroy(coin.gameObject);
                },
                collectionCheck: false,
                defaultCapacity: capacity,
                maxSize: size
            );
        }

        public Coin GetCoin(CoinType type, Vector3 position, Quaternion rotation)
        {
            if (pools == null)
            {
                pools = new Dictionary<CoinType, ObjectPool<Coin>>();
            }

            if (!pools.ContainsKey(type))
            {
                pools.Add(type, CrearPool(type));
            }

            if (pools[type].CountActive >= size)
            {
                return null;
            }

            Coin coin = pools[type].Get();
            if (coin != null)
            {
                coin.Initialize(type, this, position, rotation);
                if (!activeList.Contains(coin)) activeList.Add(coin);
            }
            return coin;
        }

        public void ReturnCoin(Coin coin)
        {
            if (coin == null) return;

            if (pools != null && pools.ContainsKey(coin.CoinType) && coin.gameObject.activeInHierarchy)
            {
                pools[coin.CoinType].Release(coin);
            }
        }

        public void ReturnAllCoins(CoinType type)
        {
            for (int i = 0; i < activeList.Count; i++)
            {
                Coin coin = activeList[i];
                if (coin != null && coin.CoinType == type && coin.gameObject.activeInHierarchy)
                {
                    ReturnCoin(coin);
                }
            }
        }
    }
}