using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScroll
{
    public abstract class Actor : MonoBehaviour
    {
        public abstract void Idle();
        
        public abstract void MoveLeft();
        public abstract void MoveRight();
        public abstract void Jump();
        public abstract void Sit();

        public abstract void Attack();
        public abstract void Skill();
        public abstract void Evade();
    }
    
    public enum ActorDirection { Right, Left }
}
