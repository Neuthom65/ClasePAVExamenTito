using Unity.VisualScripting;
using UnityEngine;
namespace Tito
{

    public class ChaseState : IEnemyState
    {

        private EnemyController controller;

        public ChaseState(EnemyController controller)
        {
            this.controller = controller;
        }

        public void Enter()
        {
            if (controller.agent == null || controller.agent.enabled)
            {
                Debug.Log("El agente de navegacion no responde en chase state");
            }
            controller.agent.isStopped = false;
            controller.agent.speed = controller.speedChase;

        }

        public void Exit()
        {
            Debug.Log("Salio al estado de persecucion");
            
        }

        public void Update()
        {
            Vector3 targetPos = controller.playerRef.position;
            Vector3 dirTarget = (targetPos - controller.transform.position).normalized;
            Vector3 direction = targetPos - dirTarget;
            float range = controller.minDistanceTarget * controller.minDistanceTarget;
            float distance = direction.sqrMagnitude;

            controller.agent.SetDestination(direction);

            if (distance <= range)
            {
                Debug.Log("Estoy a rango, cambiar a persecucion");
            }
        }
    }
}
