using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScroll.Actor
{
    public abstract class ActorBase : MonoBehaviour
    {
        protected delegate void CheckGroundDelegate(bool isGround);
        
        [SerializeField] private CharacterController controller = null;
        [SerializeField] private ActorModel model = null;
        
        private float Gravity => Physics.gravity.y;
        
        private Vector3 velocity = Vector3.zero;

        private void Start()
        {
            
        }

        private void FixedUpdate()
        {
            FallOnAir();
            
            if (velocity == Vector3.zero) return;
            controller.Move(velocity * Time.fixedDeltaTime);
        }

        // ActionMap 상에서 Move InputAction은 1D Axis로 동작하도록 함
        // expected value of 'direction' : -1 ~ 1
        public virtual void Move(int direction)
        {
            velocity.x = Const.MoveMultiplier * direction;
            model.PlayMove(direction);
        }

        public virtual void Jump()
        {
            velocity.y += Mathf.Sqrt(controller.height * 2.0f * Mathf.Abs(Gravity));
            model.PlayJump();
        }
        
        private void FallOnAir()
        {
            velocity.y = controller.isGrounded && velocity.y < 0.0f
                ? 0.0f
                : velocity.y + (Gravity * Time.fixedDeltaTime);
        }
    }
}
