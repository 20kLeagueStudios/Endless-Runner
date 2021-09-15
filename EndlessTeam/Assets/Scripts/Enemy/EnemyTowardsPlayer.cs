using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowardsPlayer : MonoBehaviour, IDamageable
{
    Vector3 targetPos;

    Vector3 startPos;

    Animator anim;

    public float offsetZ;
    public float speed;

    public LayerMask layerMask;

    public bool isAttack = false;

    GameObject lasthit;

    public Vector3 collision = Vector3.zero;

    Ray ray;

    AudioManager audioManager;
    GameManager gameManager;

    private void Awake()
    {
        audioManager = GameManager.instance.audioManager;
        gameManager = GameManager.instance;

        anim = this.gameObject.GetComponent<Animator>();

        startPos = transform.localPosition;
        targetPos = new Vector3(startPos.x, startPos.y, startPos.z + offsetZ);

    }

    void OnEnable()
    {
        isAttack = false;
        // transform.localPosition = trackPos[rndPos].transform.localPosition;
        transform.localPosition = startPos;
        anim.Play("GranchioRoccia_idle2");

    }

    private void OnDisable()
    {
        transform.localPosition = startPos;
    }

    IEnumerator ChangePos()
    {
        float elapsedTime = 0;
        float waitTime = 8f;

        anim.SetTrigger("Attack");

        while (elapsedTime < waitTime)
        {

            elapsedTime += Time.deltaTime;

            transform.localPosition = Vector3.Lerp(startPos, targetPos, (elapsedTime / waitTime) * speed);


            yield return null;

        }


        transform.localPosition = targetPos;


        yield return null;

    }

    void Start()
    {
        startPos = transform.localPosition;
    }

 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(collision, 3.2f);

    }

    Vector3 tmp;

    // Update is called once per frame
    void Update()
    {
        
        tmp = new Vector3(this.transform.position.x, this.transform.position.y + .5f, this.transform.position.z);

        RaycastHit hit;

        ray = new Ray(tmp, this.transform.forward);

        if (Physics.Raycast(ray, out hit, 30f, layerMask, QueryTriggerInteraction.Ignore))
        {
            collision = hit.point;

            lasthit = hit.transform.gameObject;

            if (lasthit.GetComponent<PlayerMovement>())
            {
                StartCoroutine(ChangePos());

             
            }
     
        }

    }

    public void Death()
    {
        audioManager.PlaySound("MoneyBag");
        GameManager.instance.currentMoney += 50;
        anim.SetTrigger("Death");
        StopAllCoroutines();
        this.gameObject.SetActive(false);
    }

    public void Disable()
    {
        transform.gameObject.SetActive(false);
    }

    public void Damage()
    {
        Death();
        gameManager.IncreaseScore(200);
    }
}
