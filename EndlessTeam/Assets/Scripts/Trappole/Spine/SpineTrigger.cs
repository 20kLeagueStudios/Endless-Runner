using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            other.GetComponent<EnemySmoothHor>().Death();
        }
    }
}
