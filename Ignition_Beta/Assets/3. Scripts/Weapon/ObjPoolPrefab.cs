using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPoolPrefab : MonoBehaviour
{
    public Bolt bolt;
    public Rigidbody rb;
    public float ejectPower;
    void Start()
    {
        StartCoroutine("DestroyObj");
    }
    IEnumerator DestroyObj()
    {
        rb.AddForce(Vector3.right * ejectPower, ForceMode.Impulse);
        yield return new WaitForSeconds(4);
        bolt.ReturnObject(this.gameObject);
    }
}
