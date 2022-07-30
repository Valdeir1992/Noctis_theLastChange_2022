using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyMoveController : MonoBehaviour
{
    private Transform _target;
    [SerializeField] private AnimationCurve _curveY;
    // Start is called before the first frame update

    private void Awake()
    {
        _target = FindObjectOfType<LeonoraCharacterMediator>().transform;
        _curveY.postWrapMode = WrapMode.PingPong;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(_target.position.x, _curveY.Evaluate(Time.time*0.6f)/10 + 1.7f, _target.position.z) + transform.forward * 0.6f;
    }
}
