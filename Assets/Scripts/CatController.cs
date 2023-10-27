using UnityEngine;

public class CatController : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] Transform playerTransform;
    
    [Header("Cat")]
    [Tooltip("Cat moving speed towards target")]
    [SerializeField] float MoveSpeed = 0.7f;

    [Tooltip("Rotation speed when cat starts following target")]
    [SerializeField] float RotationSpeed = 0.7f;

    [Tooltip("Max distance for cat to follow player")]
    [SerializeField] float maxDistance = 1.5f;

    // animations
    private Animator _animator;
    private int _animIDIsFollowing;

     private void Start()
    {
        _animator = GetComponent<Animator>();
        AssignAnimationIDs();
    }

    private void AssignAnimationIDs()
    {
        _animIDIsFollowing = Animator.StringToHash("isFollowing");
    }

    private void Update()
    {
        
        // distance between player and cat
        Vector3 catPos = new Vector3(transform.position.x,0,transform.position.z);
        Vector3 playerPos = new Vector3(playerTransform.position.x,0,playerTransform.position.z);
        float distance = Vector3.Distance(catPos,playerPos);

        if (distance > maxDistance) 
        {
            // look at player
            Vector3 direction = playerPos - catPos;
            Quaternion toRotation = Quaternion.LookRotation(direction, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, RotationSpeed * Time.deltaTime);
            
            // move
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            // animation
            _animator.SetBool(_animIDIsFollowing, true);
        }
        else _animator.SetBool(_animIDIsFollowing, false);
        
    }
}
