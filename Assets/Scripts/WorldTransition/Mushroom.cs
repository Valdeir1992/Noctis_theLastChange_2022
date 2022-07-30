using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Mushroom : WorldTranstionObject
{
    public MeshRenderer[] meshRenders;
    [SerializeField] private MushroomTransition _mushroomTransition;
    [SerializeField] private Light _mushroomLight;
    [SerializeField] private Color32 _dreamLightColor;
    [SerializeField] private Color32 _nightmareLightColor;

    private void Awake()
    {
        meshRenders = transform.GetComponentsInChildren<MeshRenderer>(); 
    }

    private void Start()
    {
        Invoke(nameof(this.TriggerChange), 1);
    }

    private void TriggerChange()
    {
        ChangeWorld(World.NIGHTMARE);
    }

    protected override IEnumerator Coroutine_End(World world)
    {
        yield break;
    }

    protected override IEnumerator Coroutine_Start(World world)
    {
        yield break;
    }

    protected override IEnumerator Coroutine_Update(World world)
    {
        float coroutineTime = 0;
        float totalTime = 1;
        Color32 startColor = (world == World.DREAM) ? _dreamLightColor : _nightmareLightColor;
        Color32 endColor = (world == World.DREAM) ? _nightmareLightColor : _dreamLightColor;

        while (true)
        {
            coroutineTime += Time.deltaTime;

            float time = coroutineTime / totalTime;

            _mushroomLight.color = Color.Lerp(startColor, endColor, time); 

            if(time >= 1)
            {
                break;
            }
            yield return null;
        }
        _world = (world == World.DREAM) ? World.NIGHTMARE : World.DREAM;
        yield break;
    }
}

