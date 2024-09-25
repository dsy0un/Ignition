using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class FadeInOut : MonoBehaviour
{
    public Image _image;
    public float _fadeTime = 1.5f;
    public AnimationCurve _fadeCurve;
    public AnimationCurve _fadeOoutCurve;

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
        StartCoroutine(Fade(1, 0, _fadeTime, _fadeCurve));
    }
    public void StartFadeOut()
    {
        StartCoroutine(Fade(0, 1, _fadeTime, _fadeOoutCurve));
    }
    /// <summary>
    /// 플레이어 페이드 In, Out 기능
    /// </summary>
    /// <param name="start">FadeIn = 1, FadeOut = 0</param>
    /// <param name="end">FadeIn = 0, FadeOut = 1</param>
    /// <param name="fadeTime">지속 시간</param>
    /// <param name="fadeCurve">자연스러운 페이드 조절</param>
    public void StartFade(float start, float end, float fadeTime, AnimationCurve fadeCurve)
    {
        StartCoroutine(Fade(start, end, fadeTime, fadeCurve));
    }

    private IEnumerator Fade(float start, float end, float fadeTime, AnimationCurve fadeCurve)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;
            Color color = _image.color;
            color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));
            _image.color = color;
            yield return null;
        }
    }
}
