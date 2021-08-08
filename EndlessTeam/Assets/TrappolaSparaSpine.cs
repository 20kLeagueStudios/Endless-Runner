using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TrappolaSparaSpine : InterazioneTrappole
{
    public Transform[] spine;
    public Transform[] endPos;

    public Vector3  startPos1;
    public Vector3 startPos2;



    Renderer rend;

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);

        rend.material.SetFloat("_Emission", 10f);
        rend.material.SetColor("_EmissionColor", Color.red);

        CallCoroutineInteraction("SparaSpine");
    }

    private void OnEnable()
    {
        //rend.material.SetFloat("_Emission", 80f);
        //rend.material.SetColor("_EmissionColor", Color.green);

        startPos1= spine[0].transform.position;
        startPos2 = spine[1].transform.position;

    }



    public IEnumerator SparaSpine()
    {
        float elapsedTime = 0f;
        float waitTime = 25;

        while (elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;

            spine[0].transform.position= Vector3.Lerp(spine[0].transform.position, endPos[0].transform.position, elapsedTime / waitTime);

            spine[1].transform.position = Vector3.Lerp(spine[1].transform.position, endPos[1].transform.position, elapsedTime / waitTime);

            yield return null;

        }

        yield return null;

    }

   void  ResetSpinePos()
    {
        spine[0].transform.position = startPos1;
        spine[1].transform.position = startPos2;
    }

    private void OnDisable()
    {
        ResetSpinePos();
    }


    void Awake()
    {
        rend = GetComponent<Renderer>();

    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
