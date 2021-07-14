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
        while (finalPos.y != upPos.position.y)
        {

            finalPos.y = Mathf.Lerp(finalPos.y, upPos.position.y, speed);
            //if (Vector3.Dot(finalPos, upPos.position))
            //    finalPos.y = upPos.position.y;
            if (Mathf.Abs(finalPos.y - upPos.position.y) < .001f)
                finalPos.y = upPos.position.y;

            transform.position = finalPos;
       
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        while (finalPos.y != 0)
        {
  
            finalPos.y = Mathf.Lerp(finalPos.y, 0, speed*2);
            if (finalPos.y < .01f)
                finalPos.y = 0;
            transform.position = finalPos;
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine("StompCor");
    }
}
