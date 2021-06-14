using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTileManager : MonoBehaviour
{
    // numero massimo di tile visibili allo stesso momento
    static int MAX_TILES = 6;

    //velocità massima delle tiles
    [Range(0f, 50f)]
    public float maxSpeed = 10f;

    //array dei tipi di tiles
    public GameObject[] tileTypes;

    //dimensione sull asse Z delle tiles (forse meglio public per controllarla nell'inspector)
    private float tileSize = 22.86f; //dipende dalla grandezza della tile

    //current speed delle tiles
    private float speed;

    //lista delle tiles attive
    private List<GameObject> tiles;

    //object pooling delle tiles, è un custom script che si trova qui, più sotto
    private TilePool tilePool;

    //iniziliazziamo allo start velocità di partenza e due liste, una per le tiles attive ed una che gestisce l'object pooling delle tiles
    public void Init() //viene usato nello Start() dello script Game
    {
        this.speed = 0f;
        this.tiles = new List<GameObject>();

        //il costruttore di tilepool vuole: un array di tipi di tiles, il numero massimo di tiles visibili a schermo contemporanemante e un Transform

        this.tilePool = new TilePool(this.tileTypes, MAX_TILES, this.transform); 
        InitTiles();
    }

    //incremento della velocità di spawn
    public void IncreaseSpeed(float amt)
    {
        this.speed += amt;
        if (this.speed > this.maxSpeed) //se per qualsiasi motivo la velocità di spawn delle tile supera il valore massimo settiamo la speed a maxspeed
            this.speed = maxSpeed;
    }

   //aggiorniamo le tiles
    public void UpdateTiles(System.Random rnd)
    {
        for (int i = tiles.Count - 1; i >= 0; i--) //tiles = lista di GO 
        {

            GameObject tile = tiles[i]; //inizializziamo una tile che è uguale alla tile a indice i della lista tiles
            tile.transform.Translate(0f, 0f, -this.speed * Time.deltaTime); //muoviamo la tile su Z verso il giocatore

       
            //se la tile finisce dietro la camera viene rimossa e ne viene aggiunta un'altra
            if (tile.transform.position.z < Camera.main.transform.position.z)
            {
                this.tiles.RemoveAt(i); //rimuoviamo la tile dalla lista delle tile attive
                this.tilePool.ReleaseTile(tile); //disattiviamo la tile dalla lista dell'object pooling
                int type = rnd.Next(0, this.tileTypes.Length); //Next() vuole due parametri: numero minimo e massimo di tipi di tiles. rnd.next= un tipo a caso tra i due valori
                AddTile(type);//aggiungiamo la nuova tile alla lista
            }
        }
    }

    //aggiungiamo le tile alla lista
    private void AddTile(int type)
    {
        GameObject tile = this.tilePool.GetTile(type); //perschiamo la tile dal pool, vedi funzione nella classe TilePool

        //posizione su z della tile a 0 o dietro l'ultima tile aggiunta alla lista
 
        //se la lista di tiles è vuota zPos è 0 altrimenti è uguale alla posizione in z dell ultima tile attiva + la sua dimensione su Z (in questo caso 22.86f)
        float zPos = this.tiles.Count == 0 ? 0f : this.tiles[this.tiles.Count - 1].transform.position.z + this.tileSize;
        tile.transform.Translate(0f, 0f, zPos); 
        this.tiles.Add(tile);
    }

    //inizializziamo le tiles. usato nell'ultima riga della funzione Init() che viene usata nello Start dello script Game
    private void InitTiles()
    {
        for (int i = 0; i < MAX_TILES; i++)
        {
            AddTile(0); //iniziamo il livello con MAX_TILES (per adesso 6) di tipo 0 (quelle senza ostacoli)
        }
    }

    //object pooling delle tiles
    class TilePool
    {
        /** Pool of Tiles */
        private List<GameObject>[] pool; //lista di array di GameObj (le tiles)

        //transform del modello
        private Transform transform;

        //costruttore
        public TilePool(GameObject[] types, int size, Transform transform) //argomenti: array di gameobj (i tipi di tiles), dimensione del pool, transform
        {
         
            this.transform = transform;
            int numTypes = types.Length; //il numero di tipi di tiles è uguale alla lunghezza dell'array passato come primo argomento
            this.pool = new List<GameObject>[numTypes];  //inizializziamo la lista pool usando il numero di tipi di tiles come dimensione
            for (int i = 0; i < numTypes; i++)
            {
                this.pool[i] = new List<GameObject>(size); //inizializziamo la lista usando il numero max di tiles che compariranno a schermo come parametro per la sua dimensione

                //cicliamo fino a quando j non è uguale a size (= numero massimo tiles che devono comparire a schermo) e iistanziamo le tile
                for (int j = 0; j < size; j++) 
                {
                    GameObject tile = (GameObject)Instantiate(types[i]); //istanziamo come GO le tile
                    tile.SetActive(false); //le disattiviamo
                    this.pool[i].Add(tile); //le aggiungiamo al pool
                }
            }
        }

        /** Get a Tile */
        public GameObject GetTile(int type)
        {
            for (int i = 0; i < this.pool[type].Count; i++)
            {
                GameObject tile = this.pool[type][i];
                // Ignore active tiles until we find the 1st inactive in appropriate list
                if (tile.activeInHierarchy)
                    continue;

                // reset the tile's transform to match the model transform
                tile.transform.position = this.transform.position;
                tile.transform.rotation = this.transform.rotation;

                // set to active and return
                tile.SetActive(true);
                return tile;
            }

            // This will never be reached, but compiler requires it
            return null;
        }

        //disattiviamo la tile quando va dietro la camera (vedi funzione UpdateTiles sopra)
        public void ReleaseTile(GameObject tile)
        {
         
            tile.SetActive(false);
        }
    }
}
