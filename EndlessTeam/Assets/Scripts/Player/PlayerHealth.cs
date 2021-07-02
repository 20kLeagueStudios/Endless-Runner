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
    HealthBar healthBar;

    [SerializeField]
    Game gameScript;

    float initialSpeed;

    [SerializeField]
    public int maxHealth;
    public int currentHealth;

    Color playerColor;

    [SerializeField]
    Animator animator;
    
    public bool canBeHit = true; //emanuele
    void Start()
    {
        gameScript = GameObject.FindObjectOfType<Game>();
        initialSpeed = objectPooling.speed;
        playerColor = playerMesh.material.color;
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth / 2;
        healthBar.SetHealth(currentHealth);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && canBeHit == true)
        {
            StartCoroutine("HitCor", playerMesh);
            TakeDamage(1);
        }
    }

    IEnumerator HitCor(MeshRenderer meshToFade)
    {
        canBeHit = false;
        objectPooling.speed = objectPooling.speed / 1.3f;
        Color fadeColor = meshToFade.material.color;

        fadeColor.a = .1f;
        for (int i = 0; i<4; i++)
        {
            yield return new WaitForSeconds(.3f);
            meshToFade.material.color = fadeColor;
            yield return new WaitForSeconds(.2f);
            meshToFade.material.color = playerColor;
        }

        objectPooling.speed = initialSpeed;
        canBeHit = true;


    }

    void TakeDamage(int value)
    {
        Debug.Log("Prova");
        currentHealth -= value;
        if (currentHealth > 0)
            healthBar.SetHealth(currentHealth);
        else healthBar.SetHealth(0);
            
        if (currentHealth <= 0)
            animator.SetTrigger("Death");
       
    }

    public void Death()
    {
        SceneManager.LoadScene(1);
        objectPooling.speed = initialSpeed;
    }

    public void InstantDeath()
    {
        TakeDamage(9999);
    }
}