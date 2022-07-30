using UnityEngine;

public sealed class LeonoraMoveController : MoveController
{
    public override void Move(Vector2 direction, float velocity)
    {
        Vector3 finalDirection = direction;
        finalDirection = FixInput(direction);

        if(direction.sqrMagnitude > 0)
        { 
            _moveController.transform.forward = finalDirection;
        } 

        _moveController.Move(finalDirection * Time.fixedDeltaTime * velocity);
    }

    private Vector3 FixInput(Vector2 direction)
    {
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = forward.normalized;
        Vector3 right = Camera.main.transform.right;
        right.y = 0;
        right = right.normalized;

        Vector3 fixedVector = (direction.y * forward) + (direction.x * right);

        if(fixedVector.magnitude > 1)
        {
            fixedVector = fixedVector.normalized;
        }
        return fixedVector;
    }
}
