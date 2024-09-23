using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Port : MonoBehaviour
{
    
    private enum mapState
    {
        None,
        Stage1,
        Stage2,
        Stage3,
        Stage4
    }

    mapState mapName = mapState.None;
    private void Update()
    {
        switch (mapName)
        {
            case mapState.Stage1:
                break;
            case mapState.Stage2:
                break;
            case mapState.Stage3:
                break;
            case mapState.Stage4:
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {

        }
    }
}
