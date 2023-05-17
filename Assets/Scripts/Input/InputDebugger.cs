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
        
        
        // 기존 Move 액션을 Vector2로 받았으나 종,횡 방향 입력을 나눌 필요가 생겨 분리
        // 수평 이동은 Axis 값 항상 적용
        public void OnMoveHorizontal(InputAction.CallbackContext ctx)
        {
            if (ctx.phase != InputActionPhase.Performed) return;

            float axis = ctx.ReadValue<float>();
            
            LogMessage(
                "Move Horizontal ",
                axis > 0.0f ? "Right" : "Left",
                axis.ToString(CultureInfo.InvariantCulture)
            );
        }

        // 수직 이동은 입력 시 마다 적용
        public void OnMoveVertical(InputAction.CallbackContext ctx)
        {
            if (ctx.phase != InputActionPhase.Started) return;

            string verticalBehavior = ctx.ReadValue<float>() > 0.0f ? "Jump" : "Sit";
            
            LogMessage("Move Vertical ", verticalBehavior);
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
