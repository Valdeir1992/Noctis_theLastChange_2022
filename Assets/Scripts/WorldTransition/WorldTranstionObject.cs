using System.Collections;
using UnityEngine;

public abstract class WorldTranstionObject : MonoBehaviour
{
    [SerializeField] protected World _world;
    public void ChangeWorld(World world)
    {
        StartCoroutine(Coroutine_World(world));
    }

    public IEnumerator Coroutine_World(World world)
    {
        yield return Coroutine_Start(world);
        yield return Coroutine_Update(world);
        yield return Coroutine_End(world);
    }

    protected abstract IEnumerator Coroutine_Start(World world);
    protected abstract IEnumerator Coroutine_Update(World world);
    protected abstract IEnumerator Coroutine_End(World world);


}

