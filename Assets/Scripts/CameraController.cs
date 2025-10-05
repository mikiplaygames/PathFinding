using UnityEngine;
using UnityEngine.InputSystem;

namespace PathfindingDemo
{
    public class CameraController : MonoBehaviour
    {
        [Header("Move Settings")]
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float rotateSpeed = 10f;

        Control control;
        float xRotation, yRotation;
        void Awake()
        {
            control = new();
            xRotation = transform.localRotation.eulerAngles.x;
            yRotation = transform.localRotation.eulerAngles.y;
        }
        void OnEnable()
        {
            control.Enable();
            control.Camera.Locked.performed += LockedPerformed;
        }
        void OnDisable()
        {
            control.Camera.Locked.performed -= LockedPerformed;
            if (Cursor.lockState == CursorLockMode.None)
                control.Camera.Rotate.performed -= RotateCamera;
            control.Disable();
        }
        private void LockedPerformed(InputAction.CallbackContext context)
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                control.Camera.Rotate.performed -= RotateCamera;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                control.Camera.Rotate.performed += RotateCamera;
            }
        }
        private void RotateCamera(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<Vector2>();
            input *= rotateSpeed * Time.deltaTime;

            xRotation -= input.y;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            yRotation += input.x;

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        }
        void Update()
        {
            MoveCamera();
        }
        void MoveCamera()
        {
            var cameraMoveInput = control.Camera.Move.ReadValue<Vector2>();
            var forwardMovement = cameraMoveInput.y * transform.forward;
            var rightMovement = cameraMoveInput.x * transform.right;
            Vector3 movement = moveSpeed * Time.deltaTime * (forwardMovement + rightMovement).normalized;
            transform.position += movement;
        }
        void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 300, 20), $"Press 'SPACE' to {(Cursor.lockState == CursorLockMode.Locked ? "" : "un")}lock the camera");
        }
    }
}
