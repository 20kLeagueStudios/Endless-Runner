using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FortuneWheel : MonoBehaviour
{

    private int randomValue;
    private float timeInterval;
    private bool coroutineAllowed;
    private int finalAngle;

    [SerializeField]
    private Text winText;

    // Start is called before the first frame update
    void Start()
    {
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(finalAngle);
    }

    private IEnumerator Spin()
    {
        coroutineAllowed = false;
        randomValue = Random.Range(20, 30);
        timeInterval = 0.1f;

        for(int i = 0; i < randomValue; i++)
        {
            transform.Rotate(0, 0, 22.5f);
            if (i > Mathf.RoundToInt(randomValue * 0.5f))
                timeInterval = 0.2f;
            if (i > Mathf.RoundToInt(randomValue * 0.85f))
                timeInterval = 0.4f;
            yield return new WaitForSeconds(timeInterval);
        }

        if (Mathf.RoundToInt(transform.eulerAngles.z) % 45 != 0)
            transform.Rotate(0, 0, 22.5f);

        finalAngle = Mathf.RoundToInt(transform.eulerAngles.z);
        
        switch (finalAngle)
        {
            case 0:
                winText.text = "Verde acqua";
                break;

            case 45:
                winText.text = "Azzurro";
                break;

            case 90:
                winText.text = "Blu";
                break;

            case 135:
                winText.text = "Viola";
                break;

            case 180:
                winText.text = "Porpora";
                break;

            case 225:
                winText.text = "Arancione";
                break;

            case 270:
                winText.text = "Giallo";
                break;

            case 315:
                winText.text = "Verde";
                break;
        }
        coroutineAllowed = true;
    }

    public void GiraRuota()
    {
        StartCoroutine(Spin());
    }
}
