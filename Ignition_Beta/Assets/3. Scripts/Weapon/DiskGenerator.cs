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
    [SerializeField]
    GameObject playerHead;

    private int bScore = 0;

    private void Awake()
    {
        playerHead = GameManager.Instance.playerHead.gameObject;
    }
    private void Start()
    {
        score = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine("DiskSpawn");
        if (Input.GetKeyDown(KeyCode.R))
        {
            
        }
        if (bScore < score)
        {
            best.text = $"Best Score\n{bScore}";
        }
        text.text = $"Score : {score}";
        Vector3 l_vector = playerHead.transform.position - bestScore.transform.position;
        bestScore.transform.rotation = Quaternion.Lerp(bestScore.transform.rotation, Quaternion.LookRotation(-l_vector).normalized, 3 * Time.deltaTime);
    }

    IEnumerator DiskSpawn()
    {
        foreach (Transform i in spawnPos)
        {
            yield return new WaitForSeconds(Random.Range(2f, 4f));
            Rigidbody diskRb = Instantiate(disk, i.position, i.rotation, gameObject.transform).GetComponent<Rigidbody>();
            diskRb.AddRelativeForce(Vector3.forward * throwPower, ForceMode.VelocityChange);
        }
    }
}
