using ClaseProgAvanzada;
using JetBrains.Annotations;
using UnityEngine;
namespace Tito
{

    public class PlayerAnim : MonoBehaviour
    {
        [Header("Controlador de animacion")]
        [SerializeField] protected Animator animController;
        [SerializeField] private PlayerMovement playerMovement;

        private bool activeJump = false;

        public void UpdateAnim(bool onGround, Vector2 dirInput)
        {
            if (animController == null) return;
            float currentSpeed = Mathf.Clamp01(dirInput.magnitude);
            animController.SetBool("OnGround", onGround);
            animController.SetFloat("Speed", currentSpeed);
            animController.SetFloat("Vertical", dirInput.y, 0.1f, Time.deltaTime);
            animController.SetFloat("Turning Direction", dirInput.x, 0.1f, Time.deltaTime);


        }

        public void Jump()
        {
            if (activeJump) return;
            activeJump = true;
            animController.SetTrigger("Jump");


        }
        public void JumpEvent()
        {
            playerMovement.JumpEvent();
        }
        public void JumpEndEvent()
        {
            activeJump = false;
        }
    }
}
