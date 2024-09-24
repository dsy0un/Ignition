using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class KeepItem : MonoBehaviour
{
    [SerializeField]
    Hand leftHand, rightHand; // 플레이어 왼손, 오른손

    GameObject currentObject;

    bool isExit, isStay;

    private void OnTriggerEnter(Collider other)
    {
        if (isStay && other.transform.root.CompareTag("Pistol"))
        {
            Debug.Log(13123);
            if (leftHand.currentAttachedObject != null) // 왼손에 들고 있는 오브젝트가 있을 때
            {
                currentObject = leftHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가
                currentObject.GetComponent<Interactable>().attachedToHand.DetachObject(currentObject);
                currentObject.transform.SetParent(transform);
                currentObject.GetComponent<Rigidbody>().isKinematic = true;
                currentObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }
            else if (rightHand.currentAttachedObject != null) // 오른손에 들고 있는 오브젝트가 있을 때
            {
                currentObject = rightHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가
                currentObject.GetComponent<Interactable>().attachedToHand.DetachObject(currentObject);
                currentObject.transform.SetParent(transform);
                currentObject.GetComponent<Rigidbody>().isKinematic = true;
                currentObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }
            isExit = false;
            isStay = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isExit && other.transform.root.CompareTag("Pistol"))
        {
            isStay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.CompareTag("Pistol"))
        {
            isExit = true;
        }
        if (other.CompareTag("Hand") && transform.childCount > 0)
        {
            if (leftHand.currentAttachedObject != null) // 왼손에 들고 있는 오브젝트가 있을 때
            {
                currentObject = leftHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가
                if (!currentObject.CompareTag("Pistol")) return;
                currentObject.transform.SetParent(null);
                currentObject.GetComponent<Rigidbody>().isKinematic = false;
            }
            else if (rightHand.currentAttachedObject != null) // 오른손에 들고 있는 오브젝트가 있을 때
            {
                currentObject = rightHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가
                if (!currentObject.CompareTag("Pistol")) return;
                currentObject.transform.SetParent(null);
                currentObject.GetComponent<Rigidbody>().isKinematic = false;
            }
            
        }
    }
}

