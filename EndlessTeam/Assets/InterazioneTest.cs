using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class InterazioneTest : MonoBehaviour, IPointerDownHandler
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
            Destroy(other.gameObject);
    }

    void Start()
    {
        startPos = this.transform.localPosition;
        endPos = new Vector3(startPos.x, 0.5f, startPos.z);
        addPhysicsRaycaster();
    }

    Vector3 startPos;
    Vector3 endPos;
    bool trig = false;

    void addPhysicsRaycaster()
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
        //Destroy(this.gameObject);

        //trig = true;

        CallCoroutineInteraction();
    }

    void CallCoroutineInteraction()
    {
        StartCoroutine(Interaction());

    }

    float speed = 3;

    IEnumerator Interaction()
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
