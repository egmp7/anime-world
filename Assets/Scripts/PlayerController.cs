using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [Tooltip("Move speed of the character in m/s")]
    public float MoveSpeed = 2.0f;

    // animation IDs
    private int _animIDIsMoving;

    private CharacterController _controller;
    private Animator _animator;
    #if ENABLE_INPUT_SYSTEM 
    private Input _input;
    #endif

    void Start()
    {
        _input = GetComponent<Input>();
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        AssignAnimationIDs();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void AssignAnimationIDs()
        {
            _animIDIsMoving = Animator.StringToHash("isMoving");
        }

    void Move()
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
        if (_input.move == Vector2.zero) _animator.SetBool(_animIDIsMoving,false);
        else _animator.SetBool(_animIDIsMoving,true);

    }
}
