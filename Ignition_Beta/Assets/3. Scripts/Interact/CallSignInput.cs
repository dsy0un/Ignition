using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallSignInput : MonoBehaviour
{
    public Open op;

    private bool inPen;
    private void Start()
    {
        inPen = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "interact" && !inPen)
        {
            op.OpenUIa();
            inPen = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "interact" && inPen)
        {
            inPen = false;
        }
    }
}
