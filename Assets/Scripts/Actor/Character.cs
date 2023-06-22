using System;
using Actor.Essential;
using UnityEngine;

namespace Actor
{
    public enum JumpState
    {
        Ground = 0,
        Upward = 1,
        Downward = 2,
    }

    [Serializable]
    public struct RayCastInfo
    {
        public Vector3 originOffset;
        public float maxDistance;
        public LayerMask layerMask;

        public bool GetRayCastResult(Vector3 position, Vector3 direction)
        {
            return Physics.Raycast(position + originOffset, direction, maxDistance, layerMask);
        }
    }
    
    public class Character : InteractableActor
    {
        [SerializeField] private RayCastInfo collisionChecker = default;

        private readonly int jumpAnimParameter = Animator.StringToHash("JumpState");
        private readonly int moveAnimParameter = Animator.StringToHash("MoveDirection");

        public bool IsGround { get; private set; } = false;
        public int JumpCount { get; private set; } = 0;
        
        private float moveDirection = 0.0f;
        
        private JumpState JumpState
        {
            get => jumpState;
            set
            {
                if (jumpState == value) return;
                
                jumpState = value;
                ModelAnimator.SetInteger(jumpAnimParameter, (int)jumpState);
            }
        }
        
        private JumpState jumpState = JumpState.Downward;

        public void Move(int axis)
        {
            if (axis != 0)
            {
                CurDirection = axis > 0
                    ? ActorDirection.Right
                    : ActorDirection.Left;
            }

            moveDirection = axis;
            ModelAnimator.SetInteger(moveAnimParameter, axis);
            SetAngle(90.0f * (CurDirection == ActorDirection.Right ? 1.0f : -1.0f));
        }

        public void Jump()
        {
            if (!IsGround) return;
            rigidBody.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
        }

        private void FixedUpdate()
        {
            CheckGroundCollision();
            rigidBody.MovePosition(rigidBody.position + Vector3.right * (moveDirection * Time.fixedDeltaTime * 5.0f));
        }

        private void CheckGroundCollision()
        {
            IsGround = collisionChecker.GetRayCastResult(rigidBody.position, Vector3.down);

            if (IsGround)
            {
                JumpState = JumpState.Ground;
            }
            else
            {
                JumpState = rigidBody.velocity.y > 0.0f
                    ? JumpState.Upward
                    : JumpState.Downward;
            }
        }
    }
}
