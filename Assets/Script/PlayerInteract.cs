using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script
{
    public class PlayerInteract : MonoBehaviour
    {
        public bool _isInteracting = false;
        private Toilet _toilet = null;

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                StartInteract();
            }
            else if (context.canceled)
            {
                StopInteract();
            }
        }
        
        private void StartInteract()
        {
            Debug.Log("Interact");
            if (_toilet != null && !_toilet.isOccupied)
            {
                StartUsingToilet();
            }
        }
        
        private void StopInteract()
        {
            Debug.Log("End Interact");
            if (_toilet != null && _isInteracting)
            {
                StopUsingToilet();
            }

        }
        
        private void StartUsingToilet()
        {
            _toilet.isOccupied = true;
                
            _isInteracting = true;
            
            GetComponent<Animator>().Play("Khee");
            GetComponent<PlayerHealth>().SetDecayValue(5f);

            transform.position = _toilet.transform.position;
        }

        private void StopUsingToilet()
        {
            if(_toilet.gameObject.activeSelf) _toilet.isOccupied = false;
                
            _isInteracting = false;
            
            GetComponent<Animator>().SetTrigger("endKhee");
            GetComponent<PlayerHealth>().SetDecayValue(-1f);

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Toilet"))
            {
                _toilet = other.GetComponent<Toilet>();
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Toilet"))
            {
                StopInteract();
                _toilet = null;
            }
        }
    }
}