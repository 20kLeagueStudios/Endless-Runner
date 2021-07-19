using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{

    //Lista di istanze Suggestions che conterranno in ordine l'HUD dei consigli da mostrare
    [SerializeField]
    List<GameObject> suggestionList = new List<GameObject>();

    GameObject currentSuggestion = default;

    //Index dei suggestion
    int i = 0;
    public void ShowHint()
    {
        Time.timeScale = 0;

        currentSuggestion = suggestionList[i];

        suggestionList[i].SetActive(true);
        //Image[] tempImg = suggestionList[i].gameObject.GetComponentsInChildren<Image>();
        //TextMeshProUGUI[] tempTxt = suggestionList[i].gameObject.GetComponentsInChildren<TextMeshProUGUI>();

        ////Attivo ogni immagine
        //for(int i=0; i<tempImg.Length; i++)
        //{
        //    tempImg[i].gameObject.SetActive(true);
        //}
        
        //for (int i=0; i<tempTxt.Length; i++)
        //{
        //    tempTxt[i].gameObject.SetActive(true);
        //}
        if (i <suggestionList.Count -1)
            i++;
    }

    public void DisableHint()
    {
        Time.timeScale = 1;

        currentSuggestion.SetActive(false);

        //Image[] tempImg = currentSuggestion.gameObject.GetComponentsInChildren<Image>();
        //TextMeshProUGUI[] tempTxt = currentSuggestion.gameObject.GetComponentsInChildren<TextMeshProUGUI>();

        //Attivo ogni immagine
        //for (int i = 0; i < tempImg.Length; i++)
        //{
        //    tempImg[i].gameObject.SetActive(false);
        //}

        //for (int i = 0; i < tempTxt.Length; i++)
        //{
        //    tempTxt[i].gameObject.SetActive(false);
        //}
    }

    private void Update()
    {   
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (currentSuggestion != null && currentSuggestion.activeSelf) DisableHint();
            else ShowHint();
        }
      
    }
}


//[System.Serializable]
//public class Suggestions
//{
//    public Image[] images;
//    public TextMeshProUGUI[] text;
//}