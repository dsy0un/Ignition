using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    private bool isActivate;
    //[HideInInspector]
    public GrenadeSafeDv[] safeDv = new GrenadeSafeDv[2];
    private AudioSource sound;
    private SoundSignal signal;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();   
        signal = GetComponent<SoundSignal>();
    }

    private void Start()
    {
        StartCoroutine("Explosion");
    }

    IEnumerator Explosion()
    {
        float time = 0;
        while (true)
        {
            if (isActivate)
            {
                time += Time.deltaTime;
                if (time > destroyTime) 
                {
                    sound.Stop();
                    signal.stopSound = true;
                    //gameObject.SetActive(false);
                }
            }
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if (safeDv[0].isReady && safeDv[1].isReady)
            {
                sound.Play();
                isActivate = true;
                safeDv[0].isReady = false;
                safeDv[1].isReady = false;
            }
        }
    }
}
