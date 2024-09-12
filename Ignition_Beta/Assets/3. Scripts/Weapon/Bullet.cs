using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float destructionDelay = 30f;
    [SerializeField] private float damage;
    private Gun gun;

    private void Awake()
    {
        gun = GetComponentInParent<Gun>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(gun.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Gun") || 
            collision.gameObject.layer == LayerMask.NameToLayer("Player")) return;
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            string coliName = collision.gameObject.name;
            if (collision.transform.root.TryGetComponent<IHitAble>(out var h))
            {
                h.Hit(damage, coliName);
                Destroy(this.gameObject);
            }
        }
        else gun.ReturnObject(gameObject);
    }
}
