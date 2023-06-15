using UnityEngine;

namespace Actor
{
    public enum JumpState
    {
        Ground,
        Upward,
        Downward,
    }
    
    public class Character : ControllableActor
    {
        public override void Move(int moveDirection)
        {
            if (moveDirection != 0)
            {
                CurDirection = moveDirection > 0 ? ActorDirection.Right : ActorDirection.Left;
            }

            MoveVelocity = moveDirection;
        }

        public void Jump()
        {
            VerticalVelocity = ActorHeight;
        }

        public override void Attack()
        {
            Debug.Log("Attack!");
        }

        protected override void PerformMovement()
        {
            
            
            base.PerformMovement();
        }
    }
}
