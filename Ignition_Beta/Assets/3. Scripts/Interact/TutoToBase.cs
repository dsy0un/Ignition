using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutoToBase : MonoBehaviour
{
    public void MoveToBase()
    {
        SceneManager.LoadScene("Base");
    }
}
