using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class Input : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool closeCamera;
    //////////////////////////////////////////////////
    #if ENABLE_INPUT_SYSTEM
    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }
    public void OnLook(InputValue value)
    {
        LookInput(value.Get<Vector2>());
    }
    public void OnCloseCamera(InputValue value)
    {
        CloseCameraInput(value.isPressed);
    }
    #endif
    ///////////////////////////////////////////////////
    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }
    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }
    public void CloseCameraInput(bool newCameraCloseState)
    {
        closeCamera = newCameraCloseState;
    }
}
