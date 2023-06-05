using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScroll.Actor
{
    // Actor가 바라보는 방향, 횡스크롤이므로 반드시 오른쪽 또는 왼쪽을 바라봅니다.
    public enum ActorDirection { Right, Left }
    
    // Actor의 활동 상태
    public enum ActorActivity { Common, Idle, OnAction, OnInteraction }

    public delegate void DirectionDelegate(ActorDirection direction);

    public delegate void ActivityDelegate(ActorActivity activity);


    public abstract class ActorBase : MonoBehaviour
    {
        [SerializeField] private CharacterController controller = null;
        [SerializeField] private Animator modelAnimator = null;
        
        #region Events

        public event DirectionDelegate OnDirectionChanged = delegate { };
        public event ActivityDelegate OnActivityChanged = delegate { };

        #endregion
        
        #region Fields & Properties
        
        private float Gravity => Physics.gravity.y;

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
        
        private ActorDirection mCurDirection = ActorDirection.Right;

        public ActorActivity CurActivity
        {
            get => mCurActivity;
            protected set
            {
                if (mCurActivity == value) return;

                mCurActivity = value;
                OnActivityChanged.Invoke(mCurActivity);
            }
        }
        
        private ActorActivity mCurActivity = ActorActivity.Idle;
        private Vector3 velocity = Vector3.zero;
        
        private static readonly int MoveDelta = Animator.StringToHash("MoveDelta");

        #endregion
        
        private void FixedUpdate()
        {
            FallOnAir();
            
            if (velocity == Vector3.zero) return;
            controller.Move(velocity * Time.fixedDeltaTime);
        }

        private void FallOnAir()
        {
            velocity.y = controller.isGrounded && velocity.y < 0.0f
                ? 0.0f
                : velocity.y + (Gravity * Time.fixedDeltaTime);
        }

        public virtual void Move(int value)
        {
            if (value != 0)
            {
                CurDirection = value > 0 ? ActorDirection.Right : ActorDirection.Left;
            }

            velocity.x = value;
            modelAnimator.SetInteger(MoveDelta, value);
        }

        public virtual void Jump()
        {
            CurActivity = ActorActivity.OnAction;
            velocity.y += Mathf.Sqrt(controller.height * 2.0f * Mathf.Abs(Physics.gravity.y));
            
            modelAnimator.Play("Jump");
        }
    }
}
