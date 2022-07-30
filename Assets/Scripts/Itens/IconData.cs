using UnityEngine;

[CreateAssetMenu(menuName ="Prototipo/Data/Icon")]
public sealed class IconData : ScriptableObject
{
    [SerializeField] private float _distanceIcon;
    [SerializeField] private float _distanceButton;  
    [SerializeField] private float _totalTime;
    [SerializeField] private Vector2 _sizeIcon;
    [SerializeField] private Vector2 _sizebutton;
    [SerializeField] private Vector2 _buttonPosition;

    public float DistanceIconSqr { get => _distanceIcon * _distanceIcon;}
    public float DistanceButtonSqr { get => _distanceButton * _distanceButton;}  
    public float TotalTime { get => _totalTime;}
    public Vector2 SizeIcon { get => _sizeIcon;}
    public Vector2 Sizebutton { get => _sizebutton;}
    public Vector2 ButtonPosition { get => _buttonPosition;}
}
  
