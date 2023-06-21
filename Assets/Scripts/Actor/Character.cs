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

        public (Vector3 origin, float maxDist, LayerMask layerMask) GetRayCastParameters(Vector3 position)
        {
            return (position + originOffset, maxDistance, layerMask);
        }
    }
    
    public class Character : InteractableActor
    {
        [SerializeField] private RayCastInfo groundCheckInfo = default;

        private readonly int jumpAnimParameter = Animator.StringToHash("JumpState");

        public bool IsGround { get; private set; } = false;
        
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

        public void Move(int moveDirection)
        {
            if (moveDirection != 0)
            {
                CurDirection = moveDirection > 0
                    ? ActorDirection.Right
                    : ActorDirection.Left;
            }
            
            rigidBody.AddForce(Vector3.right * moveDirection, ForceMode.VelocityChange);
        }

        public void Jump()
        {
            if (!IsGround) return;
            rigidBody.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
        }

        private void FixedUpdate()
        {
            CheckGroundState();
        }

        private void CheckGroundState()
        {
            var (origin, maxDist, layerMask) = groundCheckInfo.GetRayCastParameters(rigidBody.position);
            
            IsGround = Physics.Raycast(
                origin: origin,
                direction: Vector3.down,
                maxDistance: maxDist,
                layerMask: layerMask
            );

            if (IsGround)
            {
                JumpState = JumpState.Ground;
            }
            else if (rigidBody.velocity.y > 0.0f)
            {
                JumpState = JumpState.Upward;
            }
            else
            {
                JumpState = JumpState.Downward;
            }
        }
    }
}
