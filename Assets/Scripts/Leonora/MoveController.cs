using UnityEngine;

public abstract class MoveController : MonoBehaviour
{ 
    private float _gravity;
    private float _minGravity = -10;
    private float _velocityY;
    private bool _isGrounded;

    [SerializeField] private Animator _stateMachine;

    [SerializeField] protected CharacterController _moveController;
    

    private void Update()
    {
        if(_stateMachine.GetBool("Jump") && _isGrounded)
        {
            JumpAction();
        }

        ApplyGravity(); 
        _isGrounded = _moveController.isGrounded;
        _stateMachine.SetBool("Ground",_isGrounded);

        if (!_isGrounded)
        {
            _stateMachine.SetTrigger("Landing");
        }
    } 

    protected virtual void ApplyGravity()
    {
        if (!_isGrounded && _gravity >= _minGravity)
        { 
            _gravity += Physics.gravity.y * Time.fixedDeltaTime;
        } 
        _moveController.Move(new Vector3(0, _gravity, 0)*Time.fixedDeltaTime * 0.8f);
    }

    protected void JumpAction()
    {
        _gravity = Mathf.Sqrt(Physics.gravity.y * -2 * _stateMachine.GetFloat("JumpForce"));

    }

    public abstract void Move(Vector2 direction, float velocity);
}
