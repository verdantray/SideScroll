using UnityEngine;

namespace Actor
{
    /// <summary>
    /// Model의 Animator를 Actor 상태에 맞춰 재생하는 클래스
    /// </summary>
    public class ActorAnimationController : MonoBehaviour
    {
        [SerializeField] protected ActorBase actor = null;
        [SerializeField] protected Animator modelAnimator = null;
        
        private readonly int moveDirectionHash = Animator.StringToHash("MoveDirection");
        private readonly int jumpCountHash = Animator.StringToHash("JumpCount");
        private readonly int velocityYHash = Animator.StringToHash("VelocityY");

        private void Start()
        {
            SetJumpCount(actor.JumpCount);
            actor.OnActorJumped += SetJumpCount;
        }

        private void Update()
        {
            Vector3 actorVelocity = actor.Velocity;

            modelAnimator.SetInteger(moveDirectionHash, Mathf.RoundToInt(actorVelocity.x));
            modelAnimator.SetFloat(velocityYHash, actorVelocity.y);
        }

        protected void SetJumpCount(int jumpCount)
        {
            modelAnimator.SetInteger(jumpCountHash, jumpCount);
        }
    }
}
