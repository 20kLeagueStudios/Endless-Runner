﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour, IDamageable
{
    public Transform[] trackPos;

    Vector3 targetPos;

    Vector3 startPos;

    public int tmpRnd;

    public int rndPos;

    public float waitTime = 2f;

    Animator anim;

    GameManager gameManager;
    AudioManager audioManager;

    private void Awake()
    {
        gameManager = GameManager.instance;
        audioManager = GameManager.instance.audioManager;
        anim = GetComponent<Animator>();
        startPos = this.transform.localPosition;

    }

    private void Start()
    {

        transform.localPosition = startPos;
        rndPos = Random.Range(0, trackPos.Length);

        tmpRnd = (int)transform.localPosition.x;

    }

    void OnEnable()
    {

        // transform.localPosition = trackPos[rndPos].transform.localPosition;
        transform.localPosition = startPos;
        StartCoroutine(ChangePos());

    }


    IEnumerator ChangePos()
    {
        float elapsedtime = 0;
        Vector3 tmpPos = targetPos;

        tmpRnd = rndPos;

        rndPos = Random.Range(0, trackPos.Length);

        /*
        if (tmpRnd > 1 || tmpRnd==0)
        {
            // rndPos = 1;
            rndPos = 0;
        }
        */

        if (tmpRnd ==2  )
        {

            rndPos = 1;
        }

        if (tmpRnd == 0)
        {

            rndPos = 1;
        }


        if (rndPos==tmpRnd)
        {
            if (tmpRnd == 2)
                //rndPos = tmpRnd - 1;
                rndPos = 1;
         
            if (tmpRnd == 0)
                //rndPos = tmpRnd + 1;
                rndPos = Random.Range(1, 2);

            if (tmpRnd == 1)
                  rndPos = Random.Range(0, 2) == 0 ? 0 : 2;
                // rndPos = 0;
 
        }


        targetPos = trackPos[rndPos].transform.localPosition;

        while (elapsedtime < waitTime)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, elapsedtime/waitTime);

            //if (targetPos.x > tmpPos.x)
            //{
            //    anim.Play("WalkRight");
            //}
            //else { anim.Play("WalkLeft"); }

            elapsedtime += Time.deltaTime;

            yield return null;

        }

        transform.localPosition = targetPos;

        StartCoroutine(ChangePos());

    }

    
    public void Death()
    {
        audioManager.PlaySound("MorteNemico");
        // dead = true;
        anim.SetTrigger("Death");
        //transform.gameObject.SetActive(false);
        StopAllCoroutines();
        //transform.gameObject.SetActive(false);
    }

    public void Disable()
    {
        transform.gameObject.SetActive(false);
        gameManager.toReactive.Add(this.gameObject);
    }

    public void Damage()
    {
        Death();
        gameManager.IncreaseScore(200);
    }


}
