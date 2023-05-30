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
        [SerializeField] private Actor tempActor = null;
        
        private StringBuilder Logger => logger ??= new StringBuilder();

        private StringBuilder logger = null;

        private void Start()
        {
            // 어째선지 에디터 플레이 중엔 출력 안되다가 종료 시 한꺼번에 출력되는 현상이 있음
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
            ActorDirection direction = ctx.ReadValue<float>() >= 1.0f
                ? ActorDirection.Right
                : ActorDirection.Left;
            
            switch (ctx.phase)
            {
                case InputActionPhase.Started:
                    tempActor.Move(direction);
                    break;
                
                case InputActionPhase.Canceled:
                    tempActor.Idle();
                    break;
            }
            
            // if (ctx.phase != InputActionPhase.Performed) return;
            //
            // float axis = ctx.ReadValue<float>();
            //
            // LogMessage(
            //     "Move Horizontal ",
            //     axis > 0.0f ? "Right" : "Left",
            //     axis.ToString(CultureInfo.InvariantCulture)
            // );
        }

        // 수직 이동은 입력 시 마다 적용
        public void OnMoveVertical(InputAction.CallbackContext ctx)
        {
            if (ctx.phase != InputActionPhase.Started) return;
            
            if (ctx.ReadValue<float>() > 0.0f) tempActor.Jump();

            // string verticalBehavior = ctx.ReadValue<float>() > 0.0f ? "Jump" : "Sit";
            //
            // LogMessage("Move Vertical ", verticalBehavior);
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
