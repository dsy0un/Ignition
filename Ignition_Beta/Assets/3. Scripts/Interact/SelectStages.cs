using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SelectStages : MonoBehaviour
{
    //[SerializeField]
    //GameObject[] selects;

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes grab = hand.GetGrabStarting();
        bool isgrab = hand.IsGrabEnding(gameObject);
        if (grab == GrabTypes.Grip
    )
        {
            name = transform.name;
            LoadingSceneManager.LoadScene(name);
        }
    }
}
