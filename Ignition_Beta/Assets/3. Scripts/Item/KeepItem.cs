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

    GameObject currentObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pistol"))
        {
            if (leftHand.currentAttachedObject)
                currentObject = leftHand.currentAttachedObject;
            else if (rightHand.currentAttachedObject)
                currentObject = rightHand.currentAttachedObject;

            currentObject.transform.SetParent(transform.parent, false);
            currentObject.GetComponent<Rigidbody>().isKinematic = true;
            currentObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(30, 0, 0));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pistol"))
        {
            if (!leftHand.currentAttachedObject || !rightHand.currentAttachedObject) return;
            if (leftHand.currentAttachedObject)
                currentObject = leftHand.currentAttachedObject;
            else if (rightHand.currentAttachedObject)
                currentObject = rightHand.currentAttachedObject;

            currentObject.transform.SetParent(null);
            currentObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}

