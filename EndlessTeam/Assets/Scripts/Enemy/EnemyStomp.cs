using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStomp : MonoBehaviour
{
    [SerializeField]
    Transform upPos;
    [SerializeField]
    float speed;

    private void Start()
    {
        StartCoroutine("StompCor");
    }

    IEnumerator StompCor()
    {
        Vector3 finalPos = default;
        //Debug.Log("FinalPos: " + finalPos.y);
        //Debug.Log("UpPos: " + upPos.position.y);
        while (finalPos.y != upPos.localPosition.y)
        {
            Debug.Log(upPos.localPosition.y);
            finalPos.y = Mathf.Lerp(finalPos.y, upPos.localPosition.y, speed);
            //if (Vector3.Dot(finalPos, upPos.position))
            //    finalPos.y = upPos.position.y;
            if (Mathf.Abs(finalPos.y - upPos.localPosition.y) < .001f)
                finalPos.y = upPos.localPosition.y;


            transform.localPosition = finalPos;
       
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        while (finalPos.y != 0)
        {
  
            finalPos.y = Mathf.Lerp(finalPos.y, 0, speed*2);
            if (finalPos.y < .01f)
                finalPos.y = 0;
           
            transform.localPosition = finalPos;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine("StompCor");
    }
}
