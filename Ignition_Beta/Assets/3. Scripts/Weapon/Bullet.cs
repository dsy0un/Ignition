using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float destructionDelay = 30f;
    [SerializeField] private float damage;
    private Gun gun;
    private float time;

    private void Awake()
    {
        gun = GetComponentInParent<Gun>();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= destructionDelay)
        {
            gun.ReturnObject(gameObject);
            time = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Gun") || 
            collision.gameObject.layer == LayerMask.NameToLayer("Player")) return;
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            string coliName = collision.gameObject.name;
            if (collision.transform.root.TryGetComponent<IHitAble>(out var h))
            {
                h.Hit(damage, coliName);
                gun.ReturnObject(this.gameObject);
            }
        }
        else gun.ReturnObject(this.gameObject);
    }
}
