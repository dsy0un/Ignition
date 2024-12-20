using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    [SerializeField]
    Image img;
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    float cooltime;
    public float Cooltime {  get { return cooltime; }}
    [SerializeField]
    GameObject playerHead;

    float currentTime;

    private void Awake()
    {
        playerHead = GameManager.Instance.playerHead.gameObject;
    }

    private void Start()
    {
        currentTime = cooltime;
        StartCoroutine(CoolDown());
    }

    private void Update()
    {
        Vector3 l_vector = playerHead.transform.position - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-l_vector).normalized, 3 * Time.deltaTime);
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


            yield return null;
        }
        GameManager.Instance.DefSuccessEvent();
    }
}
