using Michsky.UI.Shift;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

public class LaserPointer : MonoBehaviour
{

    static SteamVR_LaserPointer[] laserPointers = null;
    static Animator anim;
    static ModalWindowManager window;
    public SteamVR_Action_Boolean menuBtn;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        window = GetComponentInChildren<ModalWindowManager>();
    }

    private void OnEnable()
    {
        //foreach (SteamVR_LaserPointer laser in FindObjectsOfType<SteamVR_LaserPointer>())
        //{
        //    laserPointer = laser;
        //}

        laserPointers = FindObjectsOfType<SteamVR_LaserPointer>();
        foreach (SteamVR_LaserPointer pointer in laserPointers)
        {
            pointer.PointerIn += PointerInside;
            pointer.PointerOut += PointerOutside;
            pointer.PointerClick += PointerClick;
        }
    }

    private void Update()
    {
        if (menuBtn.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            Debug.Log(1);
            window.ModalWindowIn();
            Time.timeScale = 0f;
        }
        //else
        //{
        //    window.ModalWindowOut();
        //    Time.timeScale = 1f;
        //}
    }

    public static void PointerInside(object sender, PointerEventArgs e)
    {
        foreach (SteamVR_LaserPointer pointer in laserPointers)
            pointer.color = Color.yellow;
    }

    public static void PointerOutside(object sender, PointerEventArgs e)
    {
        foreach (SteamVR_LaserPointer pointer in laserPointers)
            pointer.color = Color.black;
    }

    public static void PointerClick(object sender, PointerEventArgs e)
    {
        switch (e.target.name)
        {
            case "Continue":
                window.ModalWindowOut();
                Time.timeScale = 1f;
                break;
            case "Quit":
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
                break;
            default:
                return;
        }
    }
}
