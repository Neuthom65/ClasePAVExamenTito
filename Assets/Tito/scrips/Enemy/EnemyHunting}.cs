
using UnityEngine;
using UnityEngine.AI;

namespace Tito
{
    public class EnemyHunting : MonoBehaviour
    {
        [SerializeField] private float distanciaActualizacion = 0.2f;

        private NavMeshAgent agent;
        private Transform player;
        private float tiempo;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            GameObject jugador = GameObject.FindGameObjectWithTag("Player");

            if (jugador != null)
                player = jugador.transform;
        }

        private void Update()
        {
            if (player == null || agent == null || !agent.isOnNavMesh)
                return;

            tiempo += Time.deltaTime;

            if (tiempo >= distanciaActualizacion)
            {
                agent.SetDestination(player.position);
                tiempo = 0f;
            }
        }
    }
}
