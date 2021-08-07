using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallSuggestion : MonoBehaviour
{
    [SerializeField]
    int hint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TutorialManager.instance.ShowHint(hint);
        }
    }
}
