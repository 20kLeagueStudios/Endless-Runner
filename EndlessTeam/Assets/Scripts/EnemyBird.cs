using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBird : MonoBehaviour
{

    public float verticalSpeed;
    public float amplitude;

    Vector3 tmpPos;

    Vector3 startPos;

    private void Awake()
    {
        tmpPos = transform.localPosition;
        startPos = transform.localPosition;

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
        float waitTime=10f;
        float elapsedTime = 0f;

        float pingpong;

        while (elapsedTime<waitTime)
        {
            elapsedTime += Time.deltaTime;

            pingpong = Mathf.PingPong(Time.time * 4, -1-1) + 2 ;

            tmpPos.x = pingpong;

            tmpPos.y = Mathf.Sin((Time.time * verticalSpeed) * amplitude) + 2;

            //transform.localPosition =  Vector3.Lerp(startPos,tmpPos,(elapsedTime/waitTime)*4);

            transform.localPosition = new Vector3(Mathf.PingPong((elapsedTime / waitTime) * 12, 6) - 3 , tmpPos.y, transform.localPosition.z);


            yield return null;
        }

        yield return null;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
