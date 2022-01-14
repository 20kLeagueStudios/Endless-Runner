using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; ///emanuele

public class ObjectPooling : MonoBehaviour
{
    #region Variabili

    bool tags;
 
    public static ObjectPooling instance = null;
    //Conterrà i tag associati ai GameObject creati con la lista poolList
    Dictionary<string, List<GameObject>> dictPool = new Dictionary<string, List<GameObject>>();
    [SerializeField]
    //Lista di istanze di tipo Pool
    List<Pool> poolList = new List<Pool>();
    //Carreggiata vuota da far apparire 6 volte ad inizio partita
    [SerializeField]
    GameObject emptyTile = default;



    //Lista di carreggiate attive che le farà muovere all'indietro
    public List<GameObject> activeTiles = new List<GameObject>();

    [SerializeField]
    GameObject[] tutorialTiles = default;

    //Carreggiate massime iniziali con l'istanza emptyTile
    [SerializeField]
    int maxTiles = default;
    //Velocità di movimento delle carreggiate
    public float speed; // 36;
    //Riferimento al renderer per calcolare la differenza di distanza del renderer per capire dove posizionare 
    //la prossima carreggiata
    Renderer rend;

    public float maxSpeed = 76;


    GameObject parentTiles; ////emanuele

    Dictionary<string, Material[]> biomes = new Dictionary<string, Material[]>();

    

    bool tutorial = true;

    string sceneName; //emanuele

    //Numero intero per indicare il pezzo tutorial successivo da istanziare
    private int tutIndex = 0;

    //Booleana che indica se è il primo bioma, quello iniziale oppure un bioma che verrà caricata in seguito
    [SerializeField]
    private bool isFirst = false;

    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        sceneName = gameObject.scene.name;
        parentTiles = new GameObject("parentTiles" + sceneName);////emanuele

