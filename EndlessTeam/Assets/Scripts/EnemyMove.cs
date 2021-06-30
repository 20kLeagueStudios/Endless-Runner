using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform[] trackPos;

    Vector3 targetPos;

    Vector3 startPos;

    int rndPos;

    Animator anim;

    


    private void Awake()
    {
        anim = GetComponent<Animator>();
        startPos = this.transform.localPosition;

    }

    private void Start()
    {

        transform.localPosition = startPos;
        rndPos = Random.Range(0, trackPos.Length);

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
        float waitTime = 2f;

        Vector3 tmpPos = targetPos;

        rndPos = Random.Range(0, trackPos.Length);

        /*
        if (rndPos == 0)
        {
            targetPos = trackPos[rndPos + 1].transform.localPosition;

        }

        else if(rndPos >= trackPos.Length && rndPos != 0)
        {
            targetPos = trackPos[rndPos - 1].transform.localPosition;
        }

        */


        targetPos = trackPos[rndPos].transform.localPosition;

        while (elapsedtime < waitTime)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, elapsedtime/waitTime);

            if (targetPos.x > tmpPos.x)
            {
                anim.Play("WalkRight");
            }
            else { anim.Play("WalkLeft"); }

            elapsedtime += Time.deltaTime;

            yield return null;

        }

        transform.localPosition = targetPos;


       // yield return new WaitForSeconds(.3f);

        StartCoroutine(ChangePos());

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
