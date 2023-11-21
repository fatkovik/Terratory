using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Base.Input
{
     public class InputHandler : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;

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
            Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(touchPositionAction.ReadValue<Vector2>());
            Vector2 rayDirection = Vector2.zero;

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection);

            if (hit.collider != null)
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

        private void OnDisable()
        {
            tap.performed -= Tap;
        }
    }
}
