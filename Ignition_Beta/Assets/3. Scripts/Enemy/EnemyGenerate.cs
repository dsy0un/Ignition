using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    [SerializeField]
    // private List<Collider> genObj; // Enemy Generate Collider, role : Generate Position
    private Collider genObj; // Enemy Generate Collider, role : Generate Position
    [SerializeField]
    private float genCooldown; // Enemy Generate Cooldown
    [SerializeField]
    private int genMaxCount; // Enemy Max Generate
    [SerializeField]
    private GameObject enemyPrefab; // Enemy Prefab, NO UNPACK ENEMY GAMEOBJECT

    private void Start()
    {
        StartCoroutine(RandomRespawn());
    }

    //private Dictionary<string, List<Collider>> GetGenObj()
    //{
    //    Dictionary<string, List<Collider>> dict = new();
    //    for (int i = 0; i < genObj.Count; i++)
    //    {
    //        var obj = genObj[i];

    //        dict["genObj" + i] = new List<Collider> { obj };
    //    }

    //    return dict;
    //}

    private Vector3 GenEnemy()
    {
        // Dictionary<string, List<Collider>> obj = GetGenObj();

        float x = 0;
        float z = 0;
        Vector3 originPosition = Vector3.zero;
        //for (int i = 0; i < genObj.Count; i++) 
        //{
        //    List<Collider> listObj = obj["genObj" + i];
        //    x = listObj[0].bounds.size.x;
        //    z = listObj[0].bounds.size.x;
        //    originPosition = listObj[0].transform.position;
        //}

        x = genObj.bounds.size.x;
        z = genObj.bounds.size.z;
        originPosition = genObj.transform.position;

        float range_X = Random.Range(-(x / 2), x / 2);
        float range_Z = Random.Range(-(z / 2), z / 2);
        Vector3 randomPostion = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + randomPostion;

        return respawnPosition;
    }

    private IEnumerator RandomRespawn()
    {
        while (genMaxCount > 0)
        {
            yield return new WaitForSeconds(genCooldown);
            GameObject spawn = Instantiate(enemyPrefab, GenEnemy(), Quaternion.identity);
            genMaxCount--;
        }
    }
}
