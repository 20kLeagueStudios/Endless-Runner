//Si occupa di cambiare la lingua del testo a cui questo script è attaccato in base al valore nel LanguageManager
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextLanguageChange : MonoBehaviour
{
    //riferimento al manager delle lingue di gioco
    [SerializeField]
    private LanguageManager lm = default;
    //riferimento al testo da cambiare
    [SerializeField]
    private TextMeshProUGUI textToChange = default;
    //stringhe che indicano il testo da mostrare in base alla lingua scelta
    [SerializeField]
    private string italianText = default, //testo che deve essere scritto nel testo quando la lingua è italiana
                   englishText = default; //testo che deve essere scritto nel testo quando la lingua è inglese


    private void Start()
    {
        //si aggiunge alla lista dei testi da cambiare al cambio di lingua
        lm.textsToChangeLanguage.Add(this);
        //se non si è messo il testo da cambiare come riferimento nell'editor, prende il componente Text dentro il gameObject
        if (textToChange == null) { textToChange = GetComponent<TextMeshProUGUI>(); Debug.Log(gameObject + " cambia testo di " + textToChange); }
        //infine cambia il testo in base alla lingua corrente
        ChangeLanguage(lm.GetCurrentLanguage());

    }
    /// <summary>
    /// Cambia la lingua del testo in base al valore ricevuto come parametro
    /// 0 = italiano
    /// 1 = inglese
    /// </summary>
    /// <param name="currentLanguage"></param>
    public void ChangeLanguage(int currentLanguage)
    {
        //In base al valore ricevuto, cambia la lingua del testo
        switch (currentLanguage)
        {
            //LINGUA ITALIANA
            case 0:
                {
                    //cambia il testo in italiano
                    textToChange.text = englishText;
                    break;

                }
            //LINGUA INGLESE
            case 1:
                {
                    //cambia il testo in inglese
                    textToChange.text = italianText;
                    break;

                }
            //Nel caso viene dato un valore errato, viene segnalato nella console come errore
            default: Debug.LogError("Aggiunto valore di lingua sbagliato: " + currentLanguage); break;

        }

    }
    /*
    /// <summary>
    /// Permette ad altri script, senza riferimento alla lingua corrente, di cambiare la lingua del testo in base alla lingua corrente
    /// </summary>
    public void ChangeLanguage()
    {
        //In base al valore ricevuto, cambia la lingua del testo
        switch (lm.GetCurrentLanguage())
        {
            //LINGUA ITALIANA
            case 0:
                {
                    //cambia il testo in italiano
                    textToChange.text = italianText;
                    break;

                }
            //LINGUA INGLESE
            case 1:
                {
                    //cambia il testo in inglese
                    textToChange.text = englishText;
                    break;

                }
            //Nel caso viene dato un valore errato, viene segnalato nella console come errore
            default: Debug.LogError("Aggiunto valore di lingua sbagliato: " + lm.GetCurrentLanguage()); break;

        }

    }
    */
    /// <summary>
    /// Permette ad altri script di cambiare il testo da scrivere, prendendo come parametro una stringa per ogni lingua
    /// </summary>
    /// <param name="italianText"></param>
    /// <param name="englishText"></param>
    public void UpdateText(string englishText, string italianText)
    {
        //aggiorna il testo italiano al parametro del testo italiano ricevuto
        this.italianText = italianText;
        //aggiorna il testo inglese al parametro del testo inglese ricevuto
        this.englishText = englishText;
        //infine, cambia il testo da mostrare in base alla lingua corrente
        ChangeLanguage(lm.GetCurrentLanguage());

    }

    //Ogni volta che viene attivato il gameObject con questo script, viene cambiato il testo in base alla lingua corrente
    //private void OnEnable() { ChangeLanguage(lm.GetCurrentLanguage()); }

}