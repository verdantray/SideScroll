using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SideScroll.Actor
{
    [Serializable]
    public class AngleOnActivity
    {
        public ActorActivity activity;
        public float angle;
    }
    
    public class ActorModelAngle : MonoBehaviour
    {
        private const float CommonActivityAngle = 120.0f;
        
        [SerializeField] private ActorBase actor = null;
        [SerializeField] private Transform modelTransform = null;
        [SerializeField] private AngleOnActivity[] angleOnActivities = null;

        private void Reset()
        {
            modelTransform = transform;

            angleOnActivities = new []
            {
                new AngleOnActivity
                {
                    activity = ActorActivity.Common,
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
            float angle = angleOnActivities.FirstOrDefault(aoa => aoa.activity == activity)?.angle
                          ?? CommonActivityAngle;
            
            float directionMultiplier = direction == ActorDirection.Right ? 1.0f : -1.0f;

            modelTransform.eulerAngles = Vector3.up * angle * directionMultiplier;
        }
    }
}
