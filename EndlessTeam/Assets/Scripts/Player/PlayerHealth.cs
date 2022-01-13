using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    ObjectPooling objectPooling;

    private List<Material> playerMaterial = new List<Material>();
    [SerializeField]
    PlayerMovement playerMovement;
    [SerializeField]
    GameObject GameOver = default;

    public bool once = true;

    [SerializeField]
    HealthBar healthBar = default;

    [SerializeField]
    Game gameScript;

    float initialSpeed;

    [SerializeField]
    int maxHealth = default;
    public int currentHealth;

    //Colori dei pezzi del giocatore
    List<Color> playerColor = new List<Color>();

    [SerializeField]
    Animator animator = default;

    public bool canBeHit = true;

    bool isSLam;

    float gameManagerSpeed;

    public bool canCollide = true;
    //(GABRIELE)
    //riferimento allo script del boss
    [SerializeField]
    private BossBehaviour bb = default;
    //indica quanti danni ottiene il giocatore dagli ostacoli
    [SerializeField]
    private int obstacleEnemyDmg = 1;

    [SerializeField] Animator dannoAnimator = default;

    //Materiali che verranno impostati in trasparenza per il feedback del danno
    private List<Material> fadeColor = new List<Material>();

    //MeshRenderer del player
    [SerializeField]
    private SkinnedMeshRenderer skinPlayer;
    //Transform di cui il figlio contiene il character dentro la tuta che verrà trovato via codice a run time
    [SerializeField]
    private Transform charParent;

    IEnumerator CanCollideCo()
    {
        canCollide = false;

        yield return new WaitForSeconds(1f);

        canCollide = true;

        yield return null;
    }

    private void Awake()
    {
        //Inserisco il materiale del personaggio che è un materiale singolo
        playerMaterial.Add(charParent.GetComponentInChildren<SkinnedMeshRenderer>().material);
        //Inserisco i materiali del player in playerMaterial
        foreach(Material mat in skinPlayer.materials)
        {
            //Se non è il materiale del vetro
            if (!mat.name.Contains("vetro")) playerMaterial.Add(mat);
        }
    }
    void Start()
    {
        gameManagerSpeed = GameManager.instance.speed;
        gameScript = GameObject.FindObjectOfType<Game>();
        initialSpeed = GameManager.instance.speed;
      
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth / 2;
        healthBar.SetHealth(currentHealth);
        isSLam = GameManager.instance.playerGb.GetComponent<PowerUpsManager>();


        //Riempio in ordine i colori dei pezzi del giocatore
        for(int i=0; i< playerMaterial.Count; i++)
        {
            playerColor.Add(playerMaterial[i].color);

            fadeColor.Add(playerMaterial[i]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentHealth > 0 && canCollide)
        {
            if (other.CompareTag("Obstacle") || (LayerMask.LayerToName(other.gameObject.layer) == "Wall"))
            {
                StartCoroutine("HitCor", playerMaterial);
                TakeDamage(obstacleEnemyDmg);
                
                StartCoroutine("CanCollideCo");
            }
            if (other.CompareTag("Enemy") && !GameManager.instance.playerGb.GetComponent<PowerUpsManager>().inSlam)
            {
                if (!other.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Death"))
                {
                    StartCoroutine("HitCor", playerMaterial);
                    TakeDamage(obstacleEnemyDmg);

                    StartCoroutine("CanCollideCo");
                }
            }
            
        }
    }

    IEnumerator HitCor()
    {
        if (!GameManager.instance.playerDeath)
        {
            GameManager.instance.speed = GameManager.instance.speed / 1.3f;
            //Per quattro volte
            for (int i = 0; i < 4; i++)
            {
                //Aspetto un po'
                yield return new WaitForSeconds(.3f);
                //Metto i colori in fade
                for (int k = 0; k < playerMaterial.Count; k++)
                {
                    Debug.Log(playerMaterial[i].name);
                    Color fadeCol = fadeColor[k].color;
                    fadeCol.a = .4f;
                    playerMaterial[k].color = fadeCol;
                    //currentMesh.material.color = fadeColor;
                    yield return null;
                }
                //Aspetto un po'
                yield return new WaitForSeconds(.2f);
                //Rimetto i colori originali
                for (int j = 0; j < playerMaterial.Count; j++)
                {
                    playerMaterial[j].color = playerColor[j];
                }
                yield return null;
            }
            //Resetto la velocità a quella iniziale
            GameManager.instance.speed = initialSpeed;

            yield return null;
        }
        else yield return null;
        

    }

    public void TakeDamage(int value)
    {
        //Se la vita è maggiore di zero, quindi sto subendo danno e non mi sto curando faccio apparire un effetto visivo di damage
        if (value > 0) dannoAnimator.SetTrigger("danno");

        currentHealth -= value;
        if (currentHealth > 0)
            healthBar.SetHealth(currentHealth);
        else healthBar.SetHealth(0);

        if (currentHealth <= 0)
        {
            GameManager.instance.preDeath = true;
            animator.SetTrigger("Death");
        }
        //(GABRIELE)Cambia la distanza tra il giocatore e il boss
        if (bb != null) { bb.ChangeZDistanceToPlayer(/*value*/(maxHealth - currentHealth), true); }

    }

    public void Death()
    {
        //SceneManager.LoadScene(0); //emanuele prima 1
        //GameManager.instance.speed = initialSpeed;

        if (once)
        {
            GameOver.SetActive(true);
            bb.playerDefeated = true;
            once = false;
        }

        ResetHealth();

    }

    public void InstantDeath()
    {
        TakeDamage(9999);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth / 2;

        healthBar.SetHealth(currentHealth);
    }
    //IEnumerator OpenGameOver()
    //{
    //    yield return new WaitForSecondsRealtime()
    //}

    //(GABRIELE)
    public int GetMaxHealth() { return maxHealth; }

}