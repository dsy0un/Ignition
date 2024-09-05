using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField]
    Transform[] rotors;

    float speed = 1000f;

    private void Start()
    {
        StartCoroutine(RotateRotors());
    }

    IEnumerator RotateRotors()
    {
        while (true)
        {
            foreach (Transform t in rotors) 
            {
                t.Rotate(0, 0, speed * Time.deltaTime);
            }
            yield return null;
        }
    }
}
