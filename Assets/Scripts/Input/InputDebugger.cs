using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SideScroll.Input
{
    // Scripted by Chococornets
    // 2023. 05. 15

    public class InputDebugger : MonoBehaviour
    {
        private StringBuilder Logger => logger ??= new StringBuilder();

        private StringBuilder logger = null;

        private void Start()
        {
            // 어째선지 에디터 플레이 중엔 출력 안되다가 종료 시 한꺼번에 출력되는 현상이 있음
            // InputSystem.onDeviceChange += LogCurrentDevice;
        }

        #region InGame Events

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Started) return;
            LogMessage("Attack");
        }

        public void OnSkill(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Started) return;
            LogMessage("Skill");
        }

        public void OnEvade(InputAction.CallbackContext ctx)
        {
            if (ctx.phase != InputActionPhase.Started) return;
            LogMessage("Evade");
        }

        public void OnVertical(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Started) return;

            string verticalBehavior = context.ReadValue<float>() > 0.0f ? "Jump" : "Sit";

            LogMessage("Move Vertical ", verticalBehavior);
        }

        public void OnHorizontal(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Started) return;

            float axis = context.ReadValue<float>();

            LogMessage(
                "Move Horizontal ",
                axis > 0.0f ? "Right" : "Left",
                axis.ToString(CultureInfo.InvariantCulture)
            );
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
