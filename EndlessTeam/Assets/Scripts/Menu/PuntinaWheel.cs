using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuntinaWheel : MonoBehaviour
{
	public FortuneWheel _spinner;
	public Text scoretext;
	public GameObject testo;
	// Use this for initialization
	void Start()
	{
		testo.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (!_spinner.isStoped)
			return;
		scoretext.text = col.gameObject.name;

	}

}
