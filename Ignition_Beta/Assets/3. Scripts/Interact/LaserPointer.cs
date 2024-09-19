using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.Extras;

public class LaserPointer : MonoBehaviour
{
    static SteamVR_LaserPointer laserPointer = null;
    static Animator anim;

    private void Awake()
    {
        foreach (SteamVR_LaserPointer laser in FindObjectsOfType<SteamVR_LaserPointer>())
        {
            laserPointer = laser;
        }
        laserPointer.PointerClick += PointerClick;

        anim = GetComponent<Animator>();
    }

    public static void PointerClick(object sender, PointerEventArgs e)
    {
        Debug.Log(e.target.name);
        switch (e.target.name)
        {
            default:
                anim.Play("SwapStart");
                break;
        }
    }
}
