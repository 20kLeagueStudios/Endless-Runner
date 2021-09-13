//Si occupa del comportamento del boss
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    //riferimento alla posizione in cui il boss deve essere rispetto al giocatore
    [SerializeField]
    private Transform bossPositionToPlayer = default;
    //indica la distanza che deve esserci tra il boss e il giocatore(determinato dall vita del giocatore)
    [SerializeField]
    private float minZDistance = -300, maxZDistance = 300;
    //indica quanto velocemente il boss cambia posizione
    [SerializeField]
    private float smoothChangePos = 10;
    //indica la posizione Z iniziale, da cui poi far avvicinare il boss
    //private float startZPosition;
    //indica di quanto deve essere moltiplicata la distanza ricevuta
    private float moltZDistance;
    //riferimento allo script di vita del giocatore
    [SerializeField]
    private PlayerHealth ph = default;
    //indica se il giocatore ha perso o meno
    [HideInInspector]
    public bool playerDefeated = false;


    // Start is called before the first frame update
    void Start()
    {
        //calcola la distanza minima che il boss può avere rispetto al giocatore
        minZDistance += bossPositionToPlayer.localPosition.z;
        //calcola la distanza massima
        maxZDistance = minZDistance + (maxZDistance * 2);
        //il boss viene inizialmente messo alla posizione giusta(dietro il giocatore)
        transform.position = bossPositionToPlayer.position;
        //ottiene la posizione da cui iniziare i calcoli per la distanza
        //startZPosition = maxZDistance;

        moltZDistance = (maxZDistance - minZDistance) / ph.GetMaxHealth();
        Debug.LogError("Sono Gabriele, nello script BossBehaviour ho fatto questo errore e faccio girare il boss. Quando si avrà la mesh finale, disattivare rotazione");
    }

    // Update is called once per frame
    void Update()
    {
        //(GABRIELE)DA RIMUOVERE QUANDO SI AVRA' LA BUILD FINALE
        transform.Rotate(0, 90 * Time.deltaTime, 0);

    }

    private void FixedUpdate()
    {
        //la posizione del boss viene cambiata a quella in cui deve essere a poco a poco
        if(!playerDefeated) transform.position = Vector3.Lerp(transform.position, bossPositionToPlayer.position, (smoothChangePos * Time.deltaTime));

    }

    public void ChangeZDistanceToPlayer(float zDistance, bool getCloser)
    {
        //se il giocatore è ancora vivo, cambia la distanza tra loro
        if (!playerDefeated)
        {
            //se il boss deve avvicinarsi, il parametro ottenuto viene portato al suo valore negativo
            if (getCloser) { zDistance = -zDistance; }
            //la distanza viene aumentata in base alla differenza tra la minima distanza e quella massima(diviso la vita massima del giocatore)
            zDistance *= moltZDistance;
            //Debug.Log("Aggiungi distanza: " + zDistance + " : è stata moltiplicata per " + moltZDistance);
            //float newBossZPosition = bossPositionToPlayer.localPosition.z + zDistance;
            float newBossZPosition = /*startZPosition*/maxZDistance + zDistance;
            //Debug.Log(newBossZPosition + " -> BossPos : " + minZDistance + " -> minDist : " + maxZDistance + " -> maxDist : " + bossPositionToPlayer.localPosition.z + " -> PreviaPos");
            newBossZPosition = Mathf.Clamp(newBossZPosition, minZDistance, maxZDistance);
            //cambia la posizione in base al valore calcolato
            bossPositionToPlayer.localPosition = new Vector3(bossPositionToPlayer.localPosition.x, bossPositionToPlayer.localPosition.y, newBossZPosition);
            //Debug.Log(newBossZPosition + " : " + bossPositionToPlayer.localPosition.z);
        }

    }

    public void PlayerRespawned()
    {
        //riporta la variabile a true, per indicare che il giocatore è vivo
        playerDefeated = false;
        //cambia la distanza tra il boss e il giocatore in base alla vita del giocatore respawnato
        ChangeZDistanceToPlayer(ph.currentHealth, false);

    }

}
