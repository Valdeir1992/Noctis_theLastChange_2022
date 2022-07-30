using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public sealed class IconWorldSystem : MonoBehaviour
{
    [SerializeField] private Canvas _worldCanvas;
    private void Awake()
    {
        ConfigureIcons();
    }

    private async void ConfigureIcons()
    {
        await Task.Delay(500);

        IIconWorld[] icons = FindObjectsOfType<MonoBehaviour>().OfType<IIconWorld>()
            .Select(icon =>
            {
                icon.Config(_worldCanvas);
                return icon;
            }).ToArray();
    }
}
  
