using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class PlayerInputs : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    
    #if ENABLE_INPUT_SYSTEM
    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }
    #endif


    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }
}
