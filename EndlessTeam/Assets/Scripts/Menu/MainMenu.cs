using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject shopButtonPrefab;
    public GameObject shopButtonContainer;

    public InventoryManager inventory;

    public ItemsShopSO[] itemShop;

    public Material playerMaterial;

    public AudioMixer audioMixer;

    public Slider sliderMusica;
    public Slider sliderSFX;

	//VARIABILI PUBBLICHE
	//variabile che tiene conto dell'ultima lingua selezionata
	//
	//0 = ITALIANO
	//1 = INGLESE
	//public static int mostRecentValue;
	//lista di tutti gli script dei testi da far diventare della lingua selezionata(vengono aggiunti alll'Awake di TextLanguageChange)
	[HideInInspector]
	public List<TextLanguageChange> textsToChangeLanguage;

	public int savedLanguage;

	public void Play()
    {
        SceneManager.LoadScene("1");
    }

    private void Awake()
    {

        sliderSFX.value = PlayerPrefs.GetFloat("volumeSFX") ;

        sliderMusica.value = PlayerPrefs.GetFloat("volumeMusica");

		savedLanguage = PlayerPrefs.GetInt("savedLanguage"); /////

        //audioMixer.SetFloat("volueSFX", PlayerPrefs.GetFloat("volueSFX"));

        //audioMixer.SetFloat("MusicaVol", Mathf.Log10(sliderMusica.value) * 20);

    }

    private void Start()
	{//ottiene il riferimento al GameManag
	 //g = GetComponent<GameManag>();
	 //se esiste il riferimento alla dropdown list per il cambio della lingua...
		if (languageDropdownList != null)
		{
			languageDropdownList.ClearOptions();

			//...viene cambiato il suo valore in base al valore salvato
			ListLanguageChange(savedLanguage);


		}
		//Debug.Log("Lingua caricata: " + g.savedLanguage);
		//viene cambiata la lingua in base al valore nella lista dropdown
		LanguageChange();

		audioMixer.SetFloat("MusicaVol", Mathf.Log10(sliderMusica.value)*20);
        audioMixer.SetFloat("volueSFX", Mathf.Log10(sliderSFX.value) * 20);

        itemShop = inventory.itemDisponibili.ToArray();

        foreach (ItemsShopSO item in itemShop)
        {
            GameObject shopButton = Instantiate(shopButtonPrefab) as GameObject;

            shopButton.transform.GetChild(2).GetComponent<Image>().sprite = item.itemSO.itemImage;

            //shopButton.GetComponent<Image>().sprite = item.itemSO.itemImage;

            shopButton.transform.SetParent(shopButtonContainer.transform, false);

            shopButton.transform.GetChild(0).GetComponent<TMP_Text>().text = item.itemSO.itemName;
            shopButton.transform.GetChild(1).GetComponent<TMP_Text>().text = item.itemSO.itemCost.ToString();

        }
    }




	//VARIABILI PRIVATE
	//riferimento alla lista dropdown che si occupa del cambio di lingua
	[SerializeField]
	private TMP_Dropdown languageDropdownList = default;
	//liste contenenti i testi da mettere alla lista dropdown al cambio lingua
	private List<string> italianDropOptions = new List<string> { "Inglese", "Italiano" },
	englishDropOptions = new List<string> { "English", "Italian" };



	
	//all'inizio della scena la lingua viene cambiata in base all'ultima lingua impostata dal giocatore nella scena precedente
	
	/// <summary>
	/// Cambia la lingua di tutti i testi che fanno parte della lista. Viene richiamata quando viene cambiato il valore della lista dropdown
	/// </summary>
	public void LanguageChange()
	{
		//se esiste il riferimento alla dropdown list per il cambio della lingua...
		if (languageDropdownList != null)
		{

			//...aggiorna il valore del GameManag per la lingua selezionata...
			savedLanguage = languageDropdownList.value;
			//...e salva il valore appena aggiornato
			//SaveSystem.DataSave(GameManager);
			//Debug.Log("Lingua salvata: " + g.savedLanguage);
		}
		//cicla ogni oggetto nella lista dei testi da cambiare per cambiargli la lingua
		for (int i = 0; i < textsToChangeLanguage.Count; i++)
		{
			//cambia la lingua in base al valore della lista dropdown
			textsToChangeLanguage[i].ChangeLanguage(savedLanguage);

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
			savedLanguage = value;
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
	public int GetCurrentLanguage() { /*Debug.Log("Ottiene la lingua attuale");*/ return savedLanguage; }
	
}
