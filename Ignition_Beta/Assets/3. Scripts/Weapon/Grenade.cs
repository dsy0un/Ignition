using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    [SerializeField] private float respawnTime;
    private bool isActivate;
    //[HideInInspector]
    public GrenadeSafeDv[] safeDv = new GrenadeSafeDv[2];
    private AudioSource sound;
    private SoundSignal signal;
    private KeepItem keepItem;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();   
        signal = GetComponent<SoundSignal>();
    }

    private void Start()
    {
        if (transform.parent.gameObject)
        {
            keepItem = transform.parent.GetComponent<KeepItem>();
        }
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
                    Debug.Log("Explosion");
                    sound.Stop();
                    signal.stopSound = true;
                    isActivate = false;
                    StartCoroutine(Respawn());
                    StopCoroutine(Explosion());
                }
            }
            yield return null;
        }
    }

    IEnumerator Respawn()
    {
        float time = 0;
        while (time <= respawnTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        Debug.Log("respawn");
        safeDv[0].gameObject.SetActive(true);
        safeDv[1].gameObject.SetActive(true);
        keepItem.ReSpawnItem();
        StopCoroutine(Respawn());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if (safeDv[0].isReady && safeDv[1].isReady)
            {
                sound.Play();
                safeDv[0].isReady = false;
                safeDv[1].isReady = false;
                isActivate = true;
                StartCoroutine(Explosion());
            }
        }
    }
}
