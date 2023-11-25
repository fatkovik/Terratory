using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Base.Input
{
     public class InputHandler : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private LineRenderer _lineRenderer;

        private InputAction touchPositionAction;
        private InputAction touch;
        private InputAction delta;

        public Action<ISelectable> ObjectSelected;

        private void Awake()
        {
            touchPositionAction = _playerInput.actions.FindAction("TouchPosition");
            touch = _playerInput.actions.FindAction("Touch");
           // delta = _playerInput.actions.FindAction("Drag");
            
        }

        private void OnEnable()
        {
            touch.performed += TouchHandler;
        }

        private void TouchHandler(InputAction.CallbackContext obj)
        {
            Vector3 pos = touchPositionAction.ReadValue<Vector2>();
            pos.z = Camera.main.nearClipPlane;
            Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(pos);
            Vector2 rayDirection = Vector2.zero;

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection);

            if (hit.collider != null)
            {
                switch (obj.interaction)
                {
                    case TapInteraction:
                        Debug.Log("Tap");
                        if (hit.collider.TryGetComponent(out ISelectable selectable))
                        {
                            ObjectSelected?.Invoke(selectable);
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
            touch.performed -= TouchHandler;
        }
    }
}
