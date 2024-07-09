using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EW
{

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

}