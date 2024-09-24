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
    bool isRespawn;

    GameObject currentObject;
    GameObject containObject;

    float time;

    bool isExit, isStay;
    private void Start()
    {
        if (transform.childCount == 0) return; // 오브젝트를 자식으로 추가 안하면 사용 불가
        containObject = transform.GetChild(0).gameObject;
        StartCoroutine(CoroutineUpdate());
    }
    IEnumerator CoroutineUpdate()
    {
        while (true)
        {
            yield return null;
            if (isRespawn) ReSpawnItem();
            if (containObject.GetComponent<Interactable>().attachedToHand != null)
            {
                if (leftHand.currentAttachedObject != null &&
                        leftHand.currentAttachedObject.CompareTag(containObject.tag)) // 왼손에 들고 있는 오브젝트가 있을 때
                {
                    currentObject = leftHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가
                    currentObject.transform.SetParent(null);
                    currentObject.GetComponent<Rigidbody>().isKinematic = false;
                }
                else if (rightHand.currentAttachedObject != null
                        && rightHand.currentAttachedObject.CompareTag(containObject.tag)) // 오른손에 들고 있는 오브젝트가 있을 때
                {
                    currentObject = rightHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가
                    currentObject.transform.SetParent(null);
                    currentObject.GetComponent<Rigidbody>().isKinematic = false;
                }
                StartCoroutine(Stay());
            }
            else
            {
                StopCoroutine(Stay());
                isStay = false;
                isExit = false;
                time = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other) // keepitem
    {
        if(isStay && other.transform.root.CompareTag(containObject.tag)) //if (isStay && other.transform.root.CompareTag("Pistol"))
        {
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
            isStay = false; 
            time = 0f;
            StopCoroutine(Stay());
        }
    }
    IEnumerator Stay()
    {
        if (isExit)
        {

        }
        else
        {
            isExit = true;
            while (true)
            {
                yield return null;
                time += Time.deltaTime;
                if (time > 1)
                {
                    isStay = true;
                    StopCoroutine(Stay());
                }
            }
        }
    }
    public void ReSpawnItem()
    {
        isRespawn = false;
        containObject.GetComponent<Interactable>().attachedToHand.DetachObject(currentObject);
        containObject.transform.SetParent(transform);
        containObject.GetComponent<Rigidbody>().isKinematic = true;
        containObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (isExit && other.transform.root.CompareTag("Pistol"))
    //    {
    //        time += Time.deltaTime;
    //        Debug.Log(time);
    //        if (time > 1f)
    //        {
    //            isStay = true;
    //        }
    //    }
    //}


    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.transform.root.CompareTag("Pistol"))
    //    {
    //        Debug.Log(other.transform.root.name);  
    //        isExit = true;
    //        time = 0f;
    //        StartCoroutine(Stay());
    //    }
    //    if (other.CompareTag("Hand"))
    //    {
    //        if (leftHand.currentAttachedObject != null) // 왼손에 들고 있는 오브젝트가 있을 때
    //        {
    //            currentObject = leftHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가
    //            if (!currentObject.CompareTag("Pistol")) return;
    //            currentObject.transform.SetParent(null);
    //            currentObject.GetComponent<Rigidbody>().isKinematic = false;
    //        }
    //        else if (rightHand.currentAttachedObject != null) // 오른손에 들고 있는 오브젝트가 있을 때
    //        {
    //            currentObject = rightHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가
    //            if (!currentObject.CompareTag("Pistol")) return;
    //            currentObject.transform.SetParent(null);
    //            currentObject.GetComponent<Rigidbody>().isKinematic = false;
    //        }
    //    }
    //}

}

