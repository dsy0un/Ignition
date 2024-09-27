using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{
    Rigidbody rb;
    DiskGenerator generator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        generator = GetComponentInParent<DiskGenerator>();
        Destroy(gameObject, 6);
    }

    //private void Update()
    //{
    //    Invoke("AddGravity", 0.5f);
    //}

    //private void AddGravity()
    //{
    //    rb.AddForce(Vector3.down * 9.8f, ForceMode.Force);
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            generator.score += 10;
            Destroy(gameObject);
        }
    }
}
