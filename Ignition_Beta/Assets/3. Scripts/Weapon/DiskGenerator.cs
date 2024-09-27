using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiskGenerator : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Transform[] spawnPos;
    public GameObject disk;
    public float throwPower;
    public int score;

    private void Start()
    {
        score = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine("DiskSpawn");
        text.text = $"Score : {score}";
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
