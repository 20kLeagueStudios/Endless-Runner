using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CurveController : MonoBehaviour
{

    public Transform CurveOrigin; //nelnostro caso il punto di origine è la camera

    [SerializeField]
    float speed;

    [Range(-50f, 50f)]
    [SerializeField]
    float x = 0f;

    [Range(-50f, 50f)]
    [SerializeField]
    float y = 0f;

    [Range(0f, 50f)]
    [SerializeField]
    float falloff = 0f;

    private Vector2 bendAmount = Vector2.zero;

    // Global shader property ids
    private int bendAmountId;
    private int bendOriginId;
    private int bendFalloffId;

    void Start()
    {
        bendAmountId = Shader.PropertyToID("_BendAmount"); //i tre int sono inizializzati al valore dei label presenti nello script dello shader
        bendOriginId = Shader.PropertyToID("_BendOrigin");
        bendFalloffId = Shader.PropertyToID("_BendFalloff");
        x = y = 0;
        StartCoroutine(LerpValue(-2, -4));
    }

    void Update()
    {
        bendAmount.x = x;
        bendAmount.y = y;

        Shader.SetGlobalVector(bendAmountId, bendAmount);
        Shader.SetGlobalVector(bendOriginId, CurveOrigin.position);
        Shader.SetGlobalFloat(bendFalloffId, falloff);
    }

    IEnumerator LerpValue(float maxX, float maxY)
    {
        while(x != maxX && y != maxY)
        {
            x = Mathf.Lerp(x, maxX, speed * Time.deltaTime);
            y = Mathf.Lerp(y, maxY, speed * Time.deltaTime);

            if (Mathf.Abs(x - maxX) < .01f)
                x = maxX;
            if (Mathf.Abs(y - maxY) < .01f)
                y = maxY;

            Debug.Log("Dentro");
            yield return null;
        }

        yield return new WaitForSeconds(1);

        float valueX = (Random.value < 0.5f ? 2f : -2f);
        float valueY = (Random.value < 0.5f ? -4f : -1f);

        StartCoroutine(LerpValue(valueX, valueY));
    }
    

}