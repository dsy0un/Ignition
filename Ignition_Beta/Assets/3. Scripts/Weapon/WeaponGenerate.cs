using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class WeaponGenerate : MonoBehaviour
{
    [SerializeField]
    GameObject[] weapons;
    [SerializeField]
    Vector3[] weaponsInitPos;
    [SerializeField]
    Quaternion[] weaponsInitRot;

    int level;

    private void Start()
    {
        level = GameManager.Instance.Level;
        Debug.Log(level);
        if (level > 0)
        {
            for (int i = 0; i < level; i++)
            {
                weapons[i].SetActive(true);
                weaponsInitPos[i] = weapons[i].transform.position;
                weaponsInitRot[i] = weapons[i].transform.rotation;
            }
        }
    }
}
