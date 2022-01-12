using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MakePortalVisible : MonoBehaviour
{
    //Scena da creare quando sono vicino al portale
    public int sceneTarget = - 1;

    bool once = false;
    GameObject suggestion = default;

    public Animator portaleAnim;
    public BoxCollider colliderPortello;

    bool check = true;

    //Numero che indice l'indice della scena da escludere, quindi se stessa
    public static short currentInd = 2, previousInd = 0;

    //Lista che contiene i numeri disponibili per il portale
    public static List<short> indSceneList = new List<short>();

    private void Awake()
    {
        //Riempo la lista degli index delle scene ad eccezione di 2 che è la scena corrente(bioma funghi)
        for(short i =3; i<=6; i++)
        {
            if (!indSceneList.Contains(i)) indSceneList.Add(i);
        }
    }

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
            //Tolgo il numero corrente dalla lista se esiste
            if (indSceneList.Contains(currentInd)) indSceneList.Remove(currentInd);

            //Setto l'indice corrente della scena con il contenuto ottenuto da un indice randomico preso dalla lista
            sceneTarget = indSceneList[UnityEngine.Random.Range(0, indSceneList.Count - 1)];

            //Salvo il bioma precedente per disattivarlo dopo
            previousInd = currentInd;

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

            //Vieni caricata la scena
            GameManager.instance.LoadScene(sceneTarget);
            
            GameManager.instance.currentScene = sceneTarget;

            //AperturaPortelli();

            StartCoroutine(AperturaCoroutine());

            //Re inserisco la scena che era stata esclusa in precedenza nella lista delle scene
            if (!indSceneList.Contains(currentInd)) indSceneList.Add(currentInd);

            //Aggiorno l'indice della scena corrente
            currentInd = (short)sceneTarget;

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
            GameManager.instance.StartCoroutine(GameManager.instance.DeactivateScene(sceneTarget));

            GameManager.portal = -1;
        }
    }

    IEnumerator WaitCor(int seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        if (suggestion)
        {
            if (suggestion.activeSelf) TutorialManager.instance.DisableHint();
        }
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
            GameManager.instance.StartCoroutine(GameManager.instance.DeactivateScene(sceneTarget));
    }
   

}