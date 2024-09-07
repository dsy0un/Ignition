using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentManager : MonoBehaviour
{
    void Awake()
    {
        GameManager.Instance.ReGetCom();
    }
}
