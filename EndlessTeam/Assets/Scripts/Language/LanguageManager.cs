//si occupa del cambio della lingua nel gioco
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LanguageManager : MonoBehaviour
{
	//VARIABILI PUBBLICHE
	//variabile che tiene conto dell'ultima lingua selezionata
	//
	//0 = ITALIANO
	//1 = INGLESE
	//public static int mostRecentValue;
	//lista di tutti gli script dei testi da far diventare della lingua selezionata(vengono aggiunti alll'Awake di TextLanguageChange)
	[HideInInspector]
	public List<TextLanguageChange> textsToChangeLanguage;


	//VARIABILI PRIVATE
	//riferimento alla lista dropdown che si occupa del cambio di lingua
	[SerializeField]
	private TMP_Dropdown languageDropdownList = default;
    //liste contenenti i testi da mettere alla lista dropdown al cambio lingua
    private List<string> italianDropOptions = new List<string> { "Inglese", "Italiano" },
    englishDropOptions = new List<string> { "English", "Italian" };



    private void Awake()
    {
		//ottiene il riferimento al GameManag

	}

    //all'inizio della scena la lingua viene cambiata in base all'ultima lingua impostata dal giocatore nella scena precedente
    void Start()
	{
		
		//ottiene il riferimento al GameManag
		//g = GetComponent<GameManag>();
		//se esiste il riferimento alla dropdown list per il cambio della lingua...
		if (languageDropdownList != null)
		{
			languageDropdownList.ClearOptions();
            
            //...viene cambiato il suo valore in base al valore salvato
            ListLanguageChange(GameManager.instance.savedLanguage);


        }
		//Debug.Log("Lingua caricata: " + g.savedLanguage);
		//viene cambiata la lingua in base al valore nella lista dropdown
		LanguageChange();
	
	}
	/// <summary>
	/// Cambia la lingua di tutti i testi che fanno parte della lista. Viene richiamata quando viene cambiato il valore della lista dropdown
	/// </summary>
	public void LanguageChange()
	{
		//se esiste il riferimento alla dropdown list per il cambio della lingua...
		if (languageDropdownList != null)
		{

			//...aggiorna il valore del GameManag per la lingua selezionata...
			GameManager.instance.savedLanguage = languageDropdownList.value;
            //...e salva il valore appena aggiornato
            //SaveSystem.DataSave(GameManager);
            //Debug.Log("Lingua salvata: " + g.savedLanguage);
        }
		//cicla ogni oggetto nella lista dei testi da cambiare per cambiargli la lingua
		for (int i = 0; i < textsToChangeLanguage.Count; i++)
        {
			//cambia la lingua in base al valore della lista dropdown
			textsToChangeLanguage[i].ChangeLanguage(GameManager.instance.savedLanguage);

        }
		//chiama la funzione che cambia la lingua dei testi all'interno della lista dropdown
		//ChangeDropdownOptionsLanguage();
		//aggiorna il valore di recentValue al valore della selezione di lingua del giocatore nella lista dropdown
		//mostRecentValue = languageDropdownList.value;

	}

	public void ListLanguageChange(int value)
    {
		if (languageDropdownList)
        {
            //int temp = languageDropdownList.value;
            languageDropdownList.ClearOptions();
			GameManager.instance.savedLanguage = value;
			if (value == 0) languageDropdownList.AddOptions(englishDropOptions);
			else if (value == 1) languageDropdownList.AddOptions(italianDropOptions);
			languageDropdownList.SetValueWithoutNotify(value);
			LanguageChange();



		}
    }
	/// <summary>
	/// Permette ad altri script di sapere la lingua corrente nel gioco
	/// </summary>
	/// <returns></returns>
	public int GetCurrentLanguage() { /*Debug.Log("Ottiene la lingua attuale");*/ return GameManager.instance.savedLanguage; }
	/// <summary>
	/// Si occupa di cambiare la lingua dei testi all'interno della lista dropdown, se esiste
	/// </summary>
	/*public void ChangeDropdownOptionsLanguage()
	{
		//se esiste il riferimento alla dropdown list per il cambio della lingua, ne cambia il testo delle scelte
		if (languageDropdownList != null)
        {
			//se il valore della scelta di lingua è 0, cambia la lingua dei testi in italiano
			if (languageDropdownList.value == 0)
			{

				languageDropdownList.options.Clear();
				languageDropdownList.AddOptions(italianDropOptions);

			}
			//se il valore della scelta di lingua è 1, cambia la lingua dei testi in inglese
			if (languageDropdownList.value == 1)
			{

				languageDropdownList.options.Clear();
				languageDropdownList.AddOptions(englishDropOptions);

			}

		}

	}*/

}
