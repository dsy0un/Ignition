using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
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
    private GameObject enemyPrefab; // Enemy Prefab, NO UNPACK ENEMY GAMEOBJECT ¿Ö ¿µ¾î·Î ¾¸?
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private NavMeshSurface nms;

    private List<GameObject> pools = new(); // NO MODIFICATION
    private bool canSpawn = false;

    private void Start()
    {
        for (int i = 0; i < genMaxCount; i++)
        {
            GameObject spawn = Instantiate(enemyPrefab, GenEnemy(), Quaternion.identity, gameObject.transform);
            spawn.GetComponent<EnemyMove>().target = target;
            spawn.GetComponent<EnemyMove>().nms = nms;
            spawn.SetActive(false);
            pools.Add(spawn);
        }
        canSpawn = true;
    }
    public void StartSpawn()
    {
        if(!canSpawn) return;
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

    public Vector3 GenEnemy()
    {
        // Dictionary<string, List<Collider>> obj = GetGenObj();

        //for (int i = 0; i < genObj.Count; i++) 
        //{
        //    List<Collider> listObj = obj["genObj" + i];
        //    x = listObj[0].bounds.size.x;
        //    z = listObj[0].bounds.size.x;
        //    originPosition = listObj[0].transform.position;
        //}

        float x = genObj.bounds.size.x;
        float z = genObj.bounds.size.z;
        Vector3 originPosition = genObj.transform.position;

        float range_X = Random.Range(-(x / 2), x / 2);
        float range_Z = Random.Range(-(z / 2), z / 2);
        Vector3 randomPostion = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + randomPostion;

        return respawnPosition;
    }

    private IEnumerator RandomRespawn()
    {
        for (int i = 0; i < pools.Count; i++)
        {
            if (!pools[i].activeInHierarchy) pools[i].SetActive(true);
            yield return new WaitForSeconds(genCooldown);
        }
    }
}
