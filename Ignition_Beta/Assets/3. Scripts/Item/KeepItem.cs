using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class KeepItem : MonoBehaviour
{
    [SerializeField]
    Hand leftHand, rightHand; // 플레이어 왼손, 오른손

    GameObject currentObject; // 현재 들고 있는 오브젝트

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pistol"))
        {
            if (leftHand.currentAttachedObject != null) // 왼손에 들고 있는 오브젝트가 있을 때
                currentObject = leftHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가
            else if (rightHand.currentAttachedObject != null) // 오른손에 들고 있는 오브젝트가 있을 때
                currentObject = rightHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가

            currentObject.transform.SetParent(transform.parent);
            currentObject.transform.localPosition = Vector3.zero;
            currentObject.GetComponent<Rigidbody>().isKinematic = true;
            currentObject.GetComponent<Interactable>().attachedToHand.DetachObject(currentObject);
            currentObject = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pistol"))
        {
            if (leftHand.currentAttachedObject == null && rightHand.currentAttachedObject == null) return; // 양손 다 들고 있지 않을 때

            if (leftHand.currentAttachedObject != null) // 왼손에 들고 있는 오브젝트가 있을 때
                currentObject = leftHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가
            else if (rightHand.currentAttachedObject != null) // 오른손에 들고 있는 오브젝트가 있을 때
                currentObject = rightHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가
            
            currentObject.transform.SetParent(null);
            currentObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Pistol"))
    //    {
    //        if (leftHand.currentAttachedObject == null && rightHand.currentAttachedObject == null) return; // 양손 다 들고 있지 않을 때

    //        if (leftHand.currentAttachedObject != null) // 왼손에 들고 있는 오브젝트가 있을 때
    //            currentObject = leftHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가
    //        else if (rightHand.currentAttachedObject != null) // 오른손에 들고 있는 오브젝트가 있을 때
    //            currentObject = rightHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가

    //        currentObject.transform.SetParent(null);
    //        currentObject.GetComponent<Rigidbody>().isKinematic = false;
    //    }
    //}
}
