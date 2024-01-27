using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script
{
    public class PlayerAttack : MonoBehaviour
    {  
        [SerializeField] private GameObject attackPoint;
        
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Attack();
            }
        }

        private void Attack()
        {
            Debug.Log("BLING");
            StartCoroutine(AttackCoroutine());
        }

        public void TakeDamage()
        {
            GetComponent<PlayerHealth>().DecreasePlayerHealth(10);
        }
        
        IEnumerator AttackCoroutine()
        {
            attackPoint.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            attackPoint.SetActive(false);
        }

        [SerializeField] private float attackForce = 100f;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log($"{other.name} is attacked by {gameObject.name}");
                
                other.gameObject.GetComponent<PlayerMovement>().GotAttacked(transform.right);
                //other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * attackForce, ForceMode2D.Impulse);
            }
        }
    }
}