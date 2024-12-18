using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float sprintSpeed = 5.0f;
    
    [Range(0.0f, 0.3f)]
    public float rotationSpeed = 0.12f;
    public float speedChangeMultiplier = 10.0f;
    
    public AudioClip footstepAudio;
    [Range(0, 1)] public float footstepVolume = 0.5f;
    
    public GameObject cameraTarget;
    public float maxPitch = 70.0f;
    public float minPitch = -30.0f;
    private float _yaw;
    private float _pitch;
    
    private float _speed;
    private float _animationBlend;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    
    public Animator animator;
    private int _animIDSpeed;
    private int _animIDMotionSpeed;
    
    private PlayerInput _playerInput;
    private CharacterController _controller;
    private InputHandler _input;
    public GameObject mainCamera;
    
    private void Awake()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }

    private void Start()
    {
        _yaw = cameraTarget.transform.rotation.eulerAngles.y;
        
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<InputHandler>();
        _playerInput = GetComponent<PlayerInput>();
        
        if (_input.cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }
    
    private void CameraRotation()
    {
        if (_input.look.sqrMagnitude >= 0.01f)
        {
            _yaw += _input.look.x;
            _pitch += _input.look.y;
        }
        
        _yaw = ClampAngle(_yaw, float.MinValue, float.MaxValue);
        _pitch = ClampAngle(_pitch, minPitch, maxPitch);
        
        cameraTarget.transform.rotation = Quaternion.Euler(_pitch, _yaw, 0.0f);
    }

    private void Move()
    {
        float targetSpeed = _input.sprint ? sprintSpeed : moveSpeed;
        if (_input.move == Vector2.zero) targetSpeed = 0.0f;
        
        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;
        float speedOffset = 0.1f;
        
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * _input.move.magnitude, Time.deltaTime * speedChangeMultiplier);
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * speedChangeMultiplier);
        if (_animationBlend < 0.01f) _animationBlend = 0f;
        
        Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;
        
        _targetRotation = mainCamera.transform.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, rotationSpeed);
        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * inputDirection;
        
        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        
        animator.SetFloat(_animIDSpeed, _animationBlend);
        animator.SetFloat(_animIDMotionSpeed, _input.move.magnitude);
    }
    
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
    
    private void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            AudioSource.PlayClipAtPoint(footstepAudio, transform.TransformPoint(_controller.center), footstepVolume);
        }
    }
}