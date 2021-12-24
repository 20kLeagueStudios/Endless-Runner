﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInvicinble : MonoBehaviour
{
    [SerializeField] Game gameScript;

    [SerializeField] Color playerColor;

    float initialSpeed;

    [SerializeField]MeshRenderer playerMesh = default;

    [SerializeField] float timePowerUp=10f;

    [SerializeField] ObjectPooling objectPooling = default;

    [SerializeField] PlayerHealth playerHealth = default;

    //Collider[] hitObjects;

    //public float range;

    //public LayerMask layermask;

    public bool isInvincible=false;

    /*
    void DestroyAllObstacles(Vector3 origin)
    {
        hitObjects = Physics.OverlapSphere(origin, range,layermask);

        foreach (Collider hitobj  in hitObjects)
        {
            hitobj.gameObject.SetActive(false);
        }

    }

  

    private void OnCollisionEnter(Collision collision)
    {
        if(playerHealth.canBeHit == false && isInvincible == true)
        {
            DestroyAllObstacles(collision.GetContact(0).point);
        }

        else
        {
           // StopCollider(collision.GetContact(0).thisCollider.isTrigger=true);

            //sthis.gameObject.GetComponent<CharacterController>().

        }

    }

    */

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

        player = this.gameObject;

        startScale = this.gameObject.transform.localScale;

    }

    GameObject player;
    public Vector3 powerupScale;
    Vector3 startScale;
  

    // Update is called once per frame
    //void Update()
    //{
        

    //    /*
    //    if (Input.GetKeyDown(KeyCode.Z))
    //    {

    //        StartCoroutine(PowerUpMini());

    //    }
    //    */

    //}

    public void CallCoroutineMini()
    {
        StartCoroutine(PowerUpMini());
    }

    IEnumerator PowerUpMini()
    {
        
        float elapsedTime=0f;
        float waitTime = 2f;

        while(elapsedTime<waitTime)
        {
            //this.GetComponent<PlayerMovement>().enabled = false;

            elapsedTime += Time.deltaTime;

            player.transform.localScale = Vector3.Lerp(startScale, powerupScale,elapsedTime/waitTime*4);

            yield return null;

        }

       // this.GetComponent<PlayerMovement>().enabled = true;

        player.transform.localScale = powerupScale;

        yield return new WaitForSeconds(4f);

        StartCoroutine(ResetScale());

        yield return new WaitForSeconds(0.5f);

        yield return null;


    }

    IEnumerator ResetScale()
    {
        float elapsedTime = 0f;
        float waitTime = 2f;

        while (elapsedTime<waitTime)
        {

           // this.GetComponent<PlayerMovement>().enabled = false;

            elapsedTime += Time.deltaTime;

            player.transform.localScale = Vector3.Lerp(powerupScale, startScale, elapsedTime / waitTime*4);

            yield return null;

        }

        //this.GetComponent<PlayerMovement>().enabled = true;

        player.transform.localScale = startScale;

        yield return new WaitForSeconds(0.5f);

        yield return null;

    }


}
