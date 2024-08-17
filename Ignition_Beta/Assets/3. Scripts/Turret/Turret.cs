using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private float radius = 0f;
    [SerializeField]
    private LayerMask layer;
    [SerializeField]
    private Transform turretHead;

    public Collider[] colliders; //나중에 private으로 변경하기
    public Collider look_enemy; // ''

    void Update()
    {
        colliders = Physics.OverlapSphere(transform.position, radius, layer);

        if (colliders.Length == 1)
        {
            look_enemy = colliders[0];
            Debug.Log(1);
        }
        else if (colliders.Length > 0)
        {
            float enemy_distance = Vector3.Distance(transform.position, colliders[0].transform.position);
            foreach (Collider col in colliders)
            {
                float enemy_distance2 = Vector3.Distance(transform.position, col.transform.position);
                if (enemy_distance > enemy_distance2)
                {
                    enemy_distance = enemy_distance2;
                    look_enemy = col;
                }
            }

        }
        StartCoroutine("LookUp");
    }

    IEnumerator LookUp()
    {
        if (look_enemy != null)
        {
            Vector3 vector = look_enemy.transform.position - transform.position;

            Quaternion turret_Y = transform.rotation;
            Quaternion turret_X = transform.rotation;
            turret_Y.y = Quaternion.LookRotation(vector).normalized.y;
            turret_X.x = Quaternion.LookRotation(vector).normalized.x;
            transform.rotation = turret_Y;
            turretHead.transform.rotation = turret_X;
        }
        yield return null;
    }

    private void OnDrawGizmos() // 테스트 후 지워도 됨
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
