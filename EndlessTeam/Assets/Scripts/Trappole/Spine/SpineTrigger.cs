using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageComp = other.transform.GetComponent<IDamageable>();
        if (damageComp != null)
            damageComp.Damage();
    }
}
