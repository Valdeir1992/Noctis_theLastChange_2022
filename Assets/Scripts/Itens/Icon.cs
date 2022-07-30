using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class Icon : MonoBehaviour, IIconWorld
{
    private bool _isVisible = true;
    private CharacterController _character;
    private MeshRenderer _myMesh;
    private CanvasGroup _iconInWorld;
    private CanvasGroup _buttonWorld;
    private IEnumerator _currentCoroutine;
    private IconData _data;
    [SerializeField] private float _overrideDistanceIcon;
    [SerializeField] private bool _isInteractable;
    [SerializeField] private string _name;
    [SerializeField] private Transform _mainTransform;
    [SerializeField] private float _height;
    [SerializeField] private IconId _id;

    private void Awake()
    {
        _character = FindObjectOfType<CharacterController>();
        _myMesh = _mainTransform.GetComponent<MeshRenderer>();
        _data = Resources.Load<IconData>("IconData");
    }
    private void Start()
    {
        InvokeRepeating(nameof(this.UpdateIconVisibilite), 0.5f, 0.05f);
    }

    private void UpdateIconVisibilite()
    { 

        _iconInWorld.transform.forward = -Camera.main.transform.forward; 

        if (_myMesh.isVisible)
        {
            Vector2 direction = (new Vector2(_mainTransform.transform.position.x, _mainTransform.transform.position.z)
           - new Vector2(_character.transform.position.x, _character.transform.position.z)
           ).normalized; 

            float sqrDistance = (_character.transform.position - transform.position).sqrMagnitude;
            float realDistance = (_overrideDistanceIcon != 0) ? _overrideDistanceIcon : _data.DistanceIconSqr;
            if (sqrDistance < realDistance)
            {
                Show();
            }
            else
            {
                Hidden();
            }
        }
    }
    public void Hidden()
    {
        if (!_isVisible) return;
        _isVisible = false;

        if (!System.Object.ReferenceEquals(_currentCoroutine, null))
        {
            StopCoroutine(_currentCoroutine);
        }
        _currentCoroutine = Coroutine_FadeOut();
        StartCoroutine(_currentCoroutine);
    }

    public void Show()
    {
        if (_isVisible) return;
        _isVisible = true;

        if (!System.Object.ReferenceEquals(_currentCoroutine, null))
        {
            StopCoroutine(_currentCoroutine);
        }
        _currentCoroutine = Coroutine_FadeIn();
        StartCoroutine(_currentCoroutine);
    }

    public void Config(Canvas canva)
    {
        GameObject icon = new GameObject("Icon");

        Image Imageicon = icon.AddComponent<Image>();
        Imageicon.sprite = Resources.Load<Sprite>(_id.Id);
        Imageicon.rectTransform.sizeDelta = _data.SizeIcon;

        icon.transform.SetParent(canva.transform);
        icon.transform.position += transform.position + Vector3.up * _height;
        icon.transform.localScale = Vector3.one * 0.01f;

        _iconInWorld = icon.AddComponent<CanvasGroup>();

        if (_isInteractable)
        {
            GameObject buttonIcon = new GameObject("Button");

            Image button = buttonIcon.AddComponent<Image>();
            button.sprite = Resources.Load<Sprite>(_id.Button);
            button.rectTransform.sizeDelta = _data.Sizebutton;

            buttonIcon.transform.SetParent(icon.transform);
            buttonIcon.transform.localScale = Vector3.one;
            buttonIcon.transform.localPosition = _data.ButtonPosition;

            _buttonWorld = buttonIcon.gameObject.AddComponent<CanvasGroup>();
            _buttonWorld.alpha = 0;

            StartCoroutine(Coroutine_ButtonFade());
        }
    } 

    private IEnumerator Coroutine_FadeIn()
    {
        float coroutineTime = 0;
        float alpha = _iconInWorld.alpha;
        while (true)
        {
            coroutineTime += Time.deltaTime;
            float time = coroutineTime / _data.TotalTime;
            _iconInWorld.alpha = Mathf.Lerp(alpha,1, time);
            if (time >= 1)
            {
                break;
            }
            yield return null;
        }
    }

    private IEnumerator Coroutine_FadeOut()
    {
        float coroutineTime = 0;
        float alpha = _iconInWorld.alpha;
        while (true)
        {
            coroutineTime += Time.deltaTime;
            float time = coroutineTime / _data.TotalTime;
            _iconInWorld.alpha = Mathf.Lerp(alpha, 0, time);
            if (time >= 1)
            {
                break;
            }
            yield return null;
        }
    }

    private IEnumerator Coroutine_ButtonFade()
    {
        bool buttonIsVisible = false;

        float sqrDistance = 0;

        while (true)
        {
            sqrDistance = (_character.transform.position - transform.position).sqrMagnitude;

            if (!buttonIsVisible && sqrDistance < _data.DistanceButtonSqr)
            {
                buttonIsVisible = true;
                yield return Coroutine_ButtonFadeIn();
            }
            else if (buttonIsVisible && sqrDistance >= _data.DistanceButtonSqr)
            {
                buttonIsVisible = false;
                yield return Coroutine_ButtonFadeOut();
            }

            yield return null;
        }
    }

    private IEnumerator Coroutine_ButtonFadeIn()
    {
        _buttonWorld.alpha = 1;
        yield break;
    }

    private IEnumerator Coroutine_ButtonFadeOut()
    {
        _buttonWorld.alpha = 0;
        yield break;
    }
}
  
