using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]
public class CharacterController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 2f;

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _coyoteTime = 0.15f;
    [SerializeField] private float _jumpBufferTime = 0.15f;

    [Header("ground Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRad = 0.1f;
    [SerializeField] private LayerMask _groundLayer;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb2d;
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _jumpAction;

    private Vector2 _moveInput = Vector2.zero;
    private bool _isGrounded = false;
    private float _lastGroundedTime = -10f;
    private float _lastJumpPressedTime = -10f;

    private bool _isFacingRight = true;

    private void Reset()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        _rb2d = _rb2d ? _rb2d : GetComponent<Rigidbody2D>(); 
        _playerInput = _playerInput ? _playerInput : GetComponent<PlayerInput>();
        _animator = _animator ? _animator : GetComponent<Animator>();
        _spriteRenderer = _spriteRenderer ? _spriteRenderer : GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if (_playerInput != null && _playerInput.actions != null)
        {
            _moveAction = _playerInput.actions["Move"];
            _jumpAction = _playerInput.actions["Jump"];

            if (_moveAction != null) _moveAction.Enable();
            if (_jumpAction != null) 
            { 
                _jumpAction.Enable();
                _jumpAction.performed += OnJumpPerformed;
            }
        }
    }
    private void OnDisable()
    {
        if (_moveAction != null) _moveAction.Disable();
        if (_jumpAction != null)
        { 
            _jumpAction.Disable();
            _jumpAction.performed -= OnJumpPerformed;
        }
    }

    private void Update()
    {
        if (_moveInput != null) _moveInput = _moveAction.ReadValue<Vector2>();
        else _moveInput = Vector2.zero;

        _animator.SetBool("IsRunning", _moveInput.x != 0);

        if (_moveInput.x > 0)
            _isFacingRight = true;
        else
            _isFacingRight = false;

        _spriteRenderer.flipX = !_isFacingRight;

        if (_groundCheck != null)
        {
            bool wasGrounded = _isGrounded;
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRad, _groundLayer);
            if (_isGrounded) _lastGroundedTime = Time.time;
        }
    }

    private void FixedUpdate()
    {
        Vector2 linearVel = _rb2d.linearVelocity;
        linearVel.x = _moveInput.x * _moveSpeed;
        _rb2d.linearVelocity = linearVel;

        bool CanUseCoyote = (Time.time - _lastGroundedTime) <= _coyoteTime;
        bool hasBufferedJump = (Time.time - _lastJumpPressedTime) <= _jumpBufferTime;

        if (CanUseCoyote && hasBufferedJump)
        {
            DoJump();
            _lastJumpPressedTime = -10f;
        }
    }
    private void DoJump()
    {
        _rb2d.linearVelocity = new Vector2(_rb2d.linearVelocity.x, 0f);
        _rb2d.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        _lastJumpPressedTime = Time.time;
    }
}