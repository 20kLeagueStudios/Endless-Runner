using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
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
    int maxHealth;
    public int currentHealth;  //prima era privato
    public bool canBeHit=true; //emanuele

    Color playerColor;

    [SerializeField]
    Animator animator;
    void Start()
    {
        gameScript = GameObject.FindObjectOfType<Game>();
        initialSpeed = gameScript.SpeedIncrease;
        playerColor = playerMesh.material.color;
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth / 2;
        healthBar.SetHealth(currentHealth);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && canBeHit==true)
        {
            StartCoroutine("HitCor", playerMesh);
            TakeDamage(1);

            Debug.Log("currenthealth"+currentHealth);

        }
    }

    IEnumerator HitCor(MeshRenderer meshToFade)
    {
        canBeHit = false;
        gameScript.SpeedIncrease = gameScript.SpeedIncrease / 2f;
        Color fadeColor = meshToFade.material.color;

        fadeColor.a = .1f;
        for (int i = 0; i<4; i++)
        {
            yield return new WaitForSeconds(.3f);
            meshToFade.material.color = fadeColor;
            yield return new WaitForSeconds(.2f);
            meshToFade.material.color = playerColor;
        }

        gameScript.SpeedIncrease = initialSpeed;
        canBeHit = true;


    }

    void TakeDamage(int value)
    {
        currentHealth -= 1;
        healthBar.SetHealth(currentHealth);
            
        if (currentHealth <= 0)
            animator.SetTrigger("Death");
       
    }

    public void Death()
    {
        SceneManager.LoadScene(1);
        gameScript.SpeedIncrease = initialSpeed;
    }
}