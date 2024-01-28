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
        Coroutine _atkHitCour = null;

        private void Awake()
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
            if (_atkCour == null)
            {
                atkHit = false;
                _atkHitCour = StartCoroutine(AttackHit());
                _atkCour = StartCoroutine(AttackCoroutine());
            }
        }
        
        
        IEnumerator AttackCoroutine()
        {
            AudioManager.instance.PlaySound("Attack");

            _anim.Play("Attack2");
            yield return new WaitUntil(()=> !_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"));
            _atkCour = null;
        }

        IEnumerator AttackHit()
        {
            yield return new WaitUntil(()=>atkHit);
            attackHitEffect.SetActive(true);
            AudioManager.instance.PlaySound("AttackHit");

            yield return new WaitUntil(()=> !_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"));
            attackHitEffect.SetActive(false);
            _atkHitCour = null;
        }
        
        private bool atkHit = false;
        [SerializeField] private GameObject attackHitEffect;
        
        [SerializeField] private float PunchDamage = 10f;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                atkHit = true;
                other.gameObject.GetComponent<PlayerHealth>().DecreasePlayerHealth(PunchDamage);
                other.gameObject.GetComponent<PlayerMovement>().GotAttacked(transform.right);
            }
        }
    }
}