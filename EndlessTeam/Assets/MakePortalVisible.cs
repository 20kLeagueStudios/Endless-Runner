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
            GameManager.instance.currentScene = sceneTarget;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.DeactivateScene(sceneTarget);
        }
    }
}
