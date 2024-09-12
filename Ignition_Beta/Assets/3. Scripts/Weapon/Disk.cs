using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
}
