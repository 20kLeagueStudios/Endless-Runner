using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public  class InterazioneTrappole : MonoBehaviour, IPointerClickHandler
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
        

   public void addPhysicsRaycaster() //safe check
    {
        PhysicsRaycaster physicsRaycaster = GameObject.FindObjectOfType<PhysicsRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        }
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);

        //rend.material.SetFloat("_Emission", 10f);
        //rend.material.SetColor("_EmissionColor", Color.red);

        CallCoroutineInteraction("InteractionFallDown");
    }
     

    public void CallCoroutineInteraction(string nameCoroutine)
    {
        StartCoroutine(nameCoroutine);

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
