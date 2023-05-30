using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScroll
{
    // Actor가 바라보는 방향, 횡스크롤이므로 반드시 오른쪽 또는 왼쪽을 바라봅니다.
    public enum ActorDirection { Right, Left }
    
    // Actor의 활동 여부
    public enum ActorActivity { Idle, OnAction, OnInteraction, OnFinish, OnRetire }

    public delegate void DirectionDelegate(ActorDirection direction);

    public delegate void ActivityDelegate(ActorActivity activity);


    public abstract class Actor : MonoBehaviour
    {
        [SerializeField] private Animator modelAnimator = null;
        
        #region Events

        public event DirectionDelegate OnDirectionChanged = delegate { };
        public event ActivityDelegate OnActivityChanged = delegate { };

        #endregion
        
        #region Fields & Properties

        public ActorDirection CurDirection
        {
            get => mCurDirection;
            set
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
            set
            {
                if (mCurActivity == value) return;

                mCurActivity = value;
                OnActivityChanged.Invoke(mCurActivity);
            }
        }
        
        private ActorActivity mCurActivity = ActorActivity.Idle;

        #endregion

        public virtual void Idle()
        {
            CurActivity = ActorActivity.Idle;
            modelAnimator.Play("Idle");
        }

        public virtual void Move(ActorDirection direction)
        {
            CurDirection = direction;
            CurActivity = ActorActivity.OnAction;
            
            modelAnimator.Play("Move");
        }

        public virtual void Jump()
        {
            CurActivity = ActorActivity.OnAction;
            
            modelAnimator.Play("Jump");
        }
    }
}
