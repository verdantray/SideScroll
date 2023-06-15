using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor
{
    public abstract class ControllableActor : ActorBase
    {
        [SerializeField] private CharacterController controller = null;
        
        public float MoveVelocity
        {
            get => velocity.x;
            protected set => velocity.x = value;
        }

        public float VerticalVelocity
        {
            get => velocity.y;
            protected set => velocity.y = value;
        }

        protected float ActorHeight => controller.height;
        
        private Vector3 velocity = Vector3.zero;

        private void FixedUpdate()
        {
            PerformMovement();
        }

        public abstract void Move(int moveDirection);
        public abstract void Attack();

        protected virtual void PerformMovement()
        {
            if (velocity == Vector3.zero) return;
            controller.Move(velocity * Time.fixedDeltaTime);
        }

        public override void SetPosition(Vector3 position)
        {
            base.SetPosition(position);
            velocity = Vector3.zero;
        }
    }
}
