using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScroll.Actor
{
    // Actor가 바라보는 방향, 횡스크롤이므로 반드시 오른쪽 또는 왼쪽을 바라봅니다.
    public enum ActorDirection { Right, Left }

    public delegate void ActorDirectionDelegate(ActorDirection direction);

    /// <summary>
    /// Actor 클래스의 속성을 중계받고 동작에 맞춰 3d 모델을 동작시키는 클래스
    /// </summary>
    public class ActorModel : MonoBehaviour
    {
        private const float AngleOnRight = 90.0f;
        private const float AngleOnLeft = -90.0f;

        protected event ActorDirectionDelegate OnDirectionChanged = delegate { };

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

        private Transform ModelTransform
        {
            get
            {
                if (!mTransform) mTransform = gameObject.transform;
                return mTransform;
            }
        }

        private Animator ModelAnimator
        {
            get
            {
                if (!mAnimator) mAnimator = gameObject.GetComponent<Animator>();
                return mAnimator;
            }
        }
        
        private ActorDirection mCurDirection = ActorDirection.Right;
        private Transform mTransform = null;
        private Animator mAnimator = null;
        
        private readonly int moveAnimHash = Animator.StringToHash("MoveDelta");

        private void Start()
        {
            SetAngle(CurDirection);
            OnDirectionChanged += SetAngle;
        }

        public void PlayMove(int delta)
        {
            if (delta != 0)
            {
                CurDirection = delta > 0 ? ActorDirection.Right : ActorDirection.Left;
            }
            
            ModelAnimator.SetInteger(moveAnimHash, delta);
        }

        public void PlayJump()
        {
            ModelAnimator.Play("Jump");
        }

        private void SetAngle(ActorDirection direction)
        {
            ModelTransform.eulerAngles = Vector3.up * (direction == ActorDirection.Right ? AngleOnRight : AngleOnLeft);
        }
    }
}
