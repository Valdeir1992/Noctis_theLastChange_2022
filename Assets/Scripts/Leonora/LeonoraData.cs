using UnityEngine;

[CreateAssetMenu(menuName ="Noctis/Data/Leonora")]
public sealed class LeonoraData : ScriptableObject
{
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _mouseSensibilityX;
    [SerializeField] private float _mouseSensiblityY;
    [SerializeField] private bool _invertY;

    public float RunSpeed { get => _runSpeed;}
    public float WalkSpeed { get => _walkSpeed;}
    public float JumpHeight { get => _jumpHeight;}
    public float MouseSensibilityX { get => _mouseSensibilityX;}
    public float MouseSensiblityY { get => _mouseSensiblityY;}
    public bool InvertY { get => _invertY;}
}
