using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmoothHor : MonoBehaviour
{
    public Transform[] trackPos;

    Vector3 targetPos;

    Vector3 startPos;

    //  int rndPos;

    public bool isTurning = false;

    Animator anim;

    public float pingpong;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        startPos = trackPos[0].transform.localPosition;

    }

    private void Start()
    {

        transform.localPosition = startPos;
       // rndPos = Random.Range(0, trackPos.Length);

    }



    void OnEnable()
    {

        // transform.localPosition = trackPos[rndPos].transform.localPosition;
        transform.localPosition = startPos;
        StartCoroutine(ChangePos());

    }

    IEnumerator ChangePos()
    {
        float elapsedTime = 0;
        float waitTime = 20;

        //transform.localRotation = Quaternion.identity;
        targetPos = trackPos[1].transform.localPosition;

        Vector3 tmpPos = transform.localPosition;

        while (elapsedTime < waitTime)
        {
            pingpong = Mathf.PingPong(Time.time * 0.5f, targetPos.x);

            transform.localPosition = Vector3.Lerp(startPos, targetPos, pingpong);

            anim.Play("WalkFWD");

            if(transform.localPosition.x > 0.9f)
            {
                isTurning = true;
            }
            if (transform.localPosition.x < -0.9f ) 
            { 
                isTurning = false; 
            }

            if(isTurning)
            {
                transform.localRotation = Quaternion.Euler(0, -90, 0);
            }
            else if (isTurning==false)
            { 
             transform.localRotation = Quaternion.Euler(0, 90, 0);
            }

            yield return null;

        }

  

        yield return null;




    }

    // Update is called once per frame
    void Update()
    {

    }
}