        parentTiles.tag = "ParentTile";

    }

    void Start()
    {
    
        for (int i = 0; i < GameManager.instance.othersMat.Length; i++)
        {
            biomes.Add(GameManager.instance.othersMat[i].name, GameManager.instance.othersMat[i].mat);
        }
        for (int i = 0; i < GameManager.instance.biomes.Length; i++)
        {
            biomes.Add(GameManager.instance.biomes[i].name, GameManager.instance.biomes[i].mat);

        }

        speed = GameManager.instance.speed;

        rend = emptyTile.transform.GetChild(0).GetComponent<Renderer>();

        //Istanzia tutti i GameObject presenti nella lista di Pool e li inserisco nel dizionario con il tag
        //associato
        foreach (Pool temp in poolList)
        {
            List<GameObject> tempList = new List<GameObject>();
            for(int i=0; i<temp.prefab.Length; i++)
            {
                GameObject Obj = Instantiate(temp.prefab[i], transform.position, Quaternion.identity);

               
                Obj.transform.parent = parentTiles.transform;////emanuele
                
                Obj.SetActive(false);
                tempList.Add(Obj);
            }

       
                
            dictPool.Add(temp.GetTag, tempList);
        }
        //Chiamo il metodo che si occupa di creare le carreggiate tutorial
        if (GameManager.instance.firstGame)
        {
            SceneManager.MoveGameObjectToScene(parentTiles, SceneManager.GetSceneByName(sceneName));
            for (int i = 0; i < 3; i++)
            {
                AddTutorialTile();
            }
        }
        else
        {
            tutorial = false;
            SceneManager.MoveGameObjectToScene(parentTiles, SceneManager.GetSceneByName(sceneName));

            for (int i = 0; i < maxTiles; i++)
            {
                AddTile();
            }
        }

    }

    private void AddTutorialTile()
    {

        //Istanzio il tutorial successivo basandomi sull'indice tutIndex
        //Controllo che il tutorial index che voglio creare non è fuori dall'array
        if (tutIndex <= tutorialTiles.Length-1)
        {
            GameObject tile = Instantiate(tutorialTiles[tutIndex], transform.position, Quaternion.identity);
            //Se il tile ottenuto esiste posso lavorare su di esso
            if (tile)
            {
                //Setto il suo parent 
                tile.transform.parent = parentTiles.transform;
                float temp = rend.bounds.extents.z * 2;
                // position tile's z at 0 or behind the last item added to tiles collection
                float zPos = activeTiles.Count == 0 ? 130f : activeTiles[activeTiles.Count - 1].transform.position.z + temp;
                //Posiziono il tile
                tile.transform.position = new Vector3(0f, 0f, zPos);
                //Lo inserisco tra i tile attivi così che verrà disattivato automaticamente dopo
                activeTiles.Add(tile);
                //Lo attivo
                tile.SetActive(true);
                //Aumento l'index se è minore dell'ultimo tutorial
                if (tutIndex < tutorialTiles.Length) tutIndex++;
                //altrimenti vuol dire che è stato posizionato l'ultimo pezzo quindi imposto il tutorial a false e istanzio i tiles classici randomici
                else
                {
                    tutorial = false;
                    //Imposto che la prima partita è stata fatta così da non presentare più il tutorial
                    GameManager.instance.firstGame = false;
                    //Creo i pezzi randomici
                    for (int i = 0; i < maxTiles; i++)
                    {
                        AddTile();
                    }
                }
            }
        }
        else
        {
            tutorial = false;
            //Imposto che la prima partita è stata fatta così da non presentare più il tutorial
            GameManager.instance.firstGame = false;
            //Creo i pezzi randomici
            for (int i = 0; i < maxTiles; i++)
            {
                AddTile();
            }
        }
    }

    //Aggiunge un tile alla fine della carreggiata
    private void AddTile()
    {
        GameObject tile = GetTile();
        if (tile)
        {

            float temp = rend.bounds.extents.z * 2;
            // position tile's z at 0 or behind the last item added to tiles collection
            float zPos = activeTiles.Count == 0 ? 130f : activeTiles[activeTiles.Count - 1].transform.position.z + temp;
            tile.transform.position = new Vector3(0f, 0f, zPos);
       
            activeTiles.Add(tile);
            tile.SetActive(true);
        }


    }

    public void CheckPointOffset()
    {
        float temp = rend.bounds.extents.z * 2;

        activeTiles[0].transform.position = new Vector3(0, 0, 120);

        for (int i = 1; i < activeTiles.Count; i++) {
            float zPos = activeTiles[i-1] != null ? activeTiles[i-1].transform.position.z + temp : 0;
            activeTiles[i].transform.position = new Vector3(0,0,zPos);
        }
      
    }

    public void PortalOffset()
    {
        float temp = rend.bounds.extents.z * 2;
        float zPortPos = GameManager.instance.portalPos.z;

        activeTiles[0].transform.position = new Vector3(0, 0, (zPortPos) + temp - 10f);

        //Debug.Log("Posizione portale: " + zPortPos);

        for (int i = 1; i < activeTiles.Count; i++)
        {
            float zPos = activeTiles[i - 1] != null ? activeTiles[i - 1].transform.position.z + temp : 0;
            activeTiles[i].transform.position = new Vector3(0, 0, zPos);
        }

    }

    //Ritorna un tile random che dipende solo dalla difficoltà corrente
    private GameObject GetTile()
    {
        List<GameObject> tempList = dictPool[GameManager.instance.GetMode()];

        ShuffleList(tempList);
        if (tempList != null)
        {
            for (int i = 0; i < tempList.Count; i++)
            {
                if (tempList[i].activeInHierarchy)
                    continue;

                //riattivo tutti i contenuti(monete e nemici) del pezzo che sto ritornando
                ReActiveObjs(tempList[i]);
                return tempList[i];
                
            }
        }
        return null;
    }

    private void ReActiveObjs(GameObject value)
    {
        Transform[] trs = value.GetComponentsInChildren<Transform>(true);
        foreach (Transform temp in trs)
        {
            bool tags = temp.CompareTag("Enemy") || temp.CompareTag("Money");
            if (tags)
                temp.gameObject.SetActive(true);
        }

    }

    //Muove i le carreggiate all'indietro lungo l'asse Z
    //Quando sono dietro la telecamera le disattiva e aggiunge la prossima
    public void UpdateTiles()
    {
        for (int i = activeTiles.Count - 1; i >= 0; i--)
        {
            GameObject tile = activeTiles[i];
            tile.transform.Translate(0f, 0f, -GameManager.instance.speed * Time.deltaTime);

            // If a tile moves behind the camera release it and add a new one
            if (tile.transform.position.z < (Camera.main.transform.position.z * 1.5f))
            {
                activeTiles.RemoveAt(i);
                DisableObject(tile);

                if (!tutorial) {
                    Debug.Log(activeTiles.Count-1);
                    if (activeTiles.Count < maxTiles) AddTile();
                }
                else AddTutorialTile();
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

        //ReActiveObjs(obj);
        obj.SetActive(false);
    }

    public void ChangeMatFromTo(int scene1, int scene2)
    {
        GameObject scene1parent = default, scene2parent = default;

        Renderer boss = GameManager.instance.boss;

        Scene firstScene = SceneManager.GetSceneByBuildIndex(scene1),
              secondScene = SceneManager.GetSceneByBuildIndex(scene2);

        GameObject[] allObjFirstScene = firstScene.GetRootGameObjects(),
                     allObjSecondScene = secondScene.GetRootGameObjects();

        Material mat1 = default, mat2 = default, enemyMat = default;
        

        for (int i=0; i<allObjFirstScene.Length; i++)
        {
            
            if (allObjFirstScene[i].CompareTag("ParentTile"))
            {
                if (allObjFirstScene[i].transform.GetChild(0).CompareTag("Fungo")) { mat1 = biomes["Fungo"][1]; }
                else if (allObjFirstScene[i].transform.GetChild(0).CompareTag("Deserto")) { mat1 = biomes["Deserto"][1]; }
                else if (allObjFirstScene[i].transform.GetChild(0).CompareTag("Cristallo")) { mat1 = biomes["Cristallo"][1]; }
                else if (allObjFirstScene[i].transform.GetChild(0).CompareTag("Ghiaccio")) { mat1 = biomes["Ghiaccio"][1]; }
                else if (allObjFirstScene[i].transform.GetChild(0).CompareTag("Lava")) { mat1 = biomes["Lava"][1];}
                scene1parent = allObjFirstScene[i];
                break;
            }
        }

        for(int i=0; i<allObjSecondScene.Length; i++)
        {
         
            if (allObjSecondScene[i].CompareTag("ParentTile"))
            {
    
                if (allObjFirstScene[i].transform.GetChild(0).CompareTag("Fungo")) { mat2 = biomes["Fungo"][0]; enemyMat = biomes["NemiciFunghi"][0]; }
                else if (allObjFirstScene[i].transform.GetChild(0).CompareTag("Deserto")) { mat2 = biomes["Deserto"][0]; enemyMat = biomes["NemiciDeserto"][0]; }
                else if (allObjFirstScene[i].transform.GetChild(0).CompareTag("Cristallo")) { mat2 = biomes["Cristallo"][0]; enemyMat = biomes["NemiciCristalli"][0]; }
                else if (allObjFirstScene[i].transform.GetChild(0).CompareTag("Ghiaccio")) { mat2 = biomes["Ghiaccio"][0]; enemyMat = biomes["NemiciGhiaccio"][0]; }
                else if (allObjFirstScene[i].transform.GetChild(0).CompareTag("Lava")) { mat2 = biomes["Lava"][0]; enemyMat = biomes["NemiciLava"][0]; }
                scene2parent = allObjSecondScene[i];
                break;
            }
        }

        Renderer[] rendererFirst = default, rendererSecond = default;
        if (scene1parent.gameObject != null)
            rendererFirst = scene1parent.GetComponentsInChildren<Renderer>();
        if (scene2parent.gameObject != null)
            rendererSecond = scene2parent.GetComponentsInChildren<Renderer>();

       
        for (int i=0; i<rendererFirst.Length; i++)
        {
            Renderer temp = rendererFirst[i];
   
            tags = temp.CompareTag("Money") || temp.CompareTag("Enemy") || temp.CompareTag("Portal") || temp.CompareTag("PortalShader") || temp.CompareTag("PowerUp");
            if (!tags)
                temp.material = mat1;
            else if (temp.CompareTag("Money")) temp.material = biomes["Money"][1];
            else if (temp.CompareTag("Enemy")) temp.material = biomes["Enemy"][1];
            else if (temp.CompareTag("Portal")) temp.material = biomes["Portal"][1];
            else if (temp.CompareTag("PowerUp"))
            {
                //Controllo il tipo di power up ottenendo il suo script
                PowerUpStarter powerUp = temp.transform.parent.GetComponent<PowerUpStarter>();

                temp.material = biomes["PowerUp" + powerUp.GetPowerUpType()][1];
            }
        }

        for (int i=0; i<rendererSecond.Length; i++)
        {
            Renderer temp = rendererSecond[i];
            tags = temp.CompareTag("Money") || temp.CompareTag("Enemy") || temp.CompareTag("Portal") || temp.CompareTag("PortalShader") || temp.CompareTag("PowerUp");
            if (!tags)
                rendererSecond[i].material = mat2;
            else if (temp.CompareTag("Money")) temp.material = biomes["Money"][0];
            //else if (temp.CompareTag("Enemy")) temp.material = biomes["Enemy"][0];
            else if (temp.CompareTag("Enemy")) temp.material = enemyMat;
            else if (temp.CompareTag("Portal")) temp.material = biomes["Portal"][0];
            else if (temp.CompareTag("PowerUp"))
            {
                //Controllo il tipo di power up ottenendo il suo script
                PowerUpStarter powerUp = temp.transform.parent.GetComponent<PowerUpStarter>();

                temp.material = biomes["PowerUp" + powerUp.GetPowerUpType()][0];
            }
        }

        boss.material = mat2;


    }

    public void ChangeMatFromTo(int scene)
    {
        //parentTiles.SetActive(true);

        //Debug.Log("Attivato " + parentTiles.name);

        GameObject scene1parent = default;

        Scene firstScene = SceneManager.GetSceneByBuildIndex(scene);

        GameObject[] allObjFirstScene = firstScene.GetRootGameObjects();

        Material mat = default, enemyMat = default;

        ObjectPooling scenePooling = default;


        for (int i = 0; i < allObjFirstScene.Length; i++)
        {
            if(allObjFirstScene[i].CompareTag("ParentTile"))
            {
                //allObjFirstScene[i].SetActive(false);
                if (allObjFirstScene[i].transform.GetChild(0).CompareTag("Fungo")) { mat = biomes["Fungo"][1]; enemyMat = biomes["NemiciFunghi"][1]; }
                else if (allObjFirstScene[i].transform.GetChild(0).CompareTag("Deserto")) { mat = biomes["Deserto"][1]; enemyMat = biomes["NemiciDeserto"][1]; }
                else if (allObjFirstScene[i].transform.GetChild(0).CompareTag("Cristallo")) { mat = biomes["Cristallo"][1]; enemyMat = biomes["NemiciCristalli"][1]; }
                else if (allObjFirstScene[i].transform.GetChild(0).CompareTag("Ghiaccio")) { mat = biomes["Ghiaccio"][1]; enemyMat = biomes["NemiciGhiaccio"][1]; }
                else if (allObjFirstScene[i].transform.GetChild(0).CompareTag("Lava")) { mat = biomes["Lava"][1]; enemyMat = biomes["NemiciLava"][1]; }
                scene1parent = allObjFirstScene[i];
                
            } else if (allObjFirstScene[i].CompareTag("Manager"))
            {
                scenePooling = allObjFirstScene[i].GetComponent<ObjectPooling>();
            }
        }      

        Renderer[] rendererFirst = default;
        if (scene1parent.gameObject != null)
            rendererFirst = scene1parent.GetComponentsInChildren<Renderer>();

        if (scenePooling != null) scenePooling.PortalOffset();

        for (int i = 0; i < rendererFirst.Length; i++)
        {
            Renderer temp = rendererFirst[i];
          
            tags = temp.CompareTag("Money") || temp.CompareTag("Enemy") || temp.CompareTag("Portal") || temp.CompareTag("PortalShader") || temp.CompareTag("PowerUp");
            if (!tags)
            {
                temp.material = mat;
                if (!temp.CompareTag("Invisible")) temp.enabled = true;
            }
            else if (temp.CompareTag("Money")) { temp.material = biomes["Money"][1]; temp.enabled = true; }
            else if (temp.CompareTag("Enemy")) { temp.material = enemyMat; temp.enabled = true; }
            else if (temp.CompareTag("Portal")) { temp.material = biomes["Portal"][1]; temp.enabled = true; }
            else if (temp.CompareTag("PowerUp"))
            {
                //Controllo il tipo di power up ottenendo il suo script
                PowerUpStarter powerUp = temp.transform.parent.GetComponent<PowerUpStarter>();

                temp.material = biomes["PowerUp" + powerUp.GetPowerUpType()][1];
                temp.enabled = true;
            }
        }

        scene1parent.SetActive(true);
    }
}

[System.Serializable]
//Classe che avrà un tag che sarà associato all' array di GameObject
//Esempio: Array di tile facile con le sue varianti avrà il tag EasyTile
class Pool
{
    [SerializeField]
    string tag = default;

    public GameObject[] prefab = default;

    public string GetTag { get { return tag; } }

    

}