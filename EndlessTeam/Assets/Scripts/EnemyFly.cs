using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour
{

    Vector3 startPos; //cache della posizione iniziale

    [SerializeField] float speed;

    [SerializeField]float waitTime=20f; //waittime e elapsedtime vengono usate dentro una coroutine

    float elapsedTime;

    [SerializeField] float offset = 0.5f;


    void Start()
    {
        startPos = transform.localPosition; //pos di partenza
    }

    private void OnEnable() //quando l 'oggetto e questo script vengono attivati
    {
        transform.localPosition = startPos; //ci assicuriamo di far partire l'oggetto dal suo punto di partenza all'interno del prefab

        StartCoroutine(EnemiesFly()); //facciamo partire la coroutine
    }

    IEnumerator EnemiesFly()
    {
        yield return new WaitForSeconds(1.5f);

        waitTime = 20f;
        elapsedTime = 0f;

        while (elapsedTime < waitTime) //fino a quando elapsedtime è minore di waittime..
        {
            elapsedTime += Time.deltaTime; //...incrementiamo elapsedtime con il passare del tempo

            //la nuova posizione dell'oggetto è un nuovo vettore che ha su x e z la posizione dell oggetto stesso, su y un ping pong tra 0 e 2 

            transform.localPosition = new Vector3(transform.localPosition.x,   Mathf.PingPong((elapsedTime/waitTime)*speed, 2f) + offset, transform.localPosition.z);


            yield return null;

        }


        yield return null;

    }


    IEnumerator EnemiesFlyHoriz()
    {
        yield return new WaitForSeconds(1.5f);

        waitTime = 20f;
        elapsedTime = 0f;

        while (elapsedTime < waitTime) //fino a quando elapsedtime è minore di waittime..
        {
            elapsedTime += Time.deltaTime; //...incrementiamo elapsedtime con il passare del tempo

            //la nuova posizione dell'oggetto è un nuovo vettore che ha su x e z la posizione dell oggetto stesso, su y un ping pong tra 0 e 2 

            transform.localPosition = new Vector3(Mathf.PingPong((elapsedTime / waitTime) * speed, 2f) + offset, transform.localPosition.y, transform.localPosition.z);


            yield return null;

        }


        yield return null;

    }
        // Update is called once per frame
        void Update()
    {
   
    }
}
