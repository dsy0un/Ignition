using System.Collections;
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
    private Transform turretHeadPivot;
    [SerializeField]
    private Transform[] points;
    [SerializeField]
    private GameObject turretBullet;
    [SerializeField]
    private GameObject groundEffect;

    private Collider[] colliders;
    private Collider look_enemy;

    readonly float lookDelay = 3f;
    public static float shotSpeed = 100f;
    public static float hitTime;

    private void Start()
    {
        StartCoroutine(GunFire());
    }

    void Update()
    {
        LookEnemy();
        StartCoroutine(DetectEnemy());
    }

    IEnumerator DetectEnemy()
    {
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
                            look_enemy = colliders[0];
                        }
                    }
                }
            }
        }

        yield return null;
    }

    void LookEnemy()
    {
        if (look_enemy != null && colliders.Length > 0)
        {
            look_enemy = colliders[0];

            Vector3 enemyPosition = look_enemy.transform.position;

            Vector3 vector = enemyPosition - turretHeadPivot.position;

            Quaternion currentRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(vector);
            Quaternion turret_Y = Quaternion.Euler(currentRotation.eulerAngles.x, targetRotation.eulerAngles.y, currentRotation.eulerAngles.z);
            Quaternion turret_X = Quaternion.Euler(targetRotation.eulerAngles.x, currentRotation.eulerAngles.y, currentRotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, turret_Y, lookDelay * Time.deltaTime);
            turretHead.rotation = Quaternion.Lerp(turretHead.rotation, turret_X, lookDelay * Time.deltaTime);

            //float turret_Y = transform.rotation.y;
            //Quaternion rot_Y = transform.rotation;
            //turret_Y = Quaternion.LookRotation(vector).normalized.y;
            //rot_Y.eulerAngles = new Vector3(0,Mathf.Lerp(transform.rotation.y, turret_Y, lookDelay * Time.deltaTime),0);


            //Quaternion turret_Y = transform.rotation;
            //Quaternion turret_X = transform.rotation;
            //turret_Y.y = Quaternion.LookRotation(vector).normalized.y;
            //turret_X.x = Quaternion.LookRotation(vector).normalized.x;
            //transform.rotation = Quaternion.Lerp(transform.rotation, turret_Y, Time.deltaTime * lookDelay);
            //turretHead.rotation = Quaternion.Lerp(turretHead.rotation, turret_X, Time.deltaTime * lookDelay);
            //Quaternion headRotation = turretHead.rotation;
            //headRotation.x = Mathf.Clamp(turretHead.rotation.x, -45f, 45f);
        }
    }

    IEnumerator GunFire()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            if (look_enemy != null && colliders.Length > 0)
            {
                look_enemy = colliders[0];

                RaycastHit[] hits;
                float maxDistance = radius;
                int layers = (1 << LayerMask.NameToLayer("EnemyCollision")) + (1 << LayerMask.NameToLayer("Ground"));

                for (int i = 0; i < points.Length; i++) 
                {
                    Vector3 forward = turretHeadPivot.forward;

                    Debug.DrawRay(points[i].position, forward * maxDistance, Color.black, 1f);
                    hits = Physics.RaycastAll(points[i].position, forward, maxDistance, layers);

                    Instantiate(turretBullet, points[i].position, points[i].rotation);

                    foreach (RaycastHit hit in hits)
                    {
                        hitTime = (Vector3.Distance(hit.point, points[i].position) / shotSpeed);
                        StartCoroutine(GiveDamage(hit, hitTime));
                    }

                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield return null;
        }
    }

    IEnumerator GiveDamage(RaycastHit hit, float hitTime)
    {
        yield return new WaitForSeconds(hitTime);
        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy") && hit.transform.TryGetComponent<IHitAble>(out var h))
        {
            //Debug.Log(hitTime + "아 적이요");
            h.Die();
        }
        else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            //Debug.Log(hitTime + "아 땅이요");
            Instantiate(groundEffect, hit.point, Quaternion.identity); 
        }

    }

    //IEnumerator GunFire()
    //{
    //    while (true)
    //    {
    //        if (look_enemy != null && colliders.Length > 0)
    //        {
    //            look_enemy = colliders[0];

    //            RaycastHit hit;
    //            float maxDistance = 10f;
    //            Vector3 forward = turretHead.forward;
    //            Vector3 dir = new Vector3(look_enemy.transform.position.x, look_enemy.transform.position.y - .75f, look_enemy.transform.position.z);

    //            for (int i = 0; i < points.Count; i++)
    //            {
    //                if (points[i].name == "gunpoint_L" || points[i].name == "Trail") continue;
    //                Debug.DrawRay(points[i].position, forward * maxDistance, Color.red);
    //                if (Physics.Raycast(points[i].position, forward, out hit, maxDistance, layer))
    //                {
    //                    Debug.Log("충돌");
    //                    points[i].GetChild(0).GetComponent<Rigidbody>().AddForce(points[i].forward * shotSpeed * Time.deltaTime);
    //                    print(hit.transform.gameObject.layer + " " + LayerMask.NameToLayer("EnemyCollision"));
    //                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("EnemyCollision"))
    //                    {
    //                        if (hit.transform.root.TryGetComponent<IHitAble>(out var h))
    //                        {
    //                            float disSec = Vector3.Distance(hit.transform.position, points[i].position) / shotSpeed * Time.deltaTime;
    //                            print(disSec);

    //                        }
    //                    }
    //                    // yield return new WaitForSeconds(1f);
    //                }
    //            }

    //            for (int i = 0; i < points.Count; i++)
    //            {
    //                if (points[i].name == "gunpoint_R" || points[i].name == "Trail") continue;
    //                Debug.DrawRay(points[i].position, forward * maxDistance, Color.red);
    //                if (Physics.Raycast(points[i].position, forward, out hit, maxDistance, layer))
    //                {
    //                    Debug.Log("충돌");
    //                    points[i].GetChild(0).GetComponent<Rigidbody>().AddForce(points[i].forward * shotSpeed * Time.deltaTime);
    //                    // yield return new WaitForSeconds(5f);
    //                }
    //            }
    //        }
    //        yield return null;
    //    }
    //}

    private void OnDrawGizmos() // 테스트 후 지워도 됨
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
