using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBird : MonoBehaviour
{

    public float verticalSpeed;
    public float amplitude;
    public Transform[] trackPos;
    Vector3 tmpPos;

    Vector3 startPos;
    Vector3 targetPos;//

    Animator anim;

    public float speed = 35;

    private void Awake()
    {
        tmpPos = transform.localPosition;
        startPos = transform.localPosition;
        anim = GetComponent<Animator>();


    }


    // Start is called before the first frame update
    void Start()
    {
        tmpPos = transform.localPosition;
        startPos = transform.localPosition;

    }

    private void OnEnable()
    {
        transform.localPosition = startPos;
        StartCoroutine(FlyPingPong());
    }

    IEnumerator FlyPingPong()
    {
        float waitTime=20f;
        float elapsedTime = 0f;
        float pingpong;

        targetPos = trackPos[1].transform.localPosition;
        startPos = trackPos[0].transform.localPosition;

        while (isActiveAndEnabled)
        {
            anim.Play("WalkFWD");

            elapsedTime += Time.deltaTime;

            pingpong = Mathf.PingPong((elapsedTime / waitTime) * speed, targetPos.x - startPos.x) + startPos.x; //range tra - 6 e +6: max - min + min

            tmpPos.x = pingpong;

            tmpPos.y = Mathf.Sin((Time.time * verticalSpeed) * amplitude) + 2;

            transform.localPosition = new Vector3(pingpong , tmpPos.y, transform.localPosition.z);


            yield return null;
        }

            yield return null;

    }

}
