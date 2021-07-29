using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    ObjectPooling objectPooling;
    [SerializeField]
    MeshRenderer playerMesh;
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
    int currentHealth;

    Color playerColor;

    [SerializeField]
    Animator animator;

    public bool canBeHit = true;

    float gameManagerSpeed;

    void Start()
    {
        gameManagerSpeed = GameManager.instance.speed;
        gameScript = GameObject.FindObjectOfType<Game>();
        initialSpeed = GameManager.instance.speed;
        playerColor = playerMesh.material.color;
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth / 2;
        healthBar.SetHealth(currentHealth);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentHealth > 0)
        {
            if (other.CompareTag("Obstacle"))
            {
                StartCoroutine("HitCor", playerMesh);
                TakeDamage(1);
            }
        }
    }

    IEnumerator HitCor(MeshRenderer meshToFade)
    {
        
        GameManager.instance.speed = GameManager.instance.speed / 1.3f;
        Color fadeColor = meshToFade.material.color;

        fadeColor.a = .1f;
        for (int i = 0; i<4; i++)
        {
            yield return new WaitForSeconds(.3f);
            meshToFade.material.color = fadeColor;
            yield return new WaitForSeconds(.2f);
            meshToFade.material.color = playerColor;
        }

        GameManager.instance.speed = initialSpeed;




    }

    void TakeDamage(int value)
    {
        currentHealth -= value;
        if (currentHealth > 0)
            healthBar.SetHealth(currentHealth);
        else healthBar.SetHealth(0);
            
        if (currentHealth <= 0)
            animator.SetTrigger("Death");
       
    }

    public void Death()
    {
        //SceneManager.LoadScene(0); //emanuele prima 1
        //GameManager.instance.speed = initialSpeed;

        if (once)
        {
            GameOver.SetActive(true);
            once = false;
        }

    }

    public void InstantDeath()
    {
        TakeDamage(9999);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth / 2;
    }
    //IEnumerator OpenGameOver()
    //{
    //    yield return new WaitForSecondsRealtime()
    //}
}