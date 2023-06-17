using Actor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputHandle
{
    // Scripted by Chococornets
    // 2023. 05. 15
    
    [RequireComponent(typeof(PlayerInput))]
    public class InputHandler : MonoBehaviour, IInputHandleable
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Character tempActor = null;

        private void Reset()
        {
            playerInput = gameObject.GetComponent<PlayerInput>();
        }
        
        public void OnMove(InputValue inputValue)
        {
            int axis = Mathf.RoundToInt(inputValue.Get<float>());
            // tempActor.Move(axis);
        }

        public void OnJump(InputValue inputValue)
        {
            
        }

        public void OnSit(InputValue inputValue)
        {
            
        }

        public void OnAttack(InputValue inputValue)
        {
            
        }

        public void OnSkill(InputValue inputValue)
        {
            
        }

        public void OnDash(InputValue inputValue)
        {
            
        }
    }
}
