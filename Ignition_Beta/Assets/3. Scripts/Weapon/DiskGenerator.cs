using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskGenerator : MonoBehaviour
{
    public Transform[] spawnPos;
    public GameObject disk;
    public float throwPower;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine("DiskSpawn");
    }

    IEnumerator DiskSpawn()
    {
        foreach (Transform i in spawnPos)
        {
            yield return new WaitForSeconds(Random.Range(2f, 4f));
            Rigidbody diskRb = Instantiate(disk, i.position, i.rotation).GetComponent<Rigidbody>();
            diskRb.AddRelativeForce(Vector3.forward * throwPower * Time.deltaTime, ForceMode.Acceleration);
        }
    }
}
