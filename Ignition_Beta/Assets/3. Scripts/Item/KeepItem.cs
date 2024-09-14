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

    void HandHoverUpdate(Hand hand)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            if (leftHand.currentAttachedObject == null && rightHand.currentAttachedObject == null) return; // 양손 다 들고 있지 않을 때
            if (leftHand.currentAttachedObject != null && rightHand.currentAttachedObject != null) return; // 양손 다 들고 있을 때

            if (leftHand.currentAttachedObject != null) // 왼손에 들고 있는 오브젝트가 있을 때
                currentObject = leftHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가

            else if (rightHand.currentAttachedObject != null) // 오른손에 들고 있는 오브젝트가 있을 때
                currentObject = rightHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가

            switch (currentObject.tag) // 들고 있는 오브젝트의 태그를 switch 문으로 풀기
            {
                case "Pistol": // 권총일 때
                    
                    break;
                case "Rifle": // 소총일 때
                    break;
                case "Shotgun": // 샷건일 때
                    break;
                default:
                    break;
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            if (leftHand.currentAttachedObject != null || rightHand.currentAttachedObject != null) // 아무 손이나 들고 있을 때
            {
                if (leftHand.currentAttachedObject != null) // 왼손에 들고 있는 오브젝트가 있을 때
                    currentObject = leftHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가

                else if (rightHand.currentAttachedObject != null) // 오른손에 들고 있는 오브젝트가 있을 때
                    currentObject = rightHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가

                switch (currentObject.tag) // 들고 있는 오브젝트의 태그를 switch 문으로 풀기
                {
                    case "Pistol": // 권총일 때
                        currentObject.GetComponent<Rigidbody>().isKinematic = false; // isKenematic 끄기
                        break;
                    case "Rifle": // 소총일 때
                        break;
                    case "Shotgun": // 샷건일 때
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
