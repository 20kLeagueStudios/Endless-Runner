using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    float speed = default;

    [SerializeField]
    LayerMask targetMask;

    float timer;

    private void Start()
    {
        timer = Time.time;
        
    }
    void Update()
    {
        transform.position += transform.forward * -speed * Time.deltaTime;

        if (Time.time - timer > 8f)
        {
            Destroy(gameObject);
        }

        //if (Physics.CheckSphere(transform.position, .1f, targetMask))
        //{
        //    Destroy(gameObject);
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, .5f);
        Gizmos.color = Color.red;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
   
}
