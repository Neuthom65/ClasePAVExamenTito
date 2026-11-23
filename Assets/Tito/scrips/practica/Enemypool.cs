using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Tito
{
    public class Enemypool : MonoBehaviour
    {
        [SerializeField] private EnemyFactory factory;
        [SerializeField] private int capacity = 6;
        [SerializeField] private int size = 6;

        private Dictionary<EnemyType, ObjectPool<Enemy>> pools;

        public static Enemypool InstanceEnemyPool { private set; get; }

        private void Awake()
        {
            if (InstanceEnemyPool == null)
            {
                InstanceEnemyPool = this;
                pools = new Dictionary<EnemyType, ObjectPool<Enemy>>();
                return;
            }
            Destroy(gameObject);
        }

        private ObjectPool<Enemy> CrearPool(EnemyType type)
        {
            return new ObjectPool<Enemy>(
                createFunc: () =>
                {
                    GameObject obj = factory.CreateEnemy(type, Vector3.zero, Quaternion.identity);
                    obj.transform.SetParent(transform);
                    Enemy enemy = obj.GetComponent<Enemy>();
                    if (enemy == null) enemy = obj.AddComponent<Enemy>();
                    return enemy;
                },
                actionOnGet: enemy => enemy.gameObject.SetActive(true),
                actionOnRelease: enemy => enemy.gameObject.SetActive(false),
                actionOnDestroy: enemy => Destroy(enemy.gameObject),
                collectionCheck: false,
                defaultCapacity: capacity,
                maxSize: size
            );
        }

        public Enemy GetEnemy(EnemyType type, Vector3 position, Quaternion rotation)
        {
            if (!pools.ContainsKey(type))
            {
                pools.Add(type, CrearPool(type));
            }

            if (pools[type].CountActive >= size)
            {
                return null;
            }

            Enemy enemy = pools[type].Get();
            enemy.Initialize(type, this, position, rotation);
            return enemy;
        }

        public void ReturnEnemy(Enemy enemy)
        {
            if (pools.ContainsKey(enemy.EnemyType))
            {
                pools[enemy.EnemyType].Release(enemy);
            }
        }
    }
}