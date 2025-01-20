
// Centralized Input controller to make referencing inputs easier

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map References")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string look = "Look";
    [SerializeField] private string switchWeapons = "SwitchWeapons";
    [SerializeField] private string runeInputs = "RuneInputs";
    [SerializeField] private string interact = "Interact";
    [SerializeField] private string fire = "Fire";
    [SerializeField] private string crouch = "Crouch";
    [SerializeField] private string conjure = "Conjure";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string jump = "Jump";

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction switchWeaponsAction;
    private InputAction runeInputsAction;
    private InputAction interactAction;
    private InputAction fireAction;
    private InputAction crouchAction;
    private InputAction conjureAction;
    private InputAction sprintAction;
    private InputAction jumpAction;

    // Check input property
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public int SwitchWeaponsInput { get; private set; }
    public Vector2 RuneInputsInput { get; private set; }
    public bool InteractTriggered { get; private set; }
    public float FireValue { get; private set; }
    public float CrouchValue { get; private set; }
    public bool ConjureTriggered { get; private set; }
    public float SprintValue { get; private set; }
    public bool JumpTriggered { get; private set; }

    public static playerInputHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Find the action
        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
        switchWeaponsAction = playerControls.FindActionMap(actionMapName).FindAction(switchWeapons);
        runeInputsAction = playerControls.FindActionMap(actionMapName).FindAction(runeInputs);
        interactAction = playerControls.FindActionMap(actionMapName).FindAction(interact);
        fireAction = playerControls.FindActionMap(actionMapName).FindAction(fire);
        crouchAction = playerControls.FindActionMap(actionMapName).FindAction(crouch);
        conjureAction = playerControls.FindActionMap(actionMapName).FindAction(conjure);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);

        RegisterInputActions();
    }

    void RegisterInputActions()
    {

        // Register action when performed or canceled

        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => LookInput = Vector2.zero;

        switchWeaponsAction.performed += context => SwitchWeaponsInput = context.ReadValue<int>();
        switchWeaponsAction.canceled += context => SwitchWeaponsInput = 0;

        runeInputsAction.performed += context => RuneInputsInput = context.ReadValue<Vector2>();
        runeInputsAction.canceled += context => RuneInputsInput = Vector2.zero;

        interactAction.performed += context => InteractTriggered = true;
        interactAction.canceled += context => InteractTriggered = false;

        fireAction.performed += context => FireValue = context.ReadValue<float>();
        fireAction.canceled += context => FireValue = 0f;

        crouchAction.performed += context => CrouchValue = context.ReadValue<float>();
        crouchAction.canceled += context => CrouchValue = 0f;

        sprintAction.performed += context => SprintValue = context.ReadValue<float>();
        sprintAction.canceled += context => SprintValue = 0f;

        jumpAction.performed += context => SprintValue = context.ReadValue<float>();
        jumpAction.canceled += context => SprintValue = 0f;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        switchWeaponsAction.Enable();
        runeInputsAction.Enable();
        interactAction.Enable();
        fireAction.Enable();
        crouchAction.Enable();
        sprintAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        switchWeaponsAction.Disable();
        runeInputsAction.Disable();
        interactAction.Disable();
        fireAction.Disable();
        crouchAction.Disable();
        sprintAction.Disable();
        jumpAction.Disable();
    }
}
