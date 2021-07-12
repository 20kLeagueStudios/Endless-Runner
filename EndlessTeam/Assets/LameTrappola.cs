using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class LameTrappola : MonoBehaviour, IPointerDownHandler
{
    public Transform[] pos;
    public Transform[] lama;

    Transform startPos;
    Transform endPos;

    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        lama[0].position = pos[0].position;
        lama[1].position = pos[1].position;

        startPos = pos[0];
        endPos = pos[1];
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
        Debug.Log("CIP  ");

        rend.material.SetFloat("_Emission", 10f);
        rend.material.SetColor("_EmissionColor", Color.red);

        CallCoroutineInteraction();
    }

    void CallCoroutineInteraction()
    {
        StartCoroutine(MuoviLame());


    }

    float speed;
    IEnumerator MuoviLame()
    {

        float elapseTime = 0f;
        float waitTime = 2f;

        lama[0].gameObject.transform.localPosition = startPos.transform.localPosition;
        lama[1].gameObject.transform.localPosition = endPos.transform.localPosition;


        while (elapseTime < waitTime)
        {
            elapseTime += Time.deltaTime;

            speed += (Time.deltaTime * 3);

            lama[0].transform.localPosition = Vector3.Lerp(startPos.transform.localPosition, endPos.transform.localPosition, (elapseTime / waitTime) * speed);
            lama[1].transform.localPosition = Vector3.Lerp(endPos.transform.localPosition, startPos.transform.localPosition, (elapseTime / waitTime) * speed);


            yield return null;
        }

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform lama in lama)
        {
            lama.Rotate(0, 360 * Time.deltaTime, 0 );
        }
    }
}
