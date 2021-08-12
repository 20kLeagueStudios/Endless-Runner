using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegaCircolare : MonoBehaviour
{
    public GameObject sega;
    public Transform pos1;
    public Transform pos2;

    public bool ruotaSulPosto=false;
  
    IEnumerator RotazioneLamaCo()
    {
        float elapsedTime=0f;
        float waitTime=3;
 
        while (elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;
 
            sega.transform.position = Vector3.Lerp(pos1.transform.position, pos2.transform.position, elapsedTime/waitTime);
            sega.transform.Rotate(Vector3.forward, 10);

            yield return null;

        }
        sega.transform.position = pos2.transform.position;
        StartCoroutine(RotazioneLamaCoRev());
 
    }

    IEnumerator RotazioneLamaCoRev()
    {
        float elapsedTime = 0f;
        float waitTime = 3;
 
        while (elapsedTime < waitTime)
        {
            
 
            sega.transform.position = Vector3.Lerp(pos2.transform.position, pos1.transform.position, elapsedTime / waitTime );
            sega.transform.Rotate(-Vector3.forward, 10);
            elapsedTime += Time.deltaTime;
            yield return null;

        }
        sega.transform.position = pos1.transform.position;
        StartCoroutine(RotazioneLamaCo());

     }

    IEnumerator RotazioneLama()
    {
        float elapsedTime = 0f;
        float waitTime = 3;

        while (elapsedTime < waitTime)
        {
            sega.transform.Rotate(-Vector3.forward, 10);
            elapsedTime += Time.deltaTime;
            yield return null;

        }
        yield return null;

    }

    private void OnEnable()
    {   
        if(ruotaSulPosto==false)
            StartCoroutine(RotazioneLamaCo());
        else
        {
            StartCoroutine(RotazioneLama());
        }
    }

    private void Update()
    {
      
    }
}
