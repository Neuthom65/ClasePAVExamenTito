using UnityEngine;
using UnityEngine.AI;
namespace Tito
{

    public class EnemyController : MonoBehaviour
    {
        public Transform playerRef;
        public float rangeDetection = 10f;
        public float speedChase = 4.0f;
        public float minDistanceTarget = 2.0f;
        public float maxDistanceTarget = 3.0f;


        private IEnemyState currentState;


        //Estados

        public  IdleState idleState;
        public ChaseState chaseState;
        public NavMeshAgent agent;

        public void Start()
        {
            //Aqui creo objetos de estados

            idleState = new IdleState(this);
            chaseState = new ChaseState(this);
            currentState = idleState;

            ChangeState(idleState);
        }

        public void Update()
        {
            currentState?.Update();
        }

        public void ChangeState(IEnemyState newState)
        {
            if(newState == null || newState == currentState) return;
            currentState?.Exit();
            currentState = newState; //nuevo estado
            currentState.Enter();
        }




    }
}
