using System;
using UnityEngine;

namespace Actor
{
    // Actor가 바라보는 방향, 횡스크롤이므로 반드시 오른쪽 또는 왼쪽을 바라봅니다.
    public enum ActorDirection { Right, Left }
    public delegate void ActorDirectionDelegate(ActorDirection direction);
    
    public abstract class ActorBase : MonoBehaviour
    {
        [SerializeField] private GameObject modelObject = null;
        
        public event ActorDirectionDelegate OnDirectionChanged = delegate { };

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

        protected Transform ModelTransform
        {
            get
            {
                if (!(bool)mModelTransform) mModelTransform = modelObject.GetComponent<Transform>();
                return mModelTransform;
            }
        }

        protected Animator ModelAnimator
        {
            get
            {
                if (!(bool)mModelAnimator) mModelAnimator = modelObject.GetComponent<Animator>();
                return mModelAnimator;
            }
        }

        private ActorDirection mCurDirection = ActorDirection.Right;
        private Transform mModelTransform = null;
        private Animator mModelAnimator = null;

        protected virtual void Start()
        {
            // NPC 등의 경우 정면을 향하도록 합니다.
            Idle();
            SetAngle(180.0f);
        }

        protected void SetAngle(float angleToSet)
        {
            ModelTransform.eulerAngles = Vector3.up
                                          * (angleToSet * (CurDirection == ActorDirection.Right ? 1.0f : -1.0f));
        }

        public virtual void Idle()
        {
            ModelAnimator.Play("Idle");
        }

        public virtual void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}
