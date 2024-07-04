using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class M_Position : MonoBehaviour
{
    public Toggle[] position;
    public bool checkBox = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (position[0].isOn && !checkBox || 
            position[1].isOn && !checkBox)
        {
            Debug.Log(1);
            checkBox = true;
        }
        else if (!position[0].isOn && !position[1].isOn)
        {
            Debug.Log("체크 안되어 있음");
            checkBox = false;
        }
        
    }
    public void ToggleOnlyOne(bool isOn)
    {
        if (position[0].isOn)
        {
            position[1].isOn = false;
        }
        
    }

    public void ToggleOnlyTwe(bool isOn)
    {
        if (position[1].isOn)
        {
            position[0].isOn = false;
        }

    }
    
}
