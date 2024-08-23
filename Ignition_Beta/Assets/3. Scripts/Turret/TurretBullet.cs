using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    float destroyTime = 0;

    void Update()
    {
        destroyTime += Time.deltaTime;
        if (destroyTime >= 10) Destroy(gameObject);
        else transform.Translate(transform.forward * Turret.shotSpeed, Space.World);

    }
}
