using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{

    public GameObject groundTile;
    Vector3 nextSpawnPoint;

   public void SpawnTile()
    {
       GameObject go = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);

        nextSpawnPoint = go.transform.GetChild(1).transform.position; // nel progetto il gameobject nextSpawnPoint è il secondo figlio del prefab groundtile, quindi a indice 1

    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10 ; i++)
        {
            SpawnTile();
        }

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
