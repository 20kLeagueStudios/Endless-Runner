using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CurveController : MonoBehaviour
{

    public Transform CurveOrigin; //nelnostro caso il punto di origine è la camera

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
    }

    void Update()
    {
        bendAmount.x = x;
        bendAmount.y = y;

        Shader.SetGlobalVector(bendAmountId, bendAmount);
        Shader.SetGlobalVector(bendOriginId, CurveOrigin.position);
        Shader.SetGlobalFloat(bendFalloffId, falloff);
    }

}
