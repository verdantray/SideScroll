using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SideScroll
{
    [Serializable]
    public class AngleOnActivity
    {
        public ActorActivity activity;
        public float angle;
    }
    
    public class ActorModelAngle : MonoBehaviour
    {
        [SerializeField] private Actor actor = null;
        [SerializeField] private Transform modelAngle = null;
        [SerializeField] private AngleOnActivity[] angleOnActivities = null;

        private void Reset()
        {
            modelAngle = transform;

            angleOnActivities = new []
            {
                new AngleOnActivity
                {
                    activity = ActorActivity.Idle,
                    angle = 120.0f
                },
                new AngleOnActivity
                {
                    activity = ActorActivity.OnAction,
                    angle = 90.0f
                }
            };
        }

        private void Start()
        {
            if (!actor) return;

            actor.OnDirectionChanged += SetAngleOnDirection;
            actor.OnActivityChanged += SetAngleOnActivity;
            
            SetAngle(actor.CurDirection, actor.CurActivity);
        }

        private void SetAngleOnDirection(ActorDirection direction)
        {
            SetAngle(direction, actor.CurActivity);
        }

        private void SetAngleOnActivity(ActorActivity activity)
        {
            SetAngle(actor.CurDirection, activity);
        }

        private void SetAngle(ActorDirection direction, ActorActivity activity)
        {
            float angle = angleOnActivities.FirstOrDefault(aoa => aoa.activity == activity)?.angle ?? 0.0f;
            float directionMultiplier = direction == ActorDirection.Right ? 1.0f : -1.0f;

            modelAngle.eulerAngles = Vector3.up * angle * directionMultiplier;
        }
    }
}
