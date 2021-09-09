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
            if (hint == 4 || hint == 5) StartCoroutine("Timer");
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(3f);
        TutorialManager.instance.DisableHint();
    }
}
