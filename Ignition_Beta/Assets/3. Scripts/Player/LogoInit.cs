using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoInit : MonoBehaviour
{
    bool logo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Logo" && !logo)
        {
            logo = true;
            Destroy(gameObject);
        }
    }
}
