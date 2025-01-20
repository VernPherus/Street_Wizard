using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = System.Numerics.Vector2;

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

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public int SwitchWeaponsInput { get; private set; }
    public Vector2 RuneInputsInput { get; private set; }
    public Vector2 InteractInput { get; private set; }
    public float FireInput { get; private set; }
    public float CrouchInput { get; private set; }
    public bool ConjureInput { get; private set; }
    public float SprintInput { get; private set; }
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
    }

    void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.Zero;

        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => LookInput = Vector2.Zero;


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
