using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowardsPlayer : MonoBehaviour
{
    Vector3 targetPos;

    Vector3 startPos;

    Animator anim;

    public float offsetZ;
    public float speed;

    public LayerMask layerMask;

    GameObject lasthit;

    public Vector3 collision = Vector3.zero;

    Ray ray;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        startPos = transform.localPosition;

        targetPos = new Vector3(startPos.x, startPos.y, startPos.z - offsetZ);

    }

    void OnEnable()
    {
        transform.localPosition = startPos;
        anim.Play("IdleNormal");

    }

    IEnumerator ChangePos()
    {
        float elapsedTime = 0;
        float waitTime = 1.5f;

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

        if (Physics.Raycast(ray, out hit, 30f, layerMask))
        {
            anim.Play("Attack01");

            collision = hit.point;

            StartCoroutine(ChangePos());

            lasthit = hit.transform.gameObject;

            Debug.Log("hit"+lasthit.name);

        }


    }

}
