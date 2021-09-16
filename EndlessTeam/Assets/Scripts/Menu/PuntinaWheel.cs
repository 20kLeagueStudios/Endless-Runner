using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuntinaWheel : MonoBehaviour
{
	[SerializeField]
	private FortuneWheel _spinner;
    [SerializeField]
	private Text scoretext;
	[SerializeField]
	private GameObject testo;
	private int x = 0;
	// Use this for initialization
	void Start()
	{
		testo.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
        Debug.Log("il Blocco di sicurezza è " + FortuneWheel.blocco);
    }

	void OnTriggerStay2D(Collider2D col)
	{
		if (!_spinner.isStoped)
			return;
        switch (col.gameObject.name)
        {
            case "premioUNO":
                if(FortuneWheel.blocco == false)
                {
                    scoretext.text = "Hai vinto 100 monete";
                    Debug.Log("il Blocco di sicurezza è " + FortuneWheel.blocco);
                    Debug.Log(scoretext.text);
                    FortuneWheel.blocco = true;
                    //inserire premio
                }
                break;
            case "premioDUE":
                if (FortuneWheel.blocco == false)
                {
                    scoretext.text = "Hai vinto 500 monete";
                    Debug.Log("il Blocco di sicurezza è " + FortuneWheel.blocco);
                    Debug.Log(scoretext.text);
                    FortuneWheel.blocco = true;
                    //inserire premio
                }
                break;
            case "premioTRE":
                if (FortuneWheel.blocco == false)
                {
                    scoretext.text = "Hai vinto 50 monete";
                    Debug.Log("il Blocco di sicurezza è " + FortuneWheel.blocco);
                    Debug.Log(scoretext.text);
                    FortuneWheel.blocco = true;
                    //inserire premio
                }
                break;
            case "premioQUATTRO":
                if (FortuneWheel.blocco == false)
                {
                    scoretext.text = "Hai vinto 1 gemma";
                    Debug.Log("il Blocco di sicurezza è " + FortuneWheel.blocco);
                    Debug.Log(scoretext.text);
                    FortuneWheel.blocco = true;
                    //inserire premio
                }
                break;
            case "premioCINQUE":
                if (FortuneWheel.blocco == false)
                {
                    scoretext.text = "Hai vinto 10.000 monete";
                    Debug.Log("il Blocco di sicurezza è " + FortuneWheel.blocco);
                    Debug.Log(scoretext.text);
                    FortuneWheel.blocco = true;
                    //inserire premio
                }
                break;
            case "premioSEI":
                if (FortuneWheel.blocco == false)
                {
                    scoretext.text = "Hai vinto 1.000 monete";
                    Debug.Log("il Blocco di sicurezza è " + FortuneWheel.blocco);
                    Debug.Log(scoretext.text);
                    FortuneWheel.blocco = true;
                    //inserire premio
                }
                break;
            case "premioSETTE":
                if (FortuneWheel.blocco == false)
                {
                    scoretext.text = "Hai vinto 25 monete";
                    Debug.Log("il Blocco di sicurezza è " + FortuneWheel.blocco);
                    Debug.Log(scoretext.text);
                    FortuneWheel.blocco = true;
                    //inserire premio
                }
                break;
            case "premioOTTO":
                if (FortuneWheel.blocco == false)
                {
                    scoretext.text = "Hai vinto 15 gemme";
                    Debug.Log("il Blocco di sicurezza è " + FortuneWheel.blocco);
                    Debug.Log(scoretext.text);
                    FortuneWheel.blocco = true;
                    //inserire premio
                }
                break;

        }

    }

}
