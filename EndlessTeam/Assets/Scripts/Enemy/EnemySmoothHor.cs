using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmoothHor : MonoBehaviour
{
    public Transform[] trackPos;

    Vector3 targetPos;

    Vector3 startPos;

    Animator anim;

    public float speed ;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        targetPos = trackPos[1].transform.localPosition;
        startPos = trackPos[0].transform.localPosition;

    }

    private void Start()
    {
        targetPos = trackPos[1].transform.localPosition;
        startPos = trackPos[0].transform.localPosition;
    }


    void OnEnable()
    {
        //elapsedTime = 0; ----
        //pingpong = 0f;
        startPos = trackPos[0].transform.localPosition;

        transform.localPosition = startPos;
        StartCoroutine(ChangePos());

       
    }

    [SerializeField] bool deveAncheRuotare;

    IEnumerator ChangePos()
    {
        float pingpong;
        float elapsedTime = 0;
        float waitTime = 20;

        targetPos = trackPos[1].transform.localPosition;
        startPos = trackPos[0].transform.localPosition;

        while (isActiveAndEnabled)
        {

            elapsedTime += Time.deltaTime;

            pingpong = Mathf.PingPong((elapsedTime/waitTime)*speed, 1);

            transform.localPosition = Vector3.Lerp(startPos, targetPos, pingpong);

            anim.Play("GranchioFermo_Idle");

            if (deveAncheRuotare)
            {
                if (transform.localPosition.x >= targetPos.x - 0.2f)
                {
                    transform.localRotation = Quaternion.Euler(0, -90, 0);
                }
                if (transform.localPosition.x <= startPos.x + 0.2f)
                {
                    transform.localRotation = Quaternion.Euler(0, 90, 0);
                }
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
