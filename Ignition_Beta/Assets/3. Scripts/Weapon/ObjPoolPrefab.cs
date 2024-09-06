using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPoolPrefab : MonoBehaviour
{
    private Bolt bolt;
    public Rigidbody rb;
    public float ejectPower;
    private void Awake()
    {
        bolt = GetComponentInParent<Bolt>();
    }
    void OnEnable()
    {
        StartCoroutine("DestroyObj");
    }
    IEnumerator DestroyObj()
    {
        Destroy(GetComponent<FixedJoint>());
        transform.SetParent(null);
        rb.AddRelativeForce(Vector3.right * Random.Range(ejectPower * 0.5f, ejectPower), ForceMode.Impulse);
        rb.AddForce(Vector3.up * Random.Range(ejectPower * 0.2f, ejectPower * 0.3f), ForceMode.Impulse);
        rb.AddRelativeTorque(Vector3.right * 10, ForceMode.Impulse);
        yield return new WaitForSeconds(4);
        transform.rotation = Quaternion.Euler(Vector3.zero);
        bolt.ReturnObject(this.gameObject);
    }
}
