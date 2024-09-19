using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

public class LaserPointer : MonoBehaviour
{
    static SteamVR_LaserPointer laserPointer = null;
    static Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        //foreach (SteamVR_LaserPointer laser in FindObjectsOfType<SteamVR_LaserPointer>())
        //{
        //    laserPointer = laser;
        //}

        laserPointer = GameObject.Find("RightHand").GetComponent<SteamVR_LaserPointer>();
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    public static void PointerInside(object sender, PointerEventArgs e)
    {
        laserPointer.color = Color.yellow;
    }

    public static void PointerOutside(object sender, PointerEventArgs e)
    {
        laserPointer.color = Color.black;
    }

    public static void PointerClick(object sender, PointerEventArgs e)
    {
        switch (e.target.name)
        {
            case "SetGeneral":
                anim.Play("SwapStart");
                break;
            default:
                break;
        }
    }
}
