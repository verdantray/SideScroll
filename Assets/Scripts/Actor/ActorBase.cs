using UnityEngine;

namespace Actor
{
    // Actor가 바라보는 방향, 횡스크롤이므로 반드시 오른쪽 또는 왼쪽을 바라봅니다.
    public enum ActorDirection { Right, Left }
    public delegate void ActorDirectionDelegate(ActorDirection direction);
    public delegate void ActorJumpDelegate(int jumpCount);
    
    public abstract class ActorBase : MonoBehaviour
    {
        [SerializeField] private CharacterController controller = null;

        #region Events

        public event ActorDirectionDelegate OnDirectionChanged = delegate { };
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

        public Vector3 Velocity => velocity;
        
        private float Gravity => Physics.gravity.y;

        #endregion

        protected Vector3 velocity = Vector3.zero;

        private ActorDirection mCurDirection = ActorDirection.Right;
        private int mJumpCount = 0;

        private void FixedUpdate()
        {
            ComputeVelocity();
            if (velocity.y <= 0.0f && controller.isGrounded) JumpCount = 0;
        }

        // ActionMap 상에서 Move InputAction은 1D Axis로 동작하도록 함
        // expected value of 'direction' : -1 ~ 1
        public virtual void Move(int moveDirection)
        {
            ChangeDirection(moveDirection);
            velocity.x = moveDirection * Const.MoveMultiplier;
        }

        public virtual void Jump()
        {
            JumpCount++;
            velocity.y += Mathf.Sqrt(controller.height * Const.JumpMultiplier * Mathf.Abs(Gravity));
        }

        private void ChangeDirection(int moveDirection)
        {
            if (moveDirection == 0) return;
            CurDirection = moveDirection > 0 ? ActorDirection.Right : ActorDirection.Left;
        }

        private void ComputeVelocity()
        {
            // 공중에 있을 시 중력에 비례하여 아래 방향으로 당겨짐 / 지상에 있을 시 y축으로 이동하지 않음
            velocity.y = controller.isGrounded && velocity.y < 0.0f
                ? 0.0f
                : velocity.y + (Gravity * Time.fixedDeltaTime);
            
            if (velocity == Vector3.zero) return;
            controller.Move(velocity * Time.fixedDeltaTime);
        }
    }
}
