using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerFPSController : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 2.0f;
    [SerializeField] private float sprintMultiplier = 6.0f;
    [SerializeField] private float crouchReduction = 0.5f;
    [SerializeField] private float dashSpeed = 10.0f;
    [SerializeField] private float dashTime = 0.25f;

    [Header("Jump Params")]
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravity = 9.81f;

    [Header("Look Sens")]
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private float upDownRange = 90.0f;

    private CharacterController characterController;
    private Camera mainCam;
    private PlayerInputHandler inputHandler;

    private Vector3 currentMovement;
    private float verticalRotation;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCam = Camera.main;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        inputHandler = PlayerInputHandler.Instance;
        if (inputHandler == null)
        {
            Debug.Log("Player input handler is not assigned!");
            return;
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleRotation()
    {
        float mouseXRotation = inputHandler.LookInput.x * mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= inputHandler.LookInput.y * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);

        mainCam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

    }

    private void HandleMovement()
    {
        // if sprint value is greater than 1, we are sprinting
        float speed = walkSpeed * (inputHandler.SprintTriggered > 0.0 ? sprintMultiplier : 1f);

        Vector3 inputDirection = new Vector3(inputHandler.MoveInput.x, 0f, inputHandler.MoveInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);

        worldDirection.Normalize();

        currentMovement.x = worldDirection.x * speed;
        currentMovement.z = worldDirection.z * speed;

        HandleJumping();
        HandleDash();

        characterController.Move(currentMovement * Time.deltaTime);
    }

    private void HandleJumping()
    {
        if (characterController.isGrounded)
        {
            currentMovement.y = -0.5f;

            if (inputHandler.JumpTriggered)
            {
                currentMovement.y = jumpForce;
            }
        }
        else
        {
            currentMovement.y -= gravity * Time.deltaTime;
        }
    }

    private void HandleDash()
    {
        if (inputHandler.DashTriggered)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;
        Debug.Log("Player Dashed");

        while (Time.time < startTime + dashTime)
        {
            characterController.Move(dashSpeed * Time.deltaTime * currentMovement);

            yield return null;
        }
        
    }

    private void Crouch()
    {

    }
}
