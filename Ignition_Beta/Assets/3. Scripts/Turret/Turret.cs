using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private float radius = 0f;
    [SerializeField]
    private LayerMask layer;
    [SerializeField]
    private Transform turretHead;
    [SerializeField]
    private Transform gunPointR;
    [SerializeField]
    private Transform gunPointL;

    public Collider[] colliders; //나중에 private으로 변경하기
    public Collider look_enemy; // ''

    [SerializeField]


    void Update()
    {
        LookEnemy();
        GunFire();

        colliders = Physics.OverlapSphere(transform.position, radius, layer);

        if (colliders.Length == 1)
            look_enemy = colliders[0];

        else if (colliders.Length > 0)
        {
            foreach (Collider col in colliders)
            {
                if (Vector3.Distance(transform.position, colliders[0].transform.position) > Vector3.Distance(transform.position, col.transform.position))
                {
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        if (colliders[i] == col)
                        {
                            colliders[i] = colliders[0];
                            colliders[0] = col;
                        }
                    }
                }
            }
        }
    }

    void LookEnemy()
    {
        if (look_enemy != null && colliders.Length > 0)
        {
            look_enemy = colliders[0];

            Vector3 enemyPosition = new Vector3(look_enemy.transform.position.x, look_enemy.transform.position.y - .75f, look_enemy.transform.position.z);

            Vector3 vector = enemyPosition - transform.position;

            Quaternion turret_Y = transform.rotation;
            Quaternion turret_X = transform.rotation;
            turret_Y.y = Quaternion.LookRotation(vector).normalized.y;
            turret_X.x = Quaternion.LookRotation(vector).normalized.x;
            transform.rotation = turret_Y;
            turretHead.transform.rotation = turret_X;
        }
    }

    void GunFire()
    {
        if (colliders.Length > 0)
        {
            RaycastHit hit;
            float maxDistance = 10f;
            Vector3 dir = new Vector3(colliders[0].transform.position.x, colliders[0].transform.position.y - .75f, colliders[0].transform.position.z);

            Debug.DrawRay(gunPointR.position, dir * maxDistance, Color.red);
            if (Physics.Raycast(gunPointR.position, dir, out hit, maxDistance, layer))
            {
                Debug.Log("충돌");
            }

            Debug.DrawRay(gunPointL.position, dir * maxDistance, Color.red);
            if (Physics.Raycast(gunPointL.position, dir, out hit, maxDistance, layer))
            {
                Debug.Log("충돌");
            }
        }
    }

    private void OnDrawGizmos() // 테스트 후 지워도 됨
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
