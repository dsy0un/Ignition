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

    private void Start()
    {
        Quaternion r = transform.rotation;
        r.y = Quaternion.Euler(new Vector3(0, 0, 0)).normalized.y;

    }

    private void Update()
    {
        
    }

    IEnumerator CoolDown()
    {
        yield return null;

        if (starTime > 0)
        {
            starTime -= Time.deltaTime;

            if (starTime < 0)
            {
                starTime = 0;
                isUseSkill = false;
                hideImg.SetActive(false);
                starTime = coolTime;

            }
            float time = starTime / coolTime;
            hideImgFill = time;

        }
    }
}
