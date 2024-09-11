using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour, IHitAble
{
    public SteamVR_Action_Vector2 input;
    public float speed = 1;
    private CharacterController characterController;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        int layer = LayerMask.NameToLayer("Player");

        //ChangeLayerRecursively(gameObject, layer); 안써도 돌아가긴함 ㅇㅇㅇㅇ
    }
    void Update()
    {
        if (input.axis.magnitude > 0.1)
        {
            Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
            characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, 9.81f, 0) * Time.deltaTime);
        }
    }

    private void ChangeLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            ChangeLayerRecursively(child.gameObject, layer);
        }
    }

    public void Hit(float dmg, string coliName)
    {

    }
    public void Die()
    {
        
    }
    public void Respawn()
    {

    }
}
