using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script
{
    public class PlayerInteract : MonoBehaviour
    {
        public bool _isInteracting = false;
        private Toilet _toilet = null;
        private ElevatorButton _elevatorbutton = null;

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
            if (_elevatorbutton != null && _elevatorbutton.isActive)
            {
                _elevatorbutton.Press();
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
        float originZ = 0;
        private void StartUsingToilet()
        {
            _toilet.isOccupied = true;
                
            _isInteracting = true;
            
            GetComponent<Animator>().Play("Khee");
            GetComponent<PlayerHealth>().SetDecayValue(5f);

            originZ = transform.position.z;
            transform.position = _toilet.transform.position;
        }

        private void StopUsingToilet()
        {
            if(_toilet.gameObject.activeSelf) _toilet.isOccupied = false;
                
            _isInteracting = false;
            
            GetComponent<Animator>().SetTrigger("endKhee");
            GetComponent<PlayerHealth>().SetDecayValue(-1f);

            Vector3 tran = transform.position;
            tran.z = originZ;
            transform.position = tran;

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Toilet"))
            {
                _toilet = other.GetComponent<Toilet>();
            }
            if (other.CompareTag("ElevatorButton"))
            {
                _elevatorbutton = other.GetComponent<ElevatorButton>();
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Toilet"))
            {
                StopInteract();
                _toilet = null;
            }
            if (other.CompareTag("ElevatorButton"))
            {
                _elevatorbutton = null;
            }
        }
    }
}