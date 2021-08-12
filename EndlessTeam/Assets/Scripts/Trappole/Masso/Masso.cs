using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masso : MonoBehaviour
{
    [SerializeField]
    Transform finalPos;

    float vel = 0;

    [SerializeField]
    float speed;
    IEnumerator FallCor()
    {
        Vector3 temp = transform.position;
        while(transform.position.y != finalPos.position.y)
        {
            temp.x = transform.position.x;
            temp.z = transform.position.z;
            temp.y = Mathf.SmoothDamp(temp.y, finalPos.position.y, ref vel ,0.1f);

            if (Mathf.Abs(temp.y - finalPos.position.y ) < .001f)
                temp.y = finalPos.position.y;

            transform.position = temp;

            yield return null;

            }

        yield return null;
    }

    public void CallFallCor()
    {
        StartCoroutine("FallCor");
    }
}
