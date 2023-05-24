using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScroll
{
    public class Character : Actor
    {
        private const short JumpableCount = 1;
        private const float AngleOnIdle = 120.0f;
        private const float AngleOnBehaviour = 90.0f;
        
        [SerializeField] private Transform transform = null;
        [SerializeField] private Rigidbody rigidbody = null;
        
        [SerializeField] private Transform modelTransform = null;
        [SerializeField] private Animator animator = null;

        private bool IsMovable => isMovable;
        private bool IsJumpable => jumpCount < JumpableCount;

        protected float DirectionMultiplier => actorDirection == ActorDirection.Right ? 1.0f : -1.0f;

        private ActorDirection actorDirection = ActorDirection.Right;
        private bool isMovable = true;
        private short jumpCount = 0;

        private void Start()
        {
            Idle();
        }

        public override void Idle()
        {
            UnlockMove();
            InitiateJumpCount();

            SetAngle(AngleOnIdle);
        }

        public override void MoveLeft()
        {
            if (!IsMovable) return;
            actorDirection = ActorDirection.Left;
            
            SetAngle(90.0f);
        }

        public override void MoveRight()
        {
            if (!IsMovable) return;
            actorDirection = ActorDirection.Right;
            
            SetAngle(90.0f);
        }

        public override void Jump()
        {
            if (!IsJumpable) return;
        }

        public override void Sit()
        {
            LockMove();
        }

        public override void Attack() { }

        public override void Skill() { }

        public override void Evade() { }

        private void SetAngle(float angle)
        {
            modelTransform.eulerAngles = Vector3.up * angle * DirectionMultiplier;
        }
        private void LockMove() => isMovable = false;
        private void UnlockMove() => isMovable = true;
        private void InitiateJumpCount() => jumpCount = 0;
    }
}
