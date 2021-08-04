using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TrappolaSparaSpine : InterazioneTrappole
{
    public Transform[] spine;
    public Transform[] pos;

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);

        //rend.material.SetFloat("_Emission", 10f);
        //rend.material.SetColor("_EmissionColor", Color.red);

        CallCoroutineInteraction("SparaSpine");
    }

    public IEnumerator SparaSpine()
    {
        float elapsedTime = 0f;
        float waitTime = 25;

        while (elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;

            spine[0].transform.position= Vector3.Lerp(spine[0].transform.position, pos[0].transform.position, elapsedTime / waitTime);

            spine[1].transform.position = Vector3.Lerp(spine[1].transform.position, pos[1].transform.position, elapsedTime / waitTime);

            yield return null;

        }

        yield return null;

    }

 
        void Start()
    {
        addPhysicsRaycaster();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
