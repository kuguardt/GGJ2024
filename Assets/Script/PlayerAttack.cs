using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script
{
    public class PlayerAttack : MonoBehaviour
    {  
        [SerializeField] private GameObject attackPoint;

        private Animator _anim;
        Coroutine _atkCour = null;
        private void Start()
        {
            _anim = GetComponent<Animator>();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Attack();
            }
        }

        private void Attack()
        {
            if(_atkCour == null) _atkCour = StartCoroutine(AttackCoroutine());
        }

        public void TakeDamage()
        {
            GetComponent<PlayerHealth>().DecreasePlayerHealth(10);
        }
        
        IEnumerator AttackCoroutine()
        {
            _anim.Play("Attack2");
            yield return new WaitUntil(()=> !_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"));
            _atkCour = null;
         
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