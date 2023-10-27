using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [Tooltip("Move speed of the character in m/s")]
    public float MoveSpeed = 2.0f;

    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    [SerializeField] GameObject CinemachineCameraTarget;

    [Tooltip("How far in degrees can you move the camera up")]
    [SerializeField] float TopClamp = 70.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    [SerializeField] float BottomClamp = -30.0f;

    // animation IDs
    private int _animIDIsMoving;

    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    private Animator _animator;
    #if ENABLE_INPUT_SYSTEM
    private Input _input;
    #endif

    private void Start()
    {
        _input = GetComponent<Input>();
        _animator = GetComponent<Animator>();
        AssignAnimationIDs();
    }

    private void Update()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void AssignAnimationIDs()
    {
        _animIDIsMoving = Animator.StringToHash("isMoving");
    }

    private void Move()
    {
        // Inputs
        float forward = _input.move.y;
        float side = _input.move.x;

        Vector3 playerForward = transform.forward;
        Vector3 playerRight = transform.right;
        playerForward.y = 0;
        playerRight.y = 0;

        // Relative Positions
        Vector3 forwardRelative = playerForward * forward;
        Vector3 rightRelative = playerRight * side;

        // Movement
        Vector3 playerMovement = forwardRelative + rightRelative;
        transform.position += playerMovement * MoveSpeed * Time.deltaTime;

        // animation
        if (_input.move == Vector2.zero) _animator.SetBool(_animIDIsMoving, false);
        else _animator.SetBool(_animIDIsMoving, true);

    }

    private void CameraRotation()
    {
        // get input values
        _cinemachineTargetYaw += _input.look.x;
        _cinemachineTargetPitch += _input.look.y;

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch,_cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
