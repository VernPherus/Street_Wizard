using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{

    [Header("Settings")]
    private CharacterController controller;
    private PlayerControls playerInputActions;

    [Header("Movement")]
    private float playerSpeed;
    private float walkSpeed = 5.5f;
    private float sprintSpeed = 7.5f;
    private float crouchSpeed = 4.5f;
    Vector3 velocity;


    [Header("Jump")]
    private float gravity = -9.81f;
    public float jumpHeight = 3f;
    [SerializeField] private bool isGrounded = false;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Crouch")]
    public float transformToCrouchSpeed = 2f;
    public float crouchYscale;
    public float startYscale;
    private float bodyCrouchHeight = 0.5f;
    private float bodyNormalHeight = 2f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        playerInputActions = new PlayerControls();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.Move.performed += MovementPerformed;

    }

    private void OnEnable() {
    }

    private void MovementPerformed(InputAction.CallbackContext context){
        Debug.Log(context);
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        Vector3 move = transform.right * inputVector.x + transform.forward * inputVector.y;
        // Move player model
        controller.Move(move * walkSpeed * Time.deltaTime);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.performed)
        {
            Debug.Log("Jump" + context.phase);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        Vector3 move = transform.right * inputVector.x + transform.forward * inputVector.y;
        // Move player model
        controller.Move(move * walkSpeed * Time.deltaTime);

        Gravity();
    }

    private void Gravity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
