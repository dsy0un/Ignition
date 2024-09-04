using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ClickCube : MonoBehaviour
{
    int count = 0;

    private void Start()
    {
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        while (count < 11)
        {
            Toast.Instance.Show($"click + {++count}", 1.0f);
            yield return new WaitForSeconds(3f);
        }
    }
}
