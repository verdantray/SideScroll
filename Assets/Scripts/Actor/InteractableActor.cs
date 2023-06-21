using Actor.Essential;
using UnityEngine;

namespace Actor
{
    public abstract class InteractableActor : Essential.Actor
    {
        [SerializeField] protected Rigidbody rigidBody = null;

        public event ActorDirectionDelegate OnDirectionChanged = delegate { };

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
        
        public override void SetPosition(Vector3 position)
        {
            rigidBody.position = position;
            rigidBody.velocity = Vector3.zero;
        }

        protected override void SetAngle(float eulerY)
        {
            rigidBody.rotation = Quaternion.Euler(0.0f, eulerY, 0.0f);
        }
    }
}
