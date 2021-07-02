using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInvicinble : MonoBehaviour
{
    [SerializeField] Game gameScript;

    [SerializeField] Color playerColor;

    float initialSpeed;

    [SerializeField]MeshRenderer playerMesh;

    [SerializeField] float timePowerUp=10f;

    [SerializeField] ObjectPooling objectPooling;

    [SerializeField] PlayerHealth playerHealth;



    public bool isInvincible=false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && isInvincible == true)
        {
            other.gameObject.SetActive(false);
        }
    }


    IEnumerator HitCor(MeshRenderer meshToFade)
    {
        playerHealth.canBeHit = false;

        objectPooling.speed = objectPooling.maxSpeed;

        Color fadeColor = meshToFade.material.color;

        fadeColor.a = .1f;
        for (int i = 0; i < timePowerUp; i++)
        {
            yield return new WaitForSeconds(.2f);
            meshToFade.material.color = fadeColor;
            yield return new WaitForSeconds(.2f);
            meshToFade.material.color = playerColor;
        }


        objectPooling.speed = initialSpeed;
        playerHealth.canBeHit = true;
        isInvincible = false;

    }


    void Start()
    {
        initialSpeed = objectPooling.speed;

        playerColor = playerMesh.GetComponent<MeshRenderer>().material.color;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            isInvincible = true;
            StartCoroutine(HitCor(playerMesh));

        }


    }
}
