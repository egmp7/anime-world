using Cinemachine;
using UnityEngine;

public class CloseCameraController : MonoBehaviour
{
    [Tooltip("Input CS")]
    [SerializeField] Input _input; 

    private CinemachineVirtualCamera vc;
    private void Start()
    {
        vc = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (_input.closeCamera)vc.Priority = 11;
        else vc.Priority = 1;
    }
}
