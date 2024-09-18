using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class KeepItem : MonoBehaviour
{
    [SerializeField]
    Hand leftHand, rightHand; // 플레이어 왼손, 오른손
    [SerializeField]
    GameObject containedObject;

    [SerializeField]
    GameObject currentObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pistol"))
        {

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            if (leftHand.currentAttachedObject)
                currentObject = leftHand.currentAttachedObject;
            else if (rightHand.currentAttachedObject)
                currentObject = rightHand.currentAttachedObject;

            currentObject.GetComponent<Rigidbody>().isKinematic = false;
            currentObject.transform.SetParent(null, false);
        }
    }
}

