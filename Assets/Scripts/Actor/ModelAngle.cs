using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScroll.Actor
{
    public class ModelAngle : StateMachineBehaviour
    {
        [SerializeField] private float angleOnEnter = 120.0f;
        [SerializeField] private float angleOnExit = 90.0f;

        public override void OnStateEnter(Animator modelAnimator, AnimatorStateInfo _, int __)
        {
            
        }

        public override void OnStateExit(Animator modelAnimator, AnimatorStateInfo _, int __)
        {
            
        }

        private void ChangeAngle(float toChange)
        {
            
        }
    }
}
