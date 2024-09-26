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
                        leftHand.currentAttachedObject == containObject) // 왼손에 들고 있는 오브젝트가 있을 때
                {
                    // containObject = leftHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가
                    containObject.transform.SetParent(null);
                    containObject.GetComponent<Rigidbody>().isKinematic = false;
                }
                if (rightHand.currentAttachedObject != null
                        && rightHand.currentAttachedObject == containObject) // 오른손에 들고 있는 오브젝트가 있을 때
                {
                    // containObject = rightHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가
                    containObject.transform.SetParent(null);
                    containObject.GetComponent<Rigidbody>().isKinematic = false;
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
        if(isStay && other.transform.root.gameObject == containObject) //if (isStay && other.transform.root.CompareTag("Pistol"))
        {
            Debug.Log(containObject);
            Debug.Log(other.transform.root.gameObject);

            if (leftHand.currentAttachedObject != null) // 왼손에 들고 있는 오브젝트가 있을 때
            {
                // containObject = leftHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가
                containObject.GetComponent<Interactable>().attachedToHand.DetachObject(containObject);
                containObject.transform.SetParent(transform);
                containObject.GetComponent<Rigidbody>().isKinematic = true;
                containObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }
            if (rightHand.currentAttachedObject != null) // 오른손에 들고 있는 오브젝트가 있을 때
            {
                // containObject = rightHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가
                containObject.GetComponent<Interactable>().attachedToHand.DetachObject(containObject);
                containObject.transform.SetParent(transform);
                containObject.GetComponent<Rigidbody>().isKinematic = true;
                containObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
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
        if (containObject.GetComponent<Interactable>().attachedToHand)
            containObject.GetComponent<Interactable>().attachedToHand.DetachObject(containObject);
        containObject.transform.SetParent(null);
        containObject.transform.SetParent(transform);
        containObject.GetComponent<Rigidbody>().isKinematic = true;
        containObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        StopCoroutine(Stay());
        isStay = false;
        isExit = false;
        time = 0f;
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

