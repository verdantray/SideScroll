using System;
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

        private float speed = 5.0f;
        
        private bool move = false;
        private static readonly int MoveDelta = Animator.StringToHash("MoveDelta");

        #endregion

        private void Start()
        {
            Idle();
        }

        private void FixedUpdate()
        {
            MovePosition();
        }

        public virtual void Idle()
        {
            CurActivity = ActorActivity.Idle;
            modelAnimator.Play("Idle");
        }

        public virtual void Move(int value)
        {
            if (value != 0)
            {
                CurDirection = value > 0 ? ActorDirection.Right : ActorDirection.Left;
            }
            
            move = value != 0;
            modelAnimator.SetInteger(MoveDelta, value);
        }

        public virtual void Jump()
        {
            CurActivity = ActorActivity.OnAction;
            
            modelAnimator.Play("Jump");
        }

        private void MovePosition()
        {
            if (!move) return;
            
            Vector3 translation = Vector3.right * (speed * (CurDirection == ActorDirection.Right ? 1.0f : -1.0f) * Time.fixedDeltaTime);
            transform.Translate(translation);
        }
    }
}
