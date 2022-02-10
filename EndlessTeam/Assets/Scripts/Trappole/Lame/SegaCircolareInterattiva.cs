using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SegaCircolareInterattiva : InterazioneTrappole
{
    public GameObject sega;
    public Transform pos1;
    public Transform pos2;

    [SerializeField] bool ruotaSulPosto = false;

    Renderer rend;

    [SerializeField] bool isInteractive = false;

    [SerializeField]
    private GameObject VFXOn;

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (isInteractive)
        {
            //Disattivo il particle che indica che la trappola è utilizzabile
            //if (VFXOn) { if (VFXOn.activeSelf) VFXOn.SetActive(false); }


            GameObject suggTemp = GameManager.instance.GetObjFromArray("Hint3", GameManager.instance.suggestions);
            if (suggTemp.activeSelf) TutorialManager.instance.DisableHint();

            rend.material.SetFloat("_Emission", 10f);
            rend.material.SetColor("_EmissionColor", Color.red);

            CallCoroutineInteraction("RotazioneLamaCo");
        }

    }
    IEnumerator RotazioneLamaCo()
    {
        float elapsedTime = 0f;
        float waitTime = 1;

        while (elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;

            sega.transform.localPosition = Vector3.Lerp(pos1.transform.localPosition, pos2.transform.localPosition, elapsedTime / waitTime);
            sega.transform.Rotate(Vector3.forward, 10);

            yield return null;

        }
        sega.transform.localPosition = pos2.transform.localPosition;
        StartCoroutine(RotazioneLamaCoRev());

    }

    IEnumerator RotazioneLamaCoRev()
    {
        float elapsedTime = 0f;
        float waitTime = 1;

        while (elapsedTime < waitTime)
        {


            sega.transform.localPosition = Vector3.Lerp(pos2.transform.localPosition, pos1.transform.localPosition, elapsedTime / waitTime);
            sega.transform.Rotate(-Vector3.forward, 10);
            elapsedTime += Time.deltaTime;
            yield return null;

        }
        sega.transform.localPosition = pos1.transform.localPosition;
        StartCoroutine(RotazioneLamaCo());

    }

    IEnumerator RotazioneLama()
    {
        float elapsedTime = 0f;
        float waitTime = 50;


        while (elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;

            sega.transform.Rotate(Vector3.forward, 100 * Time.deltaTime);

            yield return null;

        }
        yield return null;

    }

    private void OnEnable()
    {
        rend = GetComponent<Renderer>();

        rend.material.SetFloat("_Emission", 80f);
        rend.material.SetColor("_EmissionColor", Color.green);

        //Attivo il particle che indica che la trappola è utilizzabile
        //if (VFXOn) { if (!VFXOn.activeSelf) VFXOn.SetActive(true); }

        sega.transform.localPosition = pos1.transform.localPosition;

        if (ruotaSulPosto && !isInteractive)
        {
            StartCoroutine(RotazioneLama());
        }


    }

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        sega.transform.localPosition = pos1.transform.localPosition;

        //rend.material.SetFloat("_Emission", 80f);
        //rend.material.SetColor("_EmissionColor", Color.green);



    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable temp = other.GetComponent<IDamageable>();
        if (temp != null) temp.Damage();
    }
}