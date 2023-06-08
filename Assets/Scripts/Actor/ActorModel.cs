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
        [SerializeField] protected Animator modelAnimator = null;

        private Transform ModelTransform
        {
            get
            {
                if (!mTransform) mTransform = gameObject.transform;
                return mTransform;
            }
        }
        
        private Transform mTransform = null;
        
        private readonly int moveDirectionHash = Animator.StringToHash("MoveDirection");
        private readonly int jumpCountHash = Animator.StringToHash("JumpCount");
        private readonly int velocityYHash = Animator.StringToHash("VelocityY");

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            Vector3 actorVelocity = actor.Velocity;
            
            modelAnimator.SetInteger(moveDirectionHash, Mathf.RoundToInt(actorVelocity.x));
            modelAnimator.SetFloat(velocityYHash, actorVelocity.y);
        }

        private void Initialize()
        {
            SetAngle(actor.CurDirection);
            actor.OnDirectionChanged += SetAngle;

            SetJumpCount(actor.JumpCount);
            actor.OnActorJumped += SetJumpCount;
        }

        protected void SetJumpCount(int jumpCount)
        {
            Debug.Log(jumpCount);
            modelAnimator.SetInteger(jumpCountHash, jumpCount);
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
