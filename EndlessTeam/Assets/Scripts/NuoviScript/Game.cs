using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    /** Speed Increase Value */
    static float speedIncrease = 24f;
    public float SpeedIncrease { 
        get {
                return speedIncrease;
            }
        set {
                speedIncrease = value;
            }
    }

    /** Seeded Randomizer */
    static System.Random RND;

    /** Tileholder */
    public GameObject TileHolder;

    /** TileManager */
    private WorldTileManager tileManager;

    public ObjectPooling objPooling;

    /** On Awake */
    void Awake()
    {
        // 32 is just an arbitrary seed number. Could be anything.
        //RND = new System.Random(32);
        //this.tileManager = TileHolder.GetComponent<WorldTileManager>();
    }

    /** On Start */
    void Start()
    {
        //this.tileManager.Init();
    }

    /** On Update */
    void Update()
    {
        //this.tileManager.IncreaseSpeed(speedIncrease);
        //this.tileManager.UpdateTiles(RND);
        objPooling.UpdateTiles();
    }
}
