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
            if (leftHand.currentAttachedObject != null) // 왼손에 들고 있는 오브젝트가 있을 때
            {
                currentObject = leftHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가
                currentObject.transform.SetParent(transform);
                currentObject.GetComponent<Rigidbody>().isKinematic = true;
                currentObject.transform.SetLocalPositionAndRotation(transform.position, Quaternion.Euler(new Vector3(30, 0, 0)));
            }
            else if (rightHand.currentAttachedObject != null) // 오른손에 들고 있는 오브젝트가 있을 때
            {
                currentObject = rightHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가
                currentObject.transform.SetParent(currentObject.transform);
                currentObject.GetComponent<Rigidbody>().isKinematic = true;
                currentObject.transform.SetLocalPositionAndRotation(transform.position, Quaternion.Euler(new Vector3(30, 0, 0)));
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pistol") && transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);
            child.SetParent(transform);
            child.GetComponent<Rigidbody>().isKinematic = true;
            child.SetLocalPositionAndRotation(transform.position, Quaternion.Euler(new Vector3(30, 0, 0)));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand") && transform.childCount > 0)
        {
            if (leftHand.currentAttachedObject != null) // 왼손에 들고 있는 오브젝트가 있을 때
            {
                currentObject = leftHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가
                currentObject.transform.SetParent(null);
                currentObject.GetComponent<Rigidbody>().isKinematic = false;
            }
            else if (rightHand.currentAttachedObject != null) // 오른손에 들고 있는 오브젝트가 있을 때
            {
                currentObject = rightHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가
                currentObject.transform.SetParent(null);
                currentObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}

