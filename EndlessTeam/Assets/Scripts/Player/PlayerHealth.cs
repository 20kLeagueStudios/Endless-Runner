using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    MeshRenderer playerMesh;
    [SerializeField]
    PlayerMovement playerMovement;

    [SerializeField]
    Game gameScript;

    float initialSpeed;

    Color playerColor;
    void Start()
    {
        gameScript = GameObject.FindObjectOfType<Game>();
        initialSpeed = gameScript.SpeedIncrease;
        playerColor = playerMesh.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            StartCoroutine("HitCor", playerMesh);
        }
    }

    IEnumerator HitCor(MeshRenderer meshToFade)
    {
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


    }
}
