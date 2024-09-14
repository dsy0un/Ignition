//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Collider dangling from the player's head
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	// [RequireComponent( typeof( CapsuleCollider ) )]
	public class BodyCollider : MonoBehaviour
	{
		public Transform head;

        // private CapsuleCollider cc;
        // public CharacterController cc;

        //-------------------------------------------------
        void Awake()
		{
            // cc = GetComponent<CapsuleCollider>();
            // cc = GetComponent<CharacterController>();
        }


        //-------------------------------------------------
        void FixedUpdate()
		{
			float distanceFromFloor = Vector3.Dot(head.localPosition, Vector3.up);
            //cc.height = Mathf.Max(cc.radius, distanceFromFloor);
            //cc.height = Mathf.Clamp(cc.height, 0, cc.height);
            transform.localPosition = head.localPosition - distanceFromFloor * Vector3.up;
            // transform.localPosition = new Vector3(transform.position.x, 1.5f, transform.position.z);

            Vector3 vector = head.position - transform.position;

            Quaternion currentRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(vector);
            Quaternion bodyY = Quaternion.Euler(currentRotation.eulerAngles.x, targetRotation.eulerAngles.y, currentRotation.eulerAngles.z);
            // Quaternion bodyY = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, bodyY, 3 * Time.deltaTime);
        }
	}
}
