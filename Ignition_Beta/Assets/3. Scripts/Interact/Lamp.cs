using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using Valve.VR;

public class Lamp : MonoBehaviour
{
    public GameObject pointLight;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "interact" && collision.collider.name != "Pen")
        {
            if (pointLight.activeSelf == false)
            {
                pointLight.SetActive(true);
            }
            else
            {
                pointLight.SetActive(false);
            }
        }
    }
}
