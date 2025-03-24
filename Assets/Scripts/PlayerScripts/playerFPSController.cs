// Summary: This script handles what the player does on input

// TODO: Clean up this code. All input actions should be placed here and logic that is specific to a function should be placed to another script

using System;
using UnityEngine;
using Managers;
using WeaponsScripts;

public class PlayerFPSController : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 2.0f;
    [SerializeField] private float sprintMultiplier = 6.0f;

    [Header("Jump Params")]
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravity = 9.81f;

    [Header("Look Sens")]
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private float upDownRange = 90.0f;

    [Header("Weapon params")]
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private playerSummonInput summonInput;
    [SerializeField] private SummonManager summonManager;
    [SerializeField] private WeaponSwayNBob weaponSwayNBob;
    [SerializeField] private PauseScreen pauseUI;


    public event Action OnBeforeMove;

    private CharacterController characterController;
    private Camera mainCam;
    private PlayerInputHandler inputHandler;
    private PlayerDashScript playerDash;


    private Vector3 currentMovement;
    private float verticalRotation;
    private float mouseXRotation;
    private bool canDash;
    private bool canImbue;
    public float playerHeight;
    public bool IsMouseLookEnabled;

    public CharacterController Player { get; set; }
    public Camera PlayerCamera { get; set; }

    public static PlayerFPSController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("PlayerFPSController instance Assigned");
        }
        else
        {
            Debug.Log("Duplicate PlayerFPSController detected, destroying...");
            Destroy(gameObject);
        }

        characterController = GetComponent<CharacterController>();
        mainCam = Camera.main;

        Player = characterController;
        PlayerCamera = mainCam;
        weaponManager = GetComponent<WeaponManager>();
        summonInput = GetComponent<playerSummonInput>();
        summonManager = GetComponent<SummonManager>();

        inputHandler = PlayerInputHandler.Instance;
        if (inputHandler == null)
        {
            Debug.Log("Player input handler is not assigned!");
            return;
        }
    }

    private void Start()
    {
        playerDash = GetComponent<PlayerDashScript>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        canDash = true;
        canImbue = true;
        EnableMouseLook();
        playerHeight = characterController.height;
    }

    private void Update()
    {
        HandleMovement();
        
        if (IsMouseLookEnabled)
        {
            HandleRotation();
        }

        HandleAttack();
        HandleSummonInput();
        HandleWeaponSwitching();
        HandlePause();
    }

    // #############################################################################################################
    //* ## Mouse Logic ##        
    // #############################################################################################################


    private void HandleRotation()
    {


        mouseXRotation = inputHandler.LookInput.x * mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= inputHandler.LookInput.y * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);

        mainCam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        // for weapon sway
        weaponSwayNBob.Sway(inputHandler.LookInput);
        weaponSwayNBob.SwayRotation(inputHandler.LookInput);
    }

    // #############################################################################################################
    //* ## Movement Logic ##        
    // #############################################################################################################

    private void HandleMovement()
    {
        OnBeforeMove?.Invoke();
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

        // for weapon bob
        weaponSwayNBob.BobOffset(worldDirection);
        weaponSwayNBob.BobRotation(worldDirection);
    }

    public void Teleport(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        Physics.SyncTransforms();
        Debug.Log("Teleporting");

    }

    // #############################################################################################################
    //* ## Jumping Logic ##        
    // #############################################################################################################

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

    // #############################################################################################################
    //* ## Dash Logic ##        
    // #############################################################################################################

    private void HandleDash()
    {

        if (inputHandler.DashTriggered && canDash)
        {
            Debug.Log($"Magnitude: {currentMovement.magnitude}");

            Vector3 dashDirection;

            if (inputHandler.MoveInput.sqrMagnitude > 0.01f)
            {
                dashDirection = new Vector3(inputHandler.MoveInput.x, 0, inputHandler.MoveInput.y);
                dashDirection = transform.TransformDirection(dashDirection).normalized;
            }
            else
            {
                dashDirection = transform.forward;
            }

            Debug.Log($"Current Direction: {dashDirection.x}, {dashDirection.y}, {dashDirection.z} | Magnitude: {dashDirection.magnitude}");

            playerDash.HandleDash(dashDirection);
            canDash = false;
        }

        if (!inputHandler.DashTriggered) canDash = true;
    }

    public int GetDashNumber()
    {
        return playerDash.GetDashNumber();
    }

    // #############################################################################################################
    //* ## Combat Logic ##        
    // #############################################################################################################

    // Handle attack input
    private void HandleAttack()
    {
        if (weaponManager.ActiveWeapon != null)
        {
            weaponManager.ActiveWeapon.Tick(inputHandler.FireTriggered);
        }
    }

    private void HandleSummonInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) summonInput.TranslateInput(Direction.up);
        if (Input.GetKeyDown(KeyCode.DownArrow)) summonInput.TranslateInput(Direction.down);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) summonInput.TranslateInput(Direction.left);
        if (Input.GetKeyDown(KeyCode.RightArrow)) summonInput.TranslateInput(Direction.right);

        if (inputHandler.CrouchValue != 0)
        {
            Debug.Log("Triggered despawn rune");
            summonInput.DespawnRune();
        }

        if (inputHandler.ConjureTriggered && canImbue)
        {
            summonManager.Imbue();

            Debug.Log("Conjure Triggered");
            canImbue = false;
        }
        if (!inputHandler.ConjureTriggered) canImbue = true;
    }

    private void HandleWeaponSwitching()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            weaponManager.SwitchWeapon(scroll > 0 ? 1 : -1);
        }

        // // Number key input (1-9)
        // for (int i = 0; i < 9; i++)
        // {
        //     if (Input.GetKeyDown((i + 1).ToString()))
        //     {
        //         weaponManager.SwitchWeaponByIndex(i);
        //     }
        // }
    }

    private void HandlePause()
    {
        if (inputHandler.EscapteTriggered)
        {
            pauseUI.PauseGame();
            DisableMouseLook();
        }
    }

    public void DisableMouseLook()
    {
        IsMouseLookEnabled = false;
    }

    public void EnableMouseLook()
    {
        IsMouseLookEnabled = true;
    }

}
