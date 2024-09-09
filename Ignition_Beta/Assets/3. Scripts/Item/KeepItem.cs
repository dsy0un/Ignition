using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepItem : MonoBehaviour
{
    Vector3 offset;
    Camera mainCamera;

    private void Start()
    {
        mainCamera = GameManager.Instance.mainCamera;
    }

    private void Update()
    {
        FollowCamera();
    }

    void FollowCamera()
    {
        transform.position = mainCamera.transform.position
            + mainCamera.transform.forward * offset.z
            + mainCamera.transform.up * offset.y
            + mainCamera.transform.right * offset.x;

        Vector3 Rvector = mainCamera.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Rvector).normalized;
    }
}
