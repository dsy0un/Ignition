using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    float destroyTime = 0;

    void Update()
    {
        destroyTime += Time.deltaTime;
        if (destroyTime >= 7) Destroy(gameObject);
        else transform.Translate((transform.forward * Turret.shotSpeed) * Time.deltaTime, Space.World);

    }

    private Vector3 m_LastPosition;
    public float m_Speed;

    void FixedUpdate()
    {
        m_Speed = GetSpeed();
        Debug.Log(string.Format("{0:00.00} m/s", m_Speed));
        Debug.Log(string.Format("{0:00.00} km/h", m_Speed * 3.6f));
    }

    float GetSpeed()
    {
        float speed = (((transform.position - m_LastPosition).magnitude) / Time.deltaTime);
        m_LastPosition = transform.position;

        return speed;
    }
}
