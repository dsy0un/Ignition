using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cooldown : MonoBehaviour
{
    [SerializeField]
    Image img;
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    float cooltime;

    float currentTime;

    private void Awake()
    {
        currentTime = cooltime;
    }

    private void Start()
    {
        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            int sec = (int)(currentTime % 60);
            int min = (int)(currentTime / 60);
            int hour = (int)(currentTime / 3600);

            img.fillAmount = currentTime / cooltime;

            if (min >= 60) text.text = $"{hour}:{min}:{sec}";
            else if (sec >= 60) text.text = $"{min}:{sec}";
            else text.text = $"{min:00}:{currentTime:00.00}";
            

            yield return new WaitForFixedUpdate();
        }
    }
}
