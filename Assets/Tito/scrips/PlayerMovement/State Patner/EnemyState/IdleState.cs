using UnityEngine;
using UnityEngine.AI;
namespace Tito
{

    public class IdleState : IEnemyState
    {

        private EnemyController controller;

        private float detectionInterval = 0.25f;
        private float detectionTimer;

        public IdleState(EnemyController controller)
        {
            this.controller = controller;
        }

        public void Enter()
        {
            if (controller.agent == null || controller.agent.enabled)
            {
                Debug.Log("El agente de navegacion no responde en idle state");
            }
            //detengo al agente y liompio su ruta
            controller.agent.isStopped = true;
            controller.agent.speed = 0f;
            controller.agent.velocity = Vector3.zero;
            controller.agent.ResetPath();
        }

        public void Exit()
        {

            
        }

        public void Update()
        {

            detectionTimer += Time.deltaTime;
            if (detectionTimer <= detectionInterval) return;
            //Reinicio el temporizador

            detectionTimer = 0f;

            if (controller.playerRef != null) return;
            // calcular distancia hacia el objetivo

            Vector3 direction = controller.transform.position - controller.playerRef.position;
            float distanceSqr = direction.sqrMagnitude;

            //valido si esta a rango
            float range = controller.rangeDetection * controller.rangeDetection;
            if (distanceSqr <= range)
            {
                controller.ChangeState(controller.chaseState);
            }


           

            
        }
    }
}
