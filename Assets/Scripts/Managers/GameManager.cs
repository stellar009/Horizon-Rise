using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerInputActions playerInputActions { get; private set; }
    public Vector2 moveInput { get; private set; }
    public Vector2 lookInput { get; private set; }

    private bool cursorStatus;

    [HideInInspector] public bool currentCursorState = false;
    [HideInInspector] public bool gamePauseStatus;
    public HelpSystem help;
    

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        playerInputActions = new PlayerInputActions();
        moveInput = Vector2.zero;
        lookInput = Vector2.zero;

        if(!help)
        {
            Debug.Log("Help system not found");
        }
    }

    private void Start()
    {
        ActivateCursor(currentCursorState);
        gamePauseStatus = false;
        UpdateCameraControls();
    }

    private void OnEnable()
    {
        playerInputActions.GameState.Enable();
        playerInputActions.Player.Enable();
        playerInputActions.Camera.Enable();

        playerInputActions.GameState.UseCursor.performed += CursorStatus;

        playerInputActions.Player.Movement.performed += MovementPerformed;
        playerInputActions.Player.Movement.canceled += MovementCancel;

        playerInputActions.Player.HelpMenu.performed += OpenHelpMenu;

        playerInputActions.Camera.Look.performed += LookPerform;
        playerInputActions.Camera.Look.canceled += LookCancel;
    }

    private void OnDisable()
    {
        playerInputActions.GameState.Disable();
        playerInputActions.Player.Disable();
        playerInputActions.Camera.Disable();

        playerInputActions.GameState.UseCursor.performed -= CursorStatus;

        playerInputActions.Player.Movement.performed -= MovementPerformed;
        playerInputActions.Player.Movement.canceled -= MovementCancel;

        playerInputActions.Player.HelpMenu.performed -= OpenHelpMenu;

        playerInputActions.Camera.Look.performed -= LookPerform;
        playerInputActions.Camera.Look.canceled -= LookCancel;
    }

    void CursorStatus(InputAction.CallbackContext ctx)
    {
        cursorStatus = ctx.ReadValueAsButton();
        ActivateCursor(cursorStatus);
        UpdateCameraControls();
    }

    public void ActivateCursor(bool cursorStatus)
    {
        Cursor.visible = cursorStatus;
        Cursor.lockState = cursorStatus ? CursorLockMode.None : CursorLockMode.Locked;
        currentCursorState = cursorStatus;
    }

    private void UpdateCameraControls()
    {
        if(currentCursorState || gamePauseStatus)
        {
            DisableCameraAndMovementControls();
        }
        else
        {
            EnableCameraAndMovementControls();
        }
    }

    public void DisableCameraAndMovementControls()
    {
        playerInputActions.Camera.Disable();
        playerInputActions.Player.Disable();
    }

    public void EnableCameraAndMovementControls()
    {
        playerInputActions.Camera.Enable();
        playerInputActions.Player.Enable();
    }

    void MovementPerformed(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    void MovementCancel(InputAction.CallbackContext ctx)
    {
        moveInput = Vector2.zero;
    }

    void LookPerform(InputAction.CallbackContext ctx)
    {
        lookInput = ctx.ReadValue<Vector2>();
    }

    void LookCancel(InputAction.CallbackContext ctx)
    {
        lookInput = Vector2.zero;
    }

    void OpenHelpMenu(InputAction.CallbackContext ctx)
    {
        ctx.ReadValueAsButton();
        help.OpenHelpMenu();
    }

    public bool IsInputEnabled()
    {
        return !gamePauseStatus && !currentCursorState;
    }
}
