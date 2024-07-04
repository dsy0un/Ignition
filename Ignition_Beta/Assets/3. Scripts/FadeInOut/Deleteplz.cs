using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deleteplz : MonoBehaviour
{
    public Image _image;
    public float _fadeTime = 1.5f;
    public AnimationCurve _fadeCurve;
    private void Start()
    {
        StartFadeOut();
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
