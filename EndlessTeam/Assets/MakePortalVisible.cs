using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePortalVisible : MonoBehaviour
{
    [SerializeField]
    int sceneTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.LoadScene(sceneTarget);
        }
    }
}
