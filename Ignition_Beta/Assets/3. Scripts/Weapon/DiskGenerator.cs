using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiskGenerator : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI best;
    public Transform[] spawnPos;
    public GameObject disk;
    public GameObject bestScore;
    public float throwPower;
    public int score;
    public float time;
    public float currentTime;
    private bool isTimer;
    [SerializeField]
    GameObject playerHead;

    private int bScore = 0;

    private void Awake()
    {
        playerHead = GameManager.Instance.playerHead.gameObject;
    }
    private void Start()
    {
        currentTime = time;
        isTimer = false;
        score = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isTimer = true;
            StopCoroutine("DiskSpawn");
            StartCoroutine("DiskSpawn");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            score = 0;
            currentTime = time;
            isTimer = false;
            text2.text = $"남은시간 : 00:00";
            StopCoroutine("DiskSpawn");

        }
        if (isTimer)
        {
            currentTime -= Time.deltaTime;

            int sec = (int)currentTime % 60;
            int min = (int)currentTime / 60 % 60;

            if (currentTime >= 3600) text2.text = $"남은시간 : {min:D2}:{sec:D2}";
            else if (currentTime >= 60) text2.text = $"남은시간 : {min:D2}:{sec:D2}";
            else text2.text = $"남은시간 : {min:D2}:{currentTime:00.00}";
            if (currentTime <= 0)
            {
                StopCoroutine("DiskSpawn");
                isTimer = false;
                text2.text = $"<color=red>종료!</color>";
            }
        }
        if (bScore <= score)
        {
            bScore = score;
            best.text = $"Best Score\n{bScore}";
        }
        text.text = $"Score : {score}";
        Vector3 l_vector = playerHead.transform.position - bestScore.transform.position;
        bestScore.transform.rotation = Quaternion.Lerp(bestScore.transform.rotation, Quaternion.LookRotation(-l_vector).normalized, 3 * Time.deltaTime);
    }

    IEnumerator DiskSpawn()
    {
        while (true)
        {
            foreach (Transform i in spawnPos)
            {
                yield return new WaitForSeconds(Random.Range(2f, 4f));
                Rigidbody diskRb = Instantiate(disk, i.position, i.rotation, gameObject.transform).GetComponent<Rigidbody>();
                diskRb.AddRelativeForce(Vector3.forward * throwPower, ForceMode.VelocityChange);
            }
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }
}
