using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllManager : MonoBehaviour
{
    private static AllManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
