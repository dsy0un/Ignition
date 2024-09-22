using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision pistol");
        if (collision.transform.root.CompareTag("Pistol"))
        {
            Debug.Log("collision root pistol");
        }
    }
}
