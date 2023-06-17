using System;
using System.Collections;
using System.Collections.Generic;
using Actor.Essential;
using UnityEngine;

namespace Actor
{
    public abstract class InteractableActor : Essential.Actor
    {
        [SerializeField] protected Rigidbody rigidBody = null;
        [SerializeField] private float collisionCheckDist = 0.0f;

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

        protected Ray BottomRay => new Ray(rigidBody.position, Vector3.down);

        private ActorDirection mCurDirection = ActorDirection.Right;

        private void FixedUpdate()
        {
            int layer = LayerMask.NameToLayer("Ground");
            bool raycastRes = Physics.Raycast(BottomRay, out RaycastHit bottomInfo, collisionCheckDist);

            Debug.DrawRay(BottomRay.origin, BottomRay.direction, Color.red);
            Debug.DrawRay(BottomRay.origin, Vector3.right, Color.red);

            if (raycastRes)
            {
                Debug.Log($"Hit, {bottomInfo}");
            }
        }


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
