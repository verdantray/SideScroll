using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor
{
    /// <summary>
    /// Actor가 바라보는 방향에 맞춰 Model의 각도 변경
    /// </summary>
    public class ActorAngle : MonoBehaviour
    {
        private const float ModelAngleOnRight = 90.0f;
        private const float ModelAngleOnLeft = -90.0f;

        [SerializeField] private ActorBase actor = null;

        private void Start()
        {
            SetAngle(actor.CurDirection);
            actor.OnDirectionChanged += SetAngle;
        }

        private void SetAngle(ActorDirection direction)
        {
            float directionMultiplier = direction == ActorDirection.Right
                ? ModelAngleOnRight
                : ModelAngleOnLeft;

            transform.eulerAngles = Vector3.up * directionMultiplier;
        }
    }
}
