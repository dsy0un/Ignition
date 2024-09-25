using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float destructionDelay = 30f;
    [SerializeField] private float damage;
    public GameObject hit; //삭제 필
    private Gun gun;
    private float time;
    private Rigidbody rb; // 삭제 필요
    public int shotSpeed = 100; // 삭제 필요

    bool isUpgrade = false;

    private void Awake()
    {
        gun = GetComponentInParent<Gun>();
        rb = GetComponentInParent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            rb.AddRelativeForce(Vector3.fwd * shotSpeed, ForceMode.Impulse);
        }
        //time += Time.deltaTime;
        //if (time >= destructionDelay)
        //{
        //    gun.ReturnObject(gameObject);
        //    time = 0;
        //}
        //if (!isUpgrade && GameManager.Instance.isBulletUpgrade)
        //{
        //    isUpgrade = true;
        //    damage *= 1.5f;
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            string coliName = collision.gameObject.name;
            if (collision.transform.root.TryGetComponent<IHitAble>(out var h))
            {
                h.Hit(damage, coliName);
                Instantiate(hit, transform.position , Quaternion.Euler(Vector3.back));
                Destroy(this.gameObject);
            }
        }
        //else gun.ReturnObject(gameObject);
    }
}
