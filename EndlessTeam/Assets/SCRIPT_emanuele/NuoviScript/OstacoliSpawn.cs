using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OstacoliSpawn : MonoBehaviour
{
    public GameObject[] ostacoliPrefab;

    public GameObject[] spawnPoint;

    private int rndOstacoliPos;
    public int rndOstacoli;

    public int canSpawn;

    GameObject ostacolo;

    //public static int hitCounter;

    private void Awake()
    {
      //  RandomValues();
    }

    private void OnEnable()
    {

        SpawnOstacoli();
    }

    private void OnDisable()
    {
        RandomValues();

        Destroy(ostacolo);
    }

    void SpawnOstacoli()
    {

        RandomValues();

        if (canSpawn == 0 || canSpawn == 1)
        {
          ostacolo = (GameObject)Instantiate(ostacoliPrefab[rndOstacoli], spawnPoint[rndOstacoliPos].gameObject.transform.position, Quaternion.identity);
          ostacolo.transform.parent = gameObject.transform;
        }


    }

    void RandomValues()
    {

        rndOstacoli = Random.Range(0, 2);
        rndOstacoliPos = Random.Range(0, 3);
        canSpawn = Random.Range(0, 3);

    }
 

    void Update()
    {
         
    }
}
