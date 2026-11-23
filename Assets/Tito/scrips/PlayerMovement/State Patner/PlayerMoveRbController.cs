using UnityEngine;

namespace ClaseProgAvanzada
{

    // Componente requerido para el funcionamiento del script
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]

    public class PlayerMoveRbController : PlayerMovement
    {

        // Referencia del componente de fisicass
        private Rigidbody rb;



        // Ciclo de vida de Unity, se emplea para gestionar y asignar componentes
        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody>();
        }


        // Permite gestionar la gravedad del personaje
        protected override void HandleGravity()
        {
            // Cuando salta
            if (onGround && jump)
            {

                playerAnim.Jump();
            }
            dirMove *= speedMove;
            dirMove.y = 0f;
        }


        // Se llama 50 veces x seg.
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            MovePlayer();
            ConsumeEvent();
        }


        // Permite mover el controlador del personaje
        protected override void MovePlayer()
        {
            Vector3 velocity = rb.linearVelocity;
            velocity.x = dirMove.x;
            velocity.z = dirMove.z;
            rb.linearVelocity = velocity;
        }
        public override void JumpEvent()
        {
            Vector3 velocity = rb.linearVelocity;
            velocity.x = dirMove.x;
            velocity.z = dirMove.z;
            velocity.y = 0;
            rb.linearVelocity = velocity;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            onGround = false;
        }


        // Permite obtener el movimiento del personaje
        protected override Vector3 GetMoveComponent => new(0f, rb.linearVelocity.y, 0f);
    }
}