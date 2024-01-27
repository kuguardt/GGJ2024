using UnityEngine;
using UnityEngine.InputSystem;

namespace Script
{
    public class PlayerSkill : MonoBehaviour
    {
        public void OnSkill(InputAction.CallbackContext context)
        {
            Debug.Log(
                $"{context.action.name} performed: {context.performed} started: {context.started} canceled: {context.canceled}");
        }
    }
}