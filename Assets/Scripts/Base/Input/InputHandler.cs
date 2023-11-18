using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Base.Input
{
     public class InputHandler : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Transform _transform;

        private InputAction touchPositionAction;
        private InputAction tap;

        private void Awake()
        {
            touchPositionAction = _playerInput.actions.FindAction("TouchPosition");
            tap = _playerInput.actions.FindAction("Tap");
            
        }

        private void OnEnable()
        {
            tap.performed += Tap;
        }

        private void Tap(InputAction.CallbackContext obj)
        {
            Ray ray = Camera.main.ScreenPointToRay(touchPositionAction.ReadValue<Vector2>());
            if(Physics.Raycast(ray,out RaycastHit hit))
            {
                switch (obj.interaction)
                {
                    case TapInteraction:
                        Debug.Log("Tap");
                        if (hit.collider.TryGetComponent<ISelectable>(out ISelectable selectable))
                        {
                            selectable.Select();
                        }
                        break;
                    case HoldInteraction:
                        Debug.Log("Hold");
                        break;
                }
            }

            


        }

        private void TouchPosition(InputAction.CallbackContext obj)
        {
            var value = obj.ReadValue<Vector2>();
            var a = Camera.main.ScreenToWorldPoint(value);
            _transform.position = a;
            Debug.Log(a);   
        }

        private void OnDisable()
        {
            tap.performed -= Tap;
        }
    }
}
