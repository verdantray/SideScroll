using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SideScroll.Input
{
    // Scripted by Chococornets
    // 2023. 05. 15
    
    [RequireComponent(typeof(PlayerInput))]
    public class InputListener : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;

        private void Reset()
        {
            gameObject.AddComponent<PlayerInput>();
        }
    }
}
