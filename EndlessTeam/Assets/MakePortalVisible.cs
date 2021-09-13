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
    bool once = false;
    GameObject suggestion;

    public Animator portaleAnim;
    public BoxCollider colliderPortello;

    bool check = true;
   
    public void AperturaPortelli()
    {
        if (check)
        {
            portaleAnim.SetTrigger("apri");
            colliderPortello.enabled = false;
        }

    }

    IEnumerator  AperturaCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        AperturaPortelli();
    }

    void ChiusuraPortelli()
    {
        check = false;

       
        if (!colliderPortello.enabled)
           portaleAnim.SetTrigger("chiudi");

        colliderPortello.enabled = true;
       
       
    }

    private void OnTriggerEnter(Collider other)
    {
        //Creo la scena e la imposto come corrente
        if (other.CompareTag("Player") && check==true)
        {
            GameManager.instance.currentPortal = this;
            once = true;
            if (GameManager.instance.firstPortal)
            {
  
                //da riaggiungere
                //suggestion = GameManager.instance.GetObjFromArray("Hint4", GameManager.instance.suggestions);
                //TutorialManager.instance.ShowHint(number);

                StartCoroutine(WaitCor(4));
                GameManager.instance.firstPortal = false;
            }

            GameManager.instance.portalPos = transform.position;

            GameManager.instance.LoadScene(sceneTarget);
            
            GameManager.instance.currentScene = sceneTarget;

            //AperturaPortelli();

            StartCoroutine(AperturaCoroutine());

        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (SceneManager.GetSceneByBuildIndex(sceneTarget).isLoaded && once && GameManager.portal == -1)
        {
            GameManager.portal = gameObject.GetInstanceID();
            once = false;

            ObjectPooling.instance.ChangeMatFromTo(sceneTarget);

        }
    }


    private void OnDisable()
    {
        check = true;

        if (gameObject.GetInstanceID() == GameManager.portal) GameManager.portal = -1;

    }


    //Quando esco dal trigger, se lo faccio, disattivo la scena corrente
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            check = false;
           // ChiusuraPortelli();
           
            StartCoroutine(delay()); ///
            // GameManager.instance.DeactivateScene(sceneTarget);

            GameManager.portal = -1;
        }
    }

    IEnumerator WaitCor(int seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        if (suggestion.activeSelf) TutorialManager.instance.DisableHint();
    }

    IEnumerator delay() 
    {

        ChiusuraPortelli();
        yield return new WaitForSecondsRealtime(.15f);
        GameManager.instance.DeactivateScene(sceneTarget);
        
        yield return null;
      
    }

    public void DisablePortal()
    {
        ChiusuraPortelli();
  
        if (gameObject.GetInstanceID() == GameManager.portal) GameManager.portal = -1;

        check = true;
        if (SceneManager.GetSceneByBuildIndex(sceneTarget).isLoaded)
            GameManager.instance.DeactivateScene(sceneTarget);
    }
   

}