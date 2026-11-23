using System.Collections.Generic;
using UnityEngine;

namespace Tito
{
    public class EnemySpawner : MonoBehaviour
    {
        //[SerializeField] private Enemypool enemyPool;
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private List<Transform> wayPoints;

        public void Spawn()
        {
            foreach (Transform current in wayPoints)
            {
                Enemypool.InstanceEnemyPool.GetEnemy(enemyType, current.position, current.rotation);
            }
        }
    }
}