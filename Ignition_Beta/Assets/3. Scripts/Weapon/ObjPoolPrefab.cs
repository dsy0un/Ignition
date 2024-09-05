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
        rb.AddForce(Vector3.right * Random.Range(ejectPower * 0.5f, ejectPower), ForceMode.Impulse);
        yield return new WaitForSeconds(4);
        bolt.ReturnObject(this.gameObject);
    }
}
