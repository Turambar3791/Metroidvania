using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput instance;

    public Vector2 MoveInput {  get; private set; }
    public bool JumpJustPressed { get; private set; }
    public bool JumpReleased { get; private set; }
    public bool AttackInput { get; private set; }
    public bool MenuOpenCloseInput { get; private set; }

    private PlayerInput _playerInput;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _attackAction;
    private InputAction _menuOpenCloseAction;

    void Start()
    {
        SetupInputActions();
        MoveInput = _moveAction.ReadValue<Vector2>();
        JumpJustPressed = _jumpAction.WasPressedThisFrame();
        JumpReleased = _jumpAction.WasReleasedThisFrame();
        AttackInput = _attackAction.WasPressedThisFrame();
        MenuOpenCloseInput = _menuOpenCloseAction.WasPressedThisFrame();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _playerInput = GetComponent<PlayerInput>();

        SetupInputActions();
    }

    private void Update()
    {
        UpdateInputs();
    }

    private void SetupInputActions()
    {
        _moveAction = _playerInput.actions["Move"];
        _jumpAction = _playerInput.actions["Jump"];
        _attackAction = _playerInput.actions["Attack"];
        _menuOpenCloseAction = _playerInput.actions["MenuOpenClose"];
        Debug.Log(_moveAction);
    }

    private void UpdateInputs()
    {
        MoveInput = _moveAction.ReadValue<Vector2>();
        JumpJustPressed = _jumpAction.WasPressedThisFrame();
        JumpReleased = _jumpAction.WasReleasedThisFrame();
        AttackInput = _attackAction.WasPressedThisFrame();
        MenuOpenCloseInput = _menuOpenCloseAction.WasPressedThisFrame();
    }
}
