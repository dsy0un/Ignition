using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LookOut : MonoBehaviour
{
    [SerializeField]
    private Cooldown cooldown;
    [SerializeField]
    private TMP_Text text;

    public void CoolUp()
    {
        float time = 0;
        while (time < cooldown.Cooltime)
        {
            time += Time.deltaTime;
            text.text = $"{time / 3600 % 60:D2}:{time / 60 % 60:D2}:{time % 60:D2}";
        }
    }
}
