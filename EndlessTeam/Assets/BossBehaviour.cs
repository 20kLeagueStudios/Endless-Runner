using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    //riferimento alla posizione in cui il boss deve essere rispetto al giocatore
    [SerializeField]
    private Transform bossPositionToPlayer = default;
    //indica la distanza che deve esserci tra il boss e il giocatore(determinato dall vita del giocatore)
    private float zDistanceToPlayer;

    // Start is called before the first frame update
    void Start()
    {

        transform.position = new Vector3(transform.position.x, transform.position.y, bossPositionToPlayer.position.z);
        Debug.LogError("Sono Gabriele, nello script BossBehaviour ho fatto questo errore e faccio girare il boss. Quando si avrà la mesh finale, disattivare rotazione");
    }

    // Update is called once per frame
    void Update()
    {
        //(GABRIELE)DA RIMUOVERE QUANDO SI AVRA' LA BUILD FINALE
        transform.Rotate(0, 90 * Time.deltaTime, 0);

    }

}
