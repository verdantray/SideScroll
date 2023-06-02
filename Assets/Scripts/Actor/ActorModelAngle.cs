using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

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
        
        [SerializeField] private ActorBase actorBase = null;
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
            if (!actorBase) return;

            actorBase.OnDirectionChanged += SetAngleOnDirection;
            actorBase.OnActivityChanged += SetAngleOnActivity;
            
            SetAngle(actorBase.CurDirection, actorBase.CurActivity);
        }

        private void SetAngleOnDirection(ActorDirection direction)
        {
            SetAngle(direction, actorBase.CurActivity);
        }

        private void SetAngleOnActivity(ActorActivity activity)
        {
            SetAngle(actorBase.CurDirection, activity);
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
