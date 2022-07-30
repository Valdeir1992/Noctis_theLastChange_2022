using UnityEngine;

[CreateAssetMenu(menuName ="Noctis/Data/IconId")]
public class IconId : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _button;
    public string Id { get => _id;}
    public string Button { get => _button;}
}
