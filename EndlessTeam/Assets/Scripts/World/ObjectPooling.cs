using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    Dictionary<string, List<GameObject>> dictPool = new Dictionary<string, List<GameObject>>();
    [SerializeField]
    List<Pool> poolList = new List<Pool>();

    List<GameObject> activeTiles = new List<GameObject>();
    float offset = 26f;
    int maxTiles = 6;
    float speed = 24;
    Renderer rend;
    float currentTimer;

    enum Mode
    {
        Easy,
        Medium,
        Hard
    }

    Mode mode;
    void Start()
    {
        currentTimer = Time.time;
        mode = Mode.Easy;
        foreach(Pool temp in poolList)
        {
            List<GameObject> tempList = new List<GameObject>();
            for(int i=0; i<temp.prefab.Length; i++)
            {
                GameObject Obj = Instantiate(temp.prefab[i], transform.position, Quaternion.identity);
                Obj.SetActive(false);
                tempList.Add(Obj);
            }

            dictPool.Add(temp.GetTag, tempList);
        }

        initialTiles();
    }

    private void Update()
    {
        if (Time.time - currentTimer > 5f)
        {
            Debug.Log("Prova");
            if (mode == Mode.Easy)
                mode = Mode.Medium;
            else if (mode == Mode.Medium)
                mode = Mode.Hard;
            else mode = Mode.Easy;
            currentTimer = Time.time;
        }
        

    }

    void initialTiles()
    {
        for (int i = 0; i < maxTiles; i++)
            AddTile();
    }
    private void AddTile()
    {
        GameObject tile = GetTile();
        rend = tile.transform.GetChild(1).GetComponent<Renderer>();
        float temp = rend.bounds.extents.z * 2;
        // position tile's z at 0 or behind the last item added to tiles collection
        float zPos = activeTiles.Count == 0 ? 0f : activeTiles[activeTiles.Count - 1].transform.position.z + temp;
        tile.transform.position = new Vector3(0f, 0f, zPos);
        activeTiles.Add(tile);
    }

    private GameObject GetTile()
    {
        List<GameObject> tempList = dictPool[mode.ToString()];
        ShuffleList(tempList);
        if (tempList != null)
        {
            for (int i = 0; i < tempList.Count; i++)
            {
                if (tempList[i].activeInHierarchy)
                    continue;


                tempList[i].SetActive(true);
                return tempList[i];
                
            }
        }
        return null;
    }
    public void UpdateTiles()
    {
        for (int i = activeTiles.Count - 1; i >= 0; i--)
        {
            GameObject tile = activeTiles[i];
            tile.transform.Translate(0f, 0f, -this.speed * Time.deltaTime);

            // If a tile moves behind the camera release it and add a new one
            if (tile.transform.position.z < Camera.main.transform.position.z)
            {
                activeTiles.RemoveAt(i);
                DisableObject(tile);
                AddTile();
            }
        }
    }

    void ShuffleList(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void DisableObject(GameObject obj)
    {
       
        obj.SetActive(false);
    }


}

[System.Serializable]
class Pool
{
    [SerializeField]
    string tag;

    public GameObject[] prefab;

    public string GetTag { get { return tag; } }

    

}