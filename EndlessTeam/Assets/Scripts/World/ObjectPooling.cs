using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    //Conterrà i tag associati ai GameObject creati con la lista poolList
    Dictionary<string, List<GameObject>> dictPool = new Dictionary<string, List<GameObject>>();
    [SerializeField]
    //Lista di istanze di tipo Pool
    List<Pool> poolList = new List<Pool>();
    //Carreggiata vuota da far apparire 6 volte ad inizio partita
    [SerializeField]
    GameObject emptyTile;

    //Lista di carreggiate attive che le farà muovere all'indietro
    List<GameObject> activeTiles = new List<GameObject>();
    //Carreggiate massime iniziali con l'istanza emptyTile
    int maxTiles = 6;
    //Velocità di movimento delle carreggiate

    public float speed = 36; // ora è public, emanuele
    public float maxSpeed = 70;

    //Riferimento al renderer per calcolare la differenza di distanza del renderer per capire dove posizionare 
    //la prossima carreggiata
    Renderer rend;
    //Timer prova
    float currentTimer;

    //  public float speed;

    //Enum sulla difficoltà
    enum Mode
    {
        Easy,
        Medium,
        Hard
    }

    //Riferimento all'enum Mode
    Mode mode;
    void Start()
    {

        currentTimer = Time.time;
        //Modalità iniziale a facile
        mode = Mode.Easy;
        //Istanzia tutti i GameObject presenti nella lista di Pool e li inserisco nel dizionario con il tag
        //associato
        foreach (Pool temp in poolList)
        {
            List<GameObject> tempList = new List<GameObject>();
            for (int i = 0; i < temp.prefab.Length; i++)
            {
                GameObject Obj = Instantiate(temp.prefab[i], transform.position, Quaternion.identity);
                Obj.SetActive(false);
                tempList.Add(Obj);
            }

            dictPool.Add(temp.GetTag, tempList);
        }

        //Chiamo il metodo che si occupa di creare le prime 6 carreggiate
        initialTiles();
    }

    public void IncreaseSpeed(float amt)
    {
        this.speed = amt;
        if (this.speed > this.maxSpeed)
            this.speed = maxSpeed;
    }

    private void Update()
    {

        //if (Time.time - currentTimer > 5f)
        //{
        //    if (mode == Mode.Easy)
        //        mode = Mode.Medium;
        //    else if (mode == Mode.Medium)
        //        mode = Mode.Hard;
        //    else mode = Mode.Easy;
        //    currentTimer = Time.time;
        //}


    }
    //Metodo iniziale che crea i primi sei tiles
    void initialTiles()
    {
        //Creo le prime 6 carreggiate vuote, così che il giocatore abbia il tempo di prepararsi
        //for (int i = 0; i < maxTiles; i++)
        //{
        //    GameObject tile = Instantiate(emptyTile, transform.position, Quaternion.identity);
        //    rend = emptyTile.transform.GetChild(1).GetComponent<Renderer>();
        //    float temp = rend.bounds.extents.z * 2;
        //    // position tile's z at 0 or behind the last item added to tiles collection
        //    float zPos = activeTiles.Count == 0 ? 0f : activeTiles[activeTiles.Count - 1].transform.position.z + temp;
        //    tile.transform.position = new Vector3(0f, 0f, zPos);
        //    activeTiles.Add(tile);
        //}
        //Metodo effettivo che aggiungerà le carreggiate in base alla difficolta
        for (int i = 0; i < maxTiles; i++)
            AddTile();
    }

    //Aggiunge un tile alla fine della carreggiata
    private void AddTile()
    {
        GameObject tile = GetTile();
        rend = emptyTile.transform.GetChild(1).GetComponent<Renderer>();
        float temp = rend.bounds.extents.z * 2;
        // position tile's z at 0 or behind the last item added to tiles collection
        float zPos = activeTiles.Count == 0 ? 0f : activeTiles[activeTiles.Count - 1].transform.position.z + temp;
        tile.transform.position = new Vector3(0f, 0f, zPos);
        activeTiles.Add(tile);
        tile.SetActive(true);
    }

    //Ritorna un tile random che dipende solo dalla difficoltà corrente
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



                return tempList[i];

            }
        }
        return null;
    }
    //Muove i le carreggiate all'indietro lungo l'asse Z
    //Quando sono dietro la telecamera le disattiva e aggiunge la prossima
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

    //Mescola la lista
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

    //Disattiva GameObject passato così potrà essere riutilizzato dal metodo GetTile
    public void DisableObject(GameObject obj)
    {

        obj.SetActive(false);
    }


}

[System.Serializable]
//Classe che avrà un tag che sarà associato all' array di GameObject
//Esempio: Array di tile facile con le sue varianti avrà il tag EasyTile
class Pool
{
    [SerializeField]
    string tag;

    public GameObject[] prefab;

    public string GetTag { get { return tag; } }



}