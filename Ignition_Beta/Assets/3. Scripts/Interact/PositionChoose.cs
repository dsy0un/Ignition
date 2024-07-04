using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionChoose : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "interact")
        {
            Toggle toggle;
            toggle = transform.parent.GetComponent<Toggle>();
            toggle.isOn = true;
        }
    }
}
