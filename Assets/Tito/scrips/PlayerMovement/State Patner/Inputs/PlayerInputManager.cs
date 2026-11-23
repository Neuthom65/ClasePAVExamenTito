using UnityEngine;
namespace Tito
{

    public class PlayerInputManager
    {
        private static PlayerMoveControl inputActions;

        public static PlayerMoveControl GetInputActions()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerMoveControl();
                inputActions.Enable();
            }
            return inputActions;
        }
        public static void EnablePlayer()
        {
            GetInputActions().character.Enable();
        }
        public static void DisablePlayer()
        {
            GetInputActions().character.Disable();
        }


        public static void clear()
        {
            inputActions = null;
        }
    }
}
