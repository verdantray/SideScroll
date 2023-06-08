using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScroll.Actor
{
    /// <summary>
    /// Actor 클래스의 속성을 중계받고 동작에 맞춰 3d 모델을 동작시키는 클래스
    /// </summary>
    public class ActorModel : MonoBehaviour
    {
        [SerializeField] protected ActorBase actor = null;

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
        
        private Transform mTransform = null;
        private Animator mAnimator = null;
        
        private readonly int moveDirectionHash = Animator.StringToHash("MoveDirection");
        private readonly int jumpCountHash = Animator.StringToHash("JumpCount");

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            SetMoveDirection(actor.MoveDelta);
            actor.OnActorMoved += SetMoveDirection;
            
            SetAngle(actor.CurDirection);
            actor.OnDirectionChanged += SetAngle;

            SetJumpCount(actor.JumpCount);
            actor.OnActorJumped += SetJumpCount;
        }

        protected void SetMoveDirection(int moveDelta)
        {
            ModelAnimator.SetInteger(moveDirectionHash, moveDelta);
        }

        protected void SetJumpCount(int jumpCount)
        {
            ModelAnimator.SetInteger(jumpCountHash, jumpCount);
        }

        protected void SetAngle(ActorDirection direction)
        {
            float directionMultiplier = direction == ActorDirection.Right
                ? Const.ModelAngleOnRight
                : Const.ModelAngleOnLeft;

            ModelTransform.eulerAngles = Vector3.up * directionMultiplier;
        }
    }
}
