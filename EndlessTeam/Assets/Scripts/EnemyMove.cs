using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform[] trackPos;

    Vector3 targetPos;

    Vector3 startPos;

    public int tmpRnd;

    public int rndPos;

    public float waitTime = 2f;

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

        if (tmpRnd > 1 || tmpRnd==0)
        {
            rndPos = 1;
        }

        if (rndPos==tmpRnd)
        {
            if(tmpRnd==2)
            rndPos = tmpRnd - 1;
         
            if (tmpRnd == 0)
                rndPos = tmpRnd + 1;

            if (tmpRnd == 1)
                rndPos = Random.Range(0, 2) == 0 ? 0 : 2;
        }
      

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

        StartCoroutine(ChangePos());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
