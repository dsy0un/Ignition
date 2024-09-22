using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripPos : MonoBehaviour
{
    public GameObject grip;

    private void Update()
    {
        transform.position = grip.transform.position;
    }
}
