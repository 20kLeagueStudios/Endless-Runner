using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class InterazioneTrappole : MonoBehaviour, IPointerClickHandler
{



    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Obstacle"))
    //        Destroy(other.gameObject);
    //}


    void Start()
    {
        addPhysicsRaycaster();
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

    }


    public void CallCoroutineInteraction(string nameCoroutine)
    {
        StartCoroutine(nameCoroutine);

    }


}