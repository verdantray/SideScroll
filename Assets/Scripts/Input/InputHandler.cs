using System.Collections;
using System.Collections.Generic;
using SideScroll.Actor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SideScroll.Input
{
    // Scripted by Chococornets
    // 2023. 05. 15
    
    [RequireComponent(typeof(PlayerInput))]
    public class InputHandler : MonoBehaviour, IInputHandleable
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private ActorBase tempActor = null;

        private void Reset()
        {
            playerInput = gameObject.GetComponent<PlayerInput>();
        }
        
        public void OnMove(InputValue inputValue)
        {
            Debug.Log("Move");
            
            int axis = Mathf.RoundToInt(inputValue.Get<float>());
            tempActor.Move(axis);
        }

        public void OnJump(InputValue inputValue)
        {
            tempActor.Jump();
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
