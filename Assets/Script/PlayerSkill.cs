using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script
{
    public class PlayerSkill : MonoBehaviour
    {
        [SerializeField] private GameObject gasPrefab;

        public bool canUseSkill = true;

        public float skillCD = 1.5f;

        public void OnSkill(InputAction.CallbackContext context)
        {
            if (canUseSkill && context.started)
            {
                Debug.Log($"Skill");

                AudioManager.instance.PlaySound("Shit");
                StartCoroutine(Gas());
            }
        }

        private IEnumerator Gas()
        {
            GasSkill gas = Instantiate(gasPrefab, transform.position, Quaternion.identity).GetComponent<GasSkill>();
            gas.AddGasIgnore(gameObject);
            
            yield return new WaitForEndOfFrame();
            canUseSkill = false;

            yield return new WaitForSeconds(skillCD);
            canUseSkill = true;
        }
    }
}