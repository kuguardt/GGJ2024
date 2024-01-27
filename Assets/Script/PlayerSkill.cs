using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script
{
    public class PlayerSkill : MonoBehaviour
    {
        [SerializeField] private GameObject gasPrefab;
        [SerializeField] private float SkillCoolDown = 1f;
        
        private bool canUseSkill = true;
        public void OnSkill(InputAction.CallbackContext context)
        {
            Debug.Log(
                $"{context.action.name} performed: {context.performed} started: {context.started} canceled: {context.canceled}");
            if (canUseSkill)
            {
                StartCoroutine(Gas());
            }
        }

        private IEnumerator Gas()
        {
            canUseSkill = false;
            Instantiate(gasPrefab, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(SkillCoolDown);
            canUseSkill = true;
        }
    }
}