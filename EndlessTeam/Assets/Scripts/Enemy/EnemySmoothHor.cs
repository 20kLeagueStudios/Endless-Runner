using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmoothHor : MonoBehaviour, IDamageable
{
    public Transform[] trackPos;

    Vector3 targetPos;

    Vector3 startPos;

    Animator anim;

    public float speed;

    bool dead = false;

    GameManager gameManager;
    AudioManager audioManager;

    private void Awake()
    {
        gameManager = GameManager.instance;
        audioManager = GameManager.instance.audioManager;
        anim = GetComponent<Animator>();
        targetPos = trackPos[1].transform.localPosition;
        startPos = trackPos[0].transform.localPosition;

    }

    private void Start()
    {
        targetPos = trackPos[1].transform.localPosition;
        startPos = trackPos[0].transform.localPosition;
    }


    void OnEnable()
    {
        dead = false;

        startPos = trackPos[0].transform.localPosition;

        transform.localPosition = startPos;
        StartCoroutine(ChangePos());

       
    }

    [SerializeField] bool deveAncheRuotare;

    IEnumerator ChangePos()
    {
        float pingpong;
        float elapsedTime = 0;
        float waitTime = 20;

        targetPos = trackPos[1].transform.localPosition;
        startPos = trackPos[0].transform.localPosition;

        while (isActiveAndEnabled && !dead)
        {

            elapsedTime += Time.deltaTime;

            pingpong = Mathf.PingPong((elapsedTime/waitTime)*speed, 1);

            transform.localPosition = Vector3.Lerp(startPos, targetPos, pingpong);


            if (deveAncheRuotare)
            {
                if (transform.localPosition.x >= targetPos.x - 0.2f)
                {
                    transform.localRotation = Quaternion.Euler(0, -90, 0);
                }
                if (transform.localPosition.x <= startPos.x + 0.2f)
                {
                    transform.localRotation = Quaternion.Euler(0, 90, 0);
                }
            }
          
            yield return null;

        }

        Death();

        yield return null;

    }


    public void Death()
    {
        dead = true;
        audioManager.PlaySound("MoneyBag");
        GameManager.instance.currentMoney += 50;
        anim.SetTrigger("Death");
        //transform.gameObject.SetActive(false);
        StopAllCoroutines();
        //transform.gameObject.SetActive(false);
    }

    public void Disable()
    {
        transform.gameObject.SetActive(false);
        gameManager.toReactive.Add(this.gameObject);
    }

    public void Damage()
    {
        Death();
        gameManager.IncreaseScore(200);
    }
}
