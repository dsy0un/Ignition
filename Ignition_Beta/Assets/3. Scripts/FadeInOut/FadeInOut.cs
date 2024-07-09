using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class FadeInOut : MonoBehaviour
{
    public Image _image;
    public float _fadeTime = 1.5f;
    public AnimationCurve _fadeCurve;

    [Tooltip ("스크립트 들어있는 오브젝트 추가후 StartFadeIn || StartFadeOut 선택")]
    [SerializeField]
    private UnityEvent onStartEvent;

    private void Start()
    {
        if (onStartEvent != null)
        {
            onStartEvent.Invoke();
        }
    }
    public void StartFadeIn()
    {
        StartCoroutine(Fade(1, 0));
    }
        
    public void StartFadeOut()
    {
        StartCoroutine(Fade(0, 1));
    }

    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / _fadeTime;
            Color color = _image.color;
            color.a = Mathf.Lerp(start, end, _fadeCurve.Evaluate(percent));
            _image.color = color;
            yield return null;
        }
    }
}
