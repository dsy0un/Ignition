using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x * -1.5f, transform.rotation.y, transform.rotation.z));
            rb.AddForce(Vector3.up * 1.5f, ForceMode.Impulse);
        }
    }
}
