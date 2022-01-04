using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Energy : MonoBehaviour
{

    [SerializeField] Text energyText = default;
    [SerializeField] Text timerText = default;
    [SerializeField] Slider energyBar = default;
    private int maxEnergy = 10;
    private int currentEnergy ;
    private int restoreDuration = 500;
    private DateTime nextEnergyTime;
    private DateTime lastEnergyTime;
    private bool isRestoring = false;
    public static bool winEnergy = false;
    public static int energyFortuneWheelWin;

    // Start is called before the first frame update
    void Start()
    { //sistema salvataggio energia. Se nel player prefs non c'è currentEnergy, allora setta l'energia a 30,carica e ripristina energy
        if(!PlayerPrefs.HasKey("currentEnergy")) //oppure carica e ripristina l'energia
        {
            PlayerPrefs.SetInt("currentEnergy", 10);
            Load();
            StartCoroutine(RestoreEnergy());
        }
        else
        {
            Load();
            StartCoroutine(RestoreEnergy());
        }
    }

    public void Update()
    {
        if (winEnergy == true)
        {
            currentEnergy += energyFortuneWheelWin;
            UpdateEnergyTimer();
            UpdateEnergy();
            Save();
            winEnergy = false;
        }

        if (currentEnergy<maxEnergy) //se l'energia è minore dell'energia massima richiamo la coorutine RestoreEnergy
        {
            StartCoroutine(RestoreEnergy());
        }
    }

    public void RestoreEnergyButton() //TEST premendo questo bottone aumento l'energia.
    {
        currentEnergy++;
        UpdateEnergyTimer();
        UpdateEnergy();
        Save();
    }
    public void UseEnergy() //uso l'energia
    {
        if(currentEnergy >= 1) //Se l'energia è minore uguale a 1
        {
            currentEnergy--; //tolgo 1 a currentEnergy
            UpdateEnergy(); //aggiorno energia

            if(isRestoring == false) //Se isRestoring è false
            {
                if(currentEnergy + 1 == maxEnergy) //se l'energia + 1 è uguale a maxEnergy
                {
                    nextEnergyTime = AddDuration(DateTime.Now, restoreDuration); 
                }

                StartCoroutine(RestoreEnergy()); //avvio RestoreEnergy
            }
        }
        //else
        //{
        //    Debug.Log("Energia finita!!"); //Sennò l'energia è finita
        //}
    }

    [SerializeField] InventoryManager inventory = default; //emanuele
    public void Play()
    {
        if(currentEnergy>=1)
        {
            SaveSystem.Saving(inventory); //emanuele
            SceneManager.LoadScene(1);
            Debug.Log("CALAMARO");
          
        }
       
    }

    private IEnumerator RestoreEnergy()
    {
        UpdateEnergyTimer();
        isRestoring = true;

        while(currentEnergy < maxEnergy)
        {
            DateTime currentDateTime = DateTime.Now;
            DateTime nextDateTime = nextEnergyTime;
            bool isEnergyAdding = false;

            while(currentDateTime > nextDateTime)
            {
                if(currentEnergy < maxEnergy)
                {
                    isEnergyAdding = true;
                    currentEnergy++;
                    UpdateEnergy();
                    DateTime timeToAdd = lastEnergyTime > nextDateTime ? lastEnergyTime : nextDateTime;
                    nextDateTime = AddDuration(timeToAdd, restoreDuration);
                }
                else
                {
                    break;
                }

            }

            if (isEnergyAdding == true)
            {
                lastEnergyTime = DateTime.Now;
                nextEnergyTime = nextDateTime;
            }

            UpdateEnergyTimer();
            UpdateEnergy();
            Save();
            yield return null;
        }

        
    }

    private DateTime AddDuration(DateTime datetime, int duration)
    {
        return datetime.AddSeconds(duration);
    }

    private void UpdateEnergyTimer()
    {
        if(currentEnergy >= maxEnergy)
        {
            timerText.text = "Full";
            return;
        }

        TimeSpan time = nextEnergyTime - DateTime.Now;
        string timeValue = String.Format("{0:D2}:{1:D1}", time.Minutes , time.Seconds);
        timerText.text = timeValue;
    }

    private void UpdateEnergy()
    {
        energyText.text = currentEnergy.ToString() + "/" + maxEnergy.ToString();

        energyBar.maxValue = maxEnergy;
        energyBar.value = currentEnergy;
    }

    private DateTime StringToDate(string datetime)
    {
        if(String.IsNullOrEmpty(datetime))
        {
            return DateTime.Now;
        }
        else
        {
            return DateTime.Parse(datetime);
        }
    }

    private void Load()
    {
        currentEnergy = PlayerPrefs.GetInt("currentEnergy");
        nextEnergyTime = StringToDate(PlayerPrefs.GetString("nextEnergyTime"));
        lastEnergyTime = StringToDate(PlayerPrefs.GetString("lastEnergyTime"));
    }

    private void Save()
    {
        PlayerPrefs.SetInt("currentEnergy", currentEnergy);
        PlayerPrefs.SetString("nextEnergyTime", nextEnergyTime.ToString());
        PlayerPrefs.SetString("lastEnergyTime", lastEnergyTime.ToString());
    }

   
}
