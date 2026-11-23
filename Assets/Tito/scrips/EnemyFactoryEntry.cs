using System;
using System.Runtime.CompilerServices;
using UnityEngine;
namespace Tito
{
    [Serializable]
    public class EnemyFactoryEntry
    {
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private GameObject enemyPrefab;

        public EnemyType EnemyType { get => enemyType; }
        public GameObject EnemyPrefab { get => enemyPrefab; }
    }
}
