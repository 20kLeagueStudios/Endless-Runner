using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FortuneWheel : MonoBehaviour
{
	[SerializeField]
	private float reducer;
	[SerializeField]
	private float multiplier = 1;
	private bool round1 = true;
	public bool isStoped = false;
	public static bool blocco = true;
	[SerializeField]
	private GameObject testo = default;
	private int x;


	void Start()
	{
		testo.SetActive(false);
		reducer = Random.Range(0.01f, 0.3f);
		x = 0;
	}

	// Update is called once per frameQ
	void FixedUpdate()
	{

		

		if (multiplier > 0)
		{
			transform.Rotate(Vector3.forward, 1 * multiplier);
		}
		else
		{
			isStoped = true;
	
			if (x == 1)
            {
				x = 0;
				testo.SetActive(true);
				blocco = false;
			}
			

		}

		if (multiplier < 20 && !round1)
		{
			multiplier += 0.1f;
		}
		else
		{
			round1 = true;
		}

		if (round1 && multiplier > 0)
		{
			multiplier -= reducer;
		}
	}


	public void GiraRuota()
	{
		x = 1;
		testo.SetActive(false);
		blocco = true;
		Reset();
	}

	void Reset()
	{
		multiplier = 1;
		reducer = Random.Range(0.01f, 0.5f);
		round1 = false;
		isStoped = false;
	}
}

