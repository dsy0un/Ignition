using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDust : MonoBehaviour
{
    float destroyTime = 0;

    void Update()
    {
        destroyTime += Time.deltaTime;
        if (destroyTime >= 5) Destroy(gameObject);
    }
}
