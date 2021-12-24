using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTowardsPlayer : MonoBehaviour
{

    float startTime, waitValue = 0.5f;
    [SerializeField]
    GameObject bullet = default;

    private void Start()
    {
        startTime = Time.time;
    }
    void Update()
    {

        
        if (Time.time - startTime > waitValue)
        {
            Shoot();
            waitValue = 3f;
            startTime = Time.time;
        }

    }

    void Shoot()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
        Instantiate(bullet, pos, Quaternion.identity);
    }
}
