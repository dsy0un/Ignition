using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeGrip : MonoBehaviour
{
    public GameObject fixedPoint;

    private void Update()
    {
        transform.position = fixedPoint.transform.position;
    }
}
