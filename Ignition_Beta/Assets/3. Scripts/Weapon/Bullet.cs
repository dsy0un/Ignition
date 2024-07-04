using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float destructionDelay = 30f;

    private void Start()
    {
        Destroy(this.gameObject, destructionDelay);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Gun") || collision.gameObject.layer == LayerMask.NameToLayer("Player")) return;
        else Destroy(this.gameObject);
    }
}
