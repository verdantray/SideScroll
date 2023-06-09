using System.Globalization;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputHandle
{
    // Scripted by Chococornets
    // 2023. 05. 15

    public interface IInputHandleable
    {
        public void OnMove(InputValue inputValue);
        public void OnJump(InputValue inputValue);
        public void OnSit(InputValue inputValue);
        public void OnAttack(InputValue inputValue);
        public void OnSkill(InputValue inputValue);
        public void OnDash(InputValue inputValue);
    }

    public class InputDebugger : MonoBehaviour, IInputHandleable
    {
        private StringBuilder Logger => logger ??= new StringBuilder();

        private StringBuilder logger = null;

        #region User Input Events

        public void OnMove(InputValue inputValue)
        {
            float axis = inputValue.Get<float>();
            int value = Mathf.RoundToInt(axis);
            
            LogMessage("Move ", value.ToString(CultureInfo.InvariantCulture));
        }

        public void OnJump(InputValue inputValue)
        {
            LogMessage("Jump");
        }

        public void OnSit(InputValue inputValue)
        {
            LogMessage("Sit");
        }

        public void OnAttack(InputValue inputValue)
        {
            LogMessage("Attack");
        }

        public void OnSkill(InputValue inputValue)
        {
            LogMessage("Skill");
        }

        public void OnDash(InputValue inputValue)
        {
            LogMessage("Dash");
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
