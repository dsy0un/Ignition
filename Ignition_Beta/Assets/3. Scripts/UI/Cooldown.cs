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
    [SerializeField]
    GameObject playerHead;

    float currentTime;

    private void Awake()
    {
        currentTime = cooltime;
    }

    private void Start()
    {
        StartCoroutine(CoolDown());
    }

    private void Update()
    {
        transform.LookAt(-playerHead.transform.position);
    }

    IEnumerator CoolDown()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            int sec = (int)currentTime % 60;
            int min = (int)currentTime / 60 % 60;
            int hour = (int)currentTime / 3600 % 60;

            img.fillAmount = 1 - (currentTime / cooltime);

            if (currentTime >= 3600) text.text = $"{hour:D2}:{min:D2}:{sec:D2}";
            else if (currentTime >= 60) text.text = $"{hour:D2}:{min:D2}:{sec:D2}";
            else text.text = $"{hour:D2}:{min:D2}:{currentTime:00.00}";
            

            yield return new WaitForFixedUpdate();
        }
    }
}
