using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using TMPro;
using UnityEngine;
using Valve.VR;

public class Open : MonoBehaviour
{
    public TMP_InputField input;
    private string keyboardText = string.Empty;
    public bool textInput = false;
    public string callSign;
    

    // Start is called before the first frame update
    void Start()
    {
        SteamVR_Events.System(EVREventType.VREvent_KeyboardCharInput).Listen(OnKeyboardInput);
        SteamVR_Events.System(EVREventType.VREvent_KeyboardDone).Listen(OnKeyboardDone);
        SteamVR_Events.System(EVREventType.VREvent_KeyboardClosed).Listen(OnKeyboardClosed);
        //OpenVR.Overlay.ShowKeyboard(0, 0, 0, "콜 사인을 입력해주세요.", 256, keyboardText, 0);

    }

    void OnDestroy()
    {
        // Unsubscribe from the keyboard input event
        SteamVR_Events.System(EVREventType.VREvent_KeyboardCharInput).Remove(OnKeyboardInput);
        SteamVR_Events.System(EVREventType.VREvent_KeyboardDone).Remove(OnKeyboardDone);
        SteamVR_Events.System(EVREventType.VREvent_KeyboardClosed).Remove(OnKeyboardClosed);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenUIa()
    {
        // TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        // System.Diagnostics.Process.Start("OSK.exe");
        //input.ActivateInputField();
        OpenVR.Overlay.ShowKeyboard(0, 0, 0, "콜 사인을 입력해주세요.", 256, keyboardText, 0);
    }

    private void OnKeyboardInput(VREvent_t vrEvent)
    {
        StringBuilder keyboardInput = new(256);
        SteamVR.instance.overlay.GetKeyboardText(keyboardInput, 256);

        keyboardText = keyboardInput.ToString();

        // Do something with the input text
        Debug.Log("Input text: " + keyboardInput.ToString());
    }
    private void OnKeyboardDone(VREvent_t vrEvent)
    {
        Debug.Log("키보드 입력 완료: " + keyboardText.ToString()); 
        input.text = keyboardText.ToString();
        if(input.text == "")
        {
            Debug.Log("비어있음");
            textInput = false;
        }
        else
        {
            textInput = true;
            Debug.Log("입력 신호 보냄");
            callSign = input.text;
        }
    }

    private void OnKeyboardClosed(VREvent_t vrEvent)
    {
        Debug.Log("Keyboard closed");
        if (input.text == "")
        {
            textInput = false;
        }
    }
}
