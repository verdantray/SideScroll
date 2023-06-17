using System;
using UnityEngine;

namespace Actor.Essential
{
    public enum ActorDirection { Right, Left }
    public delegate void ActorDirectionDelegate(ActorDirection direction);
    
    public abstract class Actor : MonoBehaviour
    {
        [SerializeField] private GameObject modelObject = null;

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
        
        private Transform mModelTransform = null;
        private Animator mModelAnimator = null;

        public abstract void SetPosition(Vector3 position);
        protected abstract void SetAngle(float eulerY);
    }
}
