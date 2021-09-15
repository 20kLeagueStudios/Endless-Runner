using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    ObjectPooling objectPooling;
    [SerializeField]
    SkinnedMeshRenderer[] playerMesh;
    [SerializeField]
    PlayerMovement playerMovement;
    [SerializeField]
    GameObject GameOver;

    public bool once = true;

    [SerializeField]
    HealthBar healthBar;

    [SerializeField]
    Game gameScript;

    float initialSpeed;

    [SerializeField]
    int maxHealth;
    public int currentHealth;

    Color[] playerColor;

    [SerializeField]
    Animator animator;

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

    [SerializeField] Animator dannoAnimator;

    IEnumerator CanCollideCo()
    {
        canCollide = false;

        yield return new WaitForSeconds(1f);

        canCollide = true;

        yield return null;
    }

    void Start()
    {
        gameManagerSpeed = GameManager.instance.speed;
        gameScript = GameObject.FindObjectOfType<Game>();
        initialSpeed = GameManager.instance.speed;
        /*
        for (int i = 0; i < playerMesh.Length; i++)
        {
            playerColor[i] = playerMesh[i].material.color;
        }
        */
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth / 2;
        healthBar.SetHealth(currentHealth);
        isSLam = GameManager.instance.playerGb.GetComponent<PowerUpsManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentHealth > 0 && canCollide)
        {
            if (other.CompareTag("Obstacle") || (LayerMask.LayerToName(other.gameObject.layer) == "Wall"))
            {
                StartCoroutine("HitCor", playerMesh);
                TakeDamage(obstacleEnemyDmg);
                
                StartCoroutine("CanCollideCo");
            }
            if (other.CompareTag("Enemy") && !GameManager.instance.playerGb.GetComponent<PowerUpsManager>().inSlam)
            {
                if (!other.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Death"))
                {
                    StartCoroutine("HitCor", playerMesh);
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
            //Color fadeColor = meshToFade.material.color;

            //fadeColor.a = .1f;
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(.3f);
                //meshToFade.material.color = fadeColor;
                yield return new WaitForSeconds(.2f);
                //meshToFade.material.color = playerColor;
            }

            GameManager.instance.speed = initialSpeed;
        }
        




    }

    public void TakeDamage(int value)
    {
        dannoAnimator.SetTrigger("danno");

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