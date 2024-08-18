using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnchor : MonoBehaviour
{
    public Transform playerCollider;  // 플레이어 콜라이더
    public Transform head;

    void Update()
    {
        // HMD의 위치에 플레이어 콜라이더의 위치를 동기화
        Vector3 newPosition = new Vector3(head.position.x, playerCollider.position.y, head.position.z);
        playerCollider.position = newPosition;
    }
}
