using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeonoraCharacterMediator : MonoBehaviour
{
    private InputsPlayer _inputs;
    private MoveController _moveController;
    private CameraController _cameraController;
    [SerializeField] private LeonoraData _data;
    [SerializeField] private Animator _stateMachine; 

    private void Awake()
    {
        _inputs = new InputsPlayer();

        _moveController = GetComponent<MoveController>();
        _cameraController = GetComponent<CameraController>();
    }
    private void OnEnable()
    {
        _inputs.Enable();
    }
    private void Update()
    {
        UpdateStateMachine();
    }

    private void FixedUpdate()
    {
        _moveController.Move(_inputs.Leonora.MoveDirection.ReadValue<Vector2>(), (_stateMachine.GetFloat("Velocity")> 0.5f)?_data.RunSpeed:_data.WalkSpeed);

        Vector2 camera = _inputs.Leonora.Camera.ReadValue<Vector2>();
        camera *= Time.fixedDeltaTime/2;
        _cameraController.UpdateCamera(camera.x * _data.MouseSensibilityX, (camera.y/3) * _data.MouseSensiblityY * ((_data.InvertY)?-1:1));
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }
    private void UpdateStateMachine()
    { 
        Vector2 move = _inputs.Leonora.MoveDirection.ReadValue<Vector2>(); 

        float velocity = move.magnitude;
        _stateMachine.SetFloat("Velocity", velocity);

        _stateMachine.SetBool("Move", !Mathf.Approximately(velocity, 0));

        bool Jump = _inputs.Leonora.ActionOne.ReadValue<float>() >= 1;
        _stateMachine.SetFloat("JumpForce", _data.JumpHeight);
        _stateMachine.SetBool("Jump", Jump); 
    }
}
