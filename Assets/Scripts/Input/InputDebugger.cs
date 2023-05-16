using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SideScroll.Input
{
    public class InputDebugger : MonoBehaviour
    {
        private StringBuilder Logger => logger ??= new StringBuilder();

        private StringBuilder logger = null;

        private void Start()
        {
            // InputSystem.onDeviceChange += LogCurrentDevice;
        }

        #region InGame Events

        public void OnAttack(InputAction.CallbackContext ctx)
        {
            if (ctx.phase != InputActionPhase.Started) return;
            LogMessage("Attack");
        }

        public void OnSkill(InputAction.CallbackContext ctx)
        {
            if (ctx.phase != InputActionPhase.Started) return;
            LogMessage("Skill");
        }

        public void OnEvade(InputAction.CallbackContext ctx)
        {
            if (ctx.phase != InputActionPhase.Started) return;
            LogMessage("Evade");
        }

        public void OnMove(InputAction.CallbackContext ctx)
        {
            if (ctx.phase == InputActionPhase.Canceled) return;
            LogMessage("Move", ctx.ReadValue<Vector2>().ToString());
        }

        #endregion

        private void LogCurrentDevice(InputDevice device, InputDeviceChange deviceChange)
        {
            LogMessage("Detected Device Change", device.displayName, deviceChange.ToString());
        }

        private void LogMessage(params string[] messages)
        {
            Logger.Clear();
            
            foreach (string message in messages)
            {
                Logger.AppendLine(message);
            }
            
            Debug.Log(Logger);
        }
    }
}
