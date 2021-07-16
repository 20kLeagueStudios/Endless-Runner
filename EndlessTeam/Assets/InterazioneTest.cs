using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class InterazioneTest : MonoBehaviour, IPointerDownHandler
{
   
    Vector3 startPos;
    Vector3 endPos;

    float speed = 3;

    Renderer rend;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
            Destroy(other.gameObject);
    }

    void Start()
    {

        rend = GetComponent<Renderer>();
        startPos = this.transform.localPosition;
        endPos = new Vector3(startPos.x, 0.5f, startPos.z);
        addPhysicsRaycaster();
    }

    void Awake()
    {
        startPos = this.transform.localPosition;
    }    

    void OnEnable()
    {
        this.transform.localPosition = startPos;

        rend = GetComponent<Renderer>();
        rend.material.SetFloat("_Emission", 80f);
        rend.material.SetColor("_EmissionColor", Color.green);

    }
        

    void addPhysicsRaycaster() //safe check
    {
        PhysicsRaycaster physicsRaycaster = GameObject.FindObjectOfType<PhysicsRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);

        rend.material.SetFloat("_Emission", 10f);
        rend.material.SetColor("_EmissionColor", Color.red);

        CallCoroutineInteraction();
    }

    void CallCoroutineInteraction()
    {
        StartCoroutine(InteractionFallDown());

    }


    IEnumerator InteractionFallDown()
    {
        float elapseTime = 0f;
        float waitTime = 2f;

        while (elapseTime < waitTime)
        {
            elapseTime += Time.deltaTime;

            speed += (Time.deltaTime*5);

            transform.localPosition = Vector3.Lerp(startPos, endPos, (elapseTime/waitTime)*speed);

            yield return null;
        }

        yield return null;
    }

   
    void Update()
    { 

    }

}
