using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 4f;
    public float sprintSpeed = 8f;
    public float acceleration = 10f;
    public float deceleration = 10f;

    [Header("Rotación")]
    public float mouseSensitivity = 2f;
    public Transform cameraTransform;
    float cameraPitch = 0f;

    private CharacterController controller;
    private Vector3 currentVelocity;
    private Vector3 inputDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovementInput();
        MovePlayer();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Solo la cámara rota en vertical (X)
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -85f, 85f);
        cameraTransform.localEulerAngles = new Vector3(cameraPitch, 0f, 0f);

        // Solo el jugador rota horizontalmente (Y)
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        inputDirection = new Vector3(horizontal, 0f, vertical).normalized;
    }

    void MovePlayer()
    {
        float targetSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        // Movimiento en la dirección local del jugador, sin rotarlo
        Vector3 moveDir = (transform.right * inputDirection.x + transform.forward * inputDirection.z).normalized;
        Vector3 desiredVelocity = moveDir * targetSpeed;

        // Suavizado con aceleración/desaceleración
        if (inputDirection.magnitude > 0)
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, desiredVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);
        }

        controller.Move(currentVelocity * Time.deltaTime);
    }

}
