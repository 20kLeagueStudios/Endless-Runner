using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MakePortalVisible : MonoBehaviour
{
    //Scena da creare quando sono vicino al portale
    public int sceneTarget;

    int number = 3;
    [SerializeField]
    bool once = true;
    GameObject suggestion;

    private void OnTriggerEnter(Collider other)
    {
        //Creo la scena e la imposto come corrente
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance.firstPortal)
            {
                //da riaggiungere
                //suggestion = GameManager.instance.GetObjFromArray("Hint4", GameManager.instance.suggestions);
                //TutorialManager.instance.ShowHint(number);
                StartCoroutine(WaitCor(4));
                GameManager.instance.firstPortal = false;
            }
            GameManager.instance.LoadScene(sceneTarget);

            GameManager.instance.currentScene = sceneTarget;

        }
    }


    //Quando esco dal trigger, se lo faccio, disattivo la scena corrente
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.DeactivateScene(sceneTarget);
        }
    }

    IEnumerator WaitCor(int seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        if (suggestion.activeSelf) TutorialManager.instance.DisableHint();
    }

    private void Update()
    {
        //Imposta il bioma in modalità curved tested per non mostrare la sovrapposizione
        if (SceneManager.GetSceneByBuildIndex(sceneTarget).isLoaded && once)
        {
            ObjectPooling.instance.ChangeMatFromTo(this.sceneTarget);
            once = false;
        }
    }

}
