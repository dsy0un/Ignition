using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [HideInInspector]
    public bool[] isReady = new bool[2];
    private AudioSource sound;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine("Explosion");
    }

    IEnumerator Explosion()
    {
        while (true)
        {
            if (isReady[0] && isReady[1])
            {
                sound.Play();
                yield break;
            }
            yield return null;
        }
    }
}
