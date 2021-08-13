using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IDamageable temp = other.GetComponent<IDamageable>();
        if (temp != null) temp.Damage();
    }
}
