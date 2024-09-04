using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class LookOut : MonoBehaviour
{
    [SerializeField]
    private Cooldown cooldown;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private EnemyGenerate Egen;

    bool cool = false;

    public void StartGen()
    {
        Egen.StartSpawn();
    }
    IEnumerator CoolUpCO()
    {
        cool = !cool;
        float time = 0;
        float count = 0;
        while (cool)
        {

            count += Time.deltaTime;
            time = Mathf.Lerp(0, cooldown.Cooltime, count / 1.5f);
            int sec = (int)time % 60;
            int min = (int)time / 60 % 60;
            int hour = (int)time / 3600 % 60;
            Debug.Log(time);
            text.text = $"{hour:D2}:{min:D2}:{sec:D2}";
            yield return null;
        }
    }
}
