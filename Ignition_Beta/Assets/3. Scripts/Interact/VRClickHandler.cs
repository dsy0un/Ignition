using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace EW
{
    public class VRClickHandler : MonoBehaviour
    {
        public SteamVR_Action_Boolean triggerClickAction;
        public SteamVR_Input_Sources handType;

        private void Update()
        {
            //    if (triggerClickAction.GetStateDown(handType))
            //    {
            //        Ray ray = new Ray(transform.position, transform.forward);
            //        RaycastHit hit;
            //        Debug.DrawRay(transform.position, transform.forward, Color.red);
            //        Debug.Log("´­¸²");


            //        if (Physics.Raycast(ray, out hit))
            //        {
            //            Document clickable = hit.transform.GetComponent<Document>();
            //            if (clickable != null)
            //            {
            //                clickable.Complete();
            //            }
            //        }
            //    }
        }
    }

}