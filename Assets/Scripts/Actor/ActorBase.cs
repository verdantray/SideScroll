using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScroll.Actor
{
    // Actor가 바라보는 방향, 횡스크롤이므로 반드시 오른쪽 또는 왼쪽을 바라봅니다.
    public enum ActorDirection { Right, Left }
    public delegate void ActorDirectionDelegate(ActorDirection direction);
    public delegate void ActorMoveDelegate(int moveDirection);
    public delegate void ActorJumpDelegate(int jumpCount);
    
    public abstract class ActorBase : MonoBehaviour
    {
        [SerializeField] private CharacterController controller = null;

        #region Events

        public event ActorDirectionDelegate OnDirectionChanged = delegate { };
        public event ActorMoveDelegate OnActorMoved = delegate { };
        public event ActorJumpDelegate OnActorJumped = delegate { };

        #endregion

        #region Properties

        public ActorDirection CurDirection
        {
            get => mCurDirection;
            protected set
            {
                if (mCurDirection == value) return;

                mCurDirection = value;
                OnDirectionChanged.Invoke(mCurDirection);
            }
        }

        public int MoveDelta
        {
            get => mMoveDelta;
            protected set
            {
                if (mMoveDelta == value) return;

                mMoveDelta = value;
                OnActorMoved.Invoke(mMoveDelta);
            }
        }
        
        public int JumpCount
        {
            get => mJumpCount;
            protected set
            {
                if (mJumpCount == value) return;

                mJumpCount = value;
                OnActorJumped.Invoke(mJumpCount);
            }
        }
        
        private float Gravity => Physics.gravity.y;

        #endregion

        protected Vector3 velocity = Vector3.zero;

        private ActorDirection mCurDirection = ActorDirection.Right;
        private int mMoveDelta = 0;
        private int mJumpCount = 0;

        private void FixedUpdate()
        {
            FallOnAir();
            
            if (velocity == Vector3.zero) return;
            controller.Move(velocity * Time.fixedDeltaTime);
        }

        // ActionMap 상에서 Move InputAction은 1D Axis로 동작하도록 함
        // expected value of 'direction' : -1 ~ 1
        public virtual void Move(int moveDelta)
        {
            MoveDelta = moveDelta;
            
            if (MoveDelta != 0)
            {
                CurDirection = MoveDelta > 0 ? ActorDirection.Right : ActorDirection.Left;
            }
            
            velocity.x = MoveDelta * Const.MoveMultiplier;
        }

        public virtual void Jump()
        {
            JumpCount++;
            velocity.y += Mathf.Sqrt(controller.height * Const.JumpMultiplier * Mathf.Abs(Gravity));
        }
        
        private void FallOnAir()
        {
            velocity.y = controller.isGrounded && velocity.y < 0.0f
                ? 0.0f
                : velocity.y + (Gravity * Time.fixedDeltaTime);
        }
    }
}
