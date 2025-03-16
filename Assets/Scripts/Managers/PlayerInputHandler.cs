
// Centralized Input controller to make referencing inputs easier

using UnityEngine;
using UnityEngine.InputSystem;

public class    PlayerInputHandler : MonoBehaviour
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
    [SerializeField] private string crouch = "CtrlInput";
    [SerializeField] private string conjure = "Conjure";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string dash = "Dash";

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
    private InputAction dashAction;

    // Check input property
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public int SwitchWeaponsInput { get; private set; }
    public Vector2 RuneInput { get; private set; }
    public bool InteractTriggered { get; private set; }
    public bool FireTriggered { get; private set; }
    public float CrouchValue { get; private set; }
    public bool ConjureTriggered { get; private set; }
    public float SprintTriggered { get; private set; }
    public bool JumpTriggered { get; private set; }
    public bool DashTriggered { get; private set; }

    public static PlayerInputHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("PlayerInputHandler instance assigned.");
        }
        else
        {
            Debug.Log("Duplicate PlayerInputHandler detected, destroying...");
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
        dashAction = playerControls.FindActionMap(actionMapName).FindAction(dash);

        RegisterInputActions();
    }

    void RegisterInputActions()
    {

        // Register action when performed or canceled
        // Notes, please carefully check if you put the correct values in.

        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => LookInput = Vector2.zero;

        switchWeaponsAction.performed += context => SwitchWeaponsInput = context.ReadValue<int>();
        switchWeaponsAction.canceled += context => SwitchWeaponsInput = 0;

        runeInputsAction.performed += context => RuneInput = context.ReadValue<Vector2>();
        runeInputsAction.canceled += context => RuneInput = Vector2.zero;

        interactAction.performed += context => InteractTriggered = true;
        interactAction.canceled += context => InteractTriggered = false;

        fireAction.performed += context => FireTriggered = true;
        fireAction.canceled += context => FireTriggered = false;

        crouchAction.performed += context => CrouchValue = context.ReadValue<float>();
        crouchAction.canceled += context => CrouchValue = 0f;

        conjureAction.performed += context => ConjureTriggered = true;
        conjureAction.canceled += context => ConjureTriggered = false;

        sprintAction.performed += context => SprintTriggered = context.ReadValue<float>();
        sprintAction.canceled += context => SprintTriggered = 0f;

        jumpAction.performed += context => JumpTriggered = true;
        jumpAction.canceled += context => JumpTriggered = false;

        dashAction.performed += context => DashTriggered = true;
        dashAction.canceled += context => DashTriggered = false;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        switchWeaponsAction.Enable();
        runeInputsAction.Enable();
        interactAction.Enable();
        fireAction.Enable();
        conjureAction.Enable();
        crouchAction.Enable();
        sprintAction.Enable();
        jumpAction.Enable();
        dashAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        switchWeaponsAction.Disable();
        runeInputsAction.Disable();
        interactAction.Disable();
        fireAction.Disable();
        conjureAction.Disable();
        crouchAction.Disable();
        sprintAction.Disable();
        jumpAction.Disable();
        dashAction.Disable();
    }
}
