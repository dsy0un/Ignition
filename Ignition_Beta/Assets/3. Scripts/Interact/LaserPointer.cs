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

    static SteamVR_LaserPointer laserPointer = null;
    static Animator anim;
    static ModalWindowManager window;
    public SteamVR_Action_Boolean menuBtn;

    bool isMenu;
    float currentTime;
    float openTime = 3f;

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

        laserPointer = GameObject.Find("RightHand").GetComponent<SteamVR_LaserPointer>();
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    private void Update()
    {
        if (menuBtn.GetState(SteamVR_Input_Sources.LeftHand))
        {
            if (!isMenu)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= openTime)
                {
                    window.ModalWindowIn();
                    Time.timeScale = 0f;
                    isMenu = true;
                    currentTime = 0;
                }
            }
            else
            {
                window.ModalWindowOut();
                Time.timeScale = 1f;
                isMenu = false;
            }
        }
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