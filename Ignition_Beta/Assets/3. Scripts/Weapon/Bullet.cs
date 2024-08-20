using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float destructionDelay = 30f;
    public static float Damage { get; set; }

    private void Start()
    {
        Destroy(this.gameObject, destructionDelay);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Gun") || collision.gameObject.layer == LayerMask.NameToLayer("Player")) return;
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            string coliName = collision.gameObject.name;
            if (collision.collider.TryGetComponent<IHitAble>(out var h))
            {
                h.Hit(Damage, coliName);
                Destroy(this.gameObject);
            }
        }
        else Destroy(this.gameObject);
    }
}
